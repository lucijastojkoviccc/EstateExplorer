class Zgrada {
    id;
    naziv;
    ulica;
    brojZgrade;
    brojKatastarskeParcele;
    lift;
    brojSpratova;
    grejanje;
    opis;
    katastarskaOpstina;
    stanovi;

    constructor(zgrada = undefined) {
        if(zgrada === undefined) {
            this.id = -1;
            this.naziv = 'naziv zgrade';
            this.ulica = 'ulica';
            this.brojZgrade = 'broj zgrade';
            this.brojKatastarskeParcele = 'broj KP';
            this.lift = true;
            this.brojSpratova = 1;
            this.grejanje = 'Tip grejanja';
            this.opis = 'opis zgrade';
            this.katastarskaOpstina = 'KO';
            this.stanovi = [];
        }
        else {
            this.id                     = zgrada.id                     ;
            this.naziv                  = zgrada.naziv                  ;
            this.ulica                  = zgrada.ulica                  ;
            this.brojZgrade             = zgrada.brojZgrade             ;
            this.brojKatastarskeParcele = zgrada.brojKatastarskeParcele ;
            this.lift                   = zgrada.lift                   ;
            this.brojSpratova           = zgrada.brojSpratova           ;
            this.grejanje               = zgrada.grejanje               ;
            this.opis                   = zgrada.opis                   ;
            this.katastarskaOpstina     = zgrada.katastarskaOpstina     ;
            this.stanovi                = zgrada.stanovi !== undefined ? [...zgrada.stanovi] : undefined;  
        }
    }
}

export default Zgrada;