import Nekretnina from "./Nekretnina";

class Stan extends Nekretnina {
    povrsina;
    brojSoba;
    cenaKv;
    sprat;
    brojUlaza;
    orijentacija;
    opis;

    constructor() {
        super();
        this.povrsina = 0;
        this.brojSoba = 0;
        this.cenaKv = 0;
        this.sprat = 0;
        this.brojUlaza = 0;
        this.orijentacija = "";
        this.opis = "";
    }
}

export default Stan;