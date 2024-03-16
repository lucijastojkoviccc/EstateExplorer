export class RataPrikaz {
    id;
    iznosKupac;
    datumKupac;
    iznosRadnik;
    datumRadnik;
    iznosKonacan;
    valuta;
    kes;
    nekretnina;
    status;

    constructor() {
        this.iznosKupac = 0;
        this.valuta = "EUR";
        this.kes = "Kes";
        this.status = "Ceka se potvrda";
    }
}