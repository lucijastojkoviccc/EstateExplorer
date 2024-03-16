import React, { Component } from 'react';
import { useNavigate } from 'react-router-dom';



 const Home = () =>{
  const displayName = Home.name;
  const navigate = useNavigate();

 
      return (
           
          <div className="pocetna" id="pocetna">
              <h1 className="display-3">
                  NOVO U PONUDI

              </h1>
              <div>
                  <button className="btn btn-primary btn-lg" onClick={() => navigate('/ONama')}>
                      Vise o nama
                  </button>
              </div>
              <hr className="hrZ"/>
              <h1 className="zasto">ZASTO IZABRATI NEKI OD NASIH STANOVA?</h1>
              <div className="zasto-stan">
                 
                  <div className="bezbednost">
                      <h4>BEZBEDNOST</h4>
                      <hr className="hrP"/>
                      <p>Da biste se Vi i vasa porodica osecali bezbedno,</p>
                      <p> u svaki stan ugradjujemo sigurnosna kasa vrata.</p>
                      <p> Svaki nas objekat poseduje video nadzor garaze,</p>
                      <p> parkinga i ulaza, kao i ulaznu kapiju sa daljinskom kontrolom.</p>
                  </div>

                  <div className="komfor">
                      <h4>KOMFOR</h4>
                      <hr className="hrP" />
                      <p>U cilju povecanja nivoa komforta u stanovima,</p>
                      <p> ugradjujemo hidromasazne tus kabine, protosne bojlere,</p>
                      <p>WC solje sa funkciom bidea, elektricne roletne, klima uredjaj,</p>
                      <p>podno grejanje u kupatilu i kuhinji, senzorska svetla u hodnicima i jos toga!</p>
                  </div>

                  <div className="kvalitet-izrade">
                      <h4>KVALITET IZRADE</h4>
                      <hr className="hrP" />
                      <p>Veliku paznju posvecujemo izboru najboljih materijala,</p>
                      <p> pa tako izmedju ostalog u izgradnji svakog stana koristimo</p>
                      <p>Tarkett parket sa 30 godina garancije, Ideal Standard i Geberit sanitariju,</p>
                      <p > elektro opremu Legrand.</p>
                  </div>


                  <div className="det">
                      <h4>DETALJI</h4>
                      <hr className="hrP" />
                      <p>Nasi objekti razlikuju se od drugih i zbog paznje koju posvecujemo detaljima!</p>
                      <p>Izdvajamo se postavljanjem detalja od mermera, kovanog gvozdja, grejaca i senzora</p>
                      <p>za odledjavanje stepenica ispred ulaznih vrata, spustenim plafonima sa dekorativnom</p>
                      <p> LED rasvetom, kao i ugradnjom ugaonih plafonskih lajsni u stanovima!</p>
                  </div>



              </div>
              
          </div>
    );
  
}

export default Home;

