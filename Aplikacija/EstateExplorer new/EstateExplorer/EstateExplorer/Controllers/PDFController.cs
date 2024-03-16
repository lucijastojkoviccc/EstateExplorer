using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EstateExplorer.Data;
using EstateExplorer.Models;
using Azure.Messaging;
using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.parser;


namespace EstateExplorer.Controllers
{
    [ApiController]
    [Route("api")]
    public class PDFController : Controller
    {
        public ApplicationDbContext Context { get; set; }
        public UserManager<ApplicationUser> UserManager { get; set; }
        public SignInManager<ApplicationUser> SignInManager { get; set; }
        public PDFController(ApplicationDbContext dbcontext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            Context = dbcontext;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [HttpGet]
        [Route("podaciPDF/{idStana}/{loc}/{jmbg}/{mestoKupca}/{adresaKupca}")]
        public async Task<ActionResult> generisiPDF( int idStana, string loc, string jmbg, string mestoKupca, string adresaKupca)
        {
            
            //MORA ŠIŠANA LATINICA KAD NIJE ŠIŠANA ONDA JE NEMA
            var kupac = await SignInManager.UserManager.GetUserAsync(HttpContext.User);

            var mestoProdaje = loc;
            var datum = DateTime.Now.ToString();

            var nazivFirme = "Hidrogradnja promet";
            var mestoFirme = "Cacak";
            var adresaFirme = "Kneza Milosa 28";
            var pibFirme = "1234567890";

            var imeKupca = kupac.Ime;
            var prezimeKupca = kupac.Prezime;
            var jmbgKupca = jmbg;

            var stan = await Context.Stanovi.Include(p => p.Zgrada).FirstOrDefaultAsync(p => p.id == idStana);
            if(stan == null) { return BadRequest("Nije moguce pronaci stan"); }
            var ukupnaCena = stan.Povrsina * stan.CenaPoKvadratuBezPDV * 1.2;
            var zgrada = stan.Zgrada;

            var htmlString = $@"
          <!DOCTYPE html>
            <html>
            <head>
                <meta charset='UTF-8'>
              
            </head>
            <body>
                <h1>Ugovor o kupoprodaji nekretnine</h1>
                <div class='section'>
                    <p>zakljucen u gradu: {mestoProdaje} dana {datum} godine, izmedju ugovornih strana:</p>
                    <p>1. {nazivFirme} (PIB:{pibFirme}) iz {mestoFirme}, ul. {adresaFirme} , kao prodavca nekretnine, s jedne strane i</p>
                    <p>2. {imeKupca} {prezimeKupca} ({jmbgKupca}) iz {mestoKupca}, ul. {adresaKupca} s druge strane, kao kupca nekretnine.</p>
                    <h3>Ugovorne strane su se sporazumele o sledecem:</h3> 
        <p >CLAN 1.
        Predmet ovog ugovora o kupoprodaji je stan u {mestoProdaje}, ul. {zgrada.Ulica} br. {stan.BrojUlaza}, sprat {stan.Sprat}, stan br. {stan.Broj}</p>
        <p>CLAN 2.
        Prodavac prodaje, a kupac kupuje stan iz clana 1. ovog ugovora, po medjusobno ugovorenoj ceni od {ukupnaCena}€ sa uracunatim PDV-om koju ce kupac isplatiti u dinarskoj protivvrednosti.</p>
        <p>CLAN 3.
        Zakljucenjem ovog ugovora, ugovorne strane potvrdjsuju da je kupcu predata u posed nepokretnost iz clana 1. ovog ugovora, ispraznjena od lica i stvari.</p>
        <p>CLAN 4.
        Prodavac garantuje kupcu da danom overe ovog ugovora kod Suda, na predmetnoj nepokretnosti nema nikakvih tereta, da nepokretnost nije predmet nikakvog spora, ni drugog pravnog posla i da prema istoj treca lica nemaju nikakvih prava ni potrazivanja, te se obavezuje da kupcu pruzi zastitu od evikcije.
        Prodavac garantuje kupcu da predmetna nepokretnost nema nikakvih skrivenih nedostataka, a kupuje se u vidjenom stanju.
        </p>
        <p>CLAN 5.
        Ugovorne strane su se sporazumele o snosenju troskova overe ovog ugovora i poreza na prenos apsolutnih prava po ovom ugovoru.
        </p> 
        <p>CLAN 6.
        Prilikom predaje stana (predmetne nepokretnosti) u posed, prodavac je duzan predati kupcu svu dokumentaciju koja se odnosi na pravo svojine na nepokretnosti, priznanice o placenim uslugama, naknadama i porezima koje se odnose na koriscenje predmetne nepokretnosti, zakljucno sa danom predaje nepokretnosti.

        </p>
        <p>CLAN 7.
        Prodavac je saglasan da na osnovu ovog ugovora kupac moze upisati pravo svojine u zemljisnim knjigama na svoje ime, kao novi vlasnik i drzalac te nepokretnosti, bez davanja posebne izjave i prisustva prodavca.
        Ova izjava je ""clausula intabulandi"".
        </p>
        <p>CLAN 8.
        Ugovorne strane su saglasne da ce uzajamne sporove resavati mirnim putem a ako ne postignu sporazum, spor ce resavati nadlezni sud u {mestoProdaje}.
        </p>
        <p>CLAN 9.
        Ugovorne strane su ovaj ugovor procitale i razumele, te ga u znak saglasnosti i pristanka svojerucno potpisuju.
        </p>
        <p>CLAN 10.
        Ovaj ugovor je sacinjen u pet istovetnih primeraka, od kojih tri pripadaju kupcu, a jedan primerak pripada prodavcu, dok je jedan primerak za Sud.
        U {mestoProdaje}, dana {datum} godine.
        </p>
        <p style=""margin:50px"" >Prodavac _____________________ </p>
        <p style=""margin:50px"" >Kupac _______________________</p>
                </div>
            </body>
            </html>";
            
            try
            {
                var outputStream = new MemoryStream();

                var document = new Document();
                var writer = PdfWriter.GetInstance(document, outputStream);

               
                document.Open();

                using (var stringReader = new StringReader(htmlString))
                {
                   
                    var worker = new HTMLWorker(document);
                    worker.Parse(stringReader);
                }

                document.Close();

                string outputFilePath = $"ClientApp\\public\\Ugovori\\{zgrada.Naziv}-{stan.Broj}.pdf";  
                System.IO.File.WriteAllBytes(outputFilePath, outputStream.ToArray());
                return await Task.FromResult(Ok("PDF generated successfully!"));
            }
            catch (Exception ex) { return await Task.FromResult(BadRequest("An error occurred: " + ex.Message)); }
        }
        
    }
   }

