using System;

namespace RestaurantInventory.Models
{
    public class StavkaInventara
    {
        public string Naziv { get; set; }
        public double Cijena { get; set; }
        public double Kolicina { get; set; }
        public string Dobavljac { get; set; }
        public DateTime DatumNabavke { get; set; }
        public double MinKolicina { get; set; }
        public string Opis { get; set; }
        public Kategorija Kategorija { get; set; }

        public StavkaInventara() { }

        public StavkaInventara(string naziv, double cijena, double kolicina, string dobavljac,
                               DateTime datumNabavke, double minKolicina, Kategorija kategorija)
        {
            Naziv = naziv;
            Cijena = cijena;
            Kolicina = kolicina;
            Dobavljac = dobavljac;
            DatumNabavke = datumNabavke;
            MinKolicina = minKolicina;
            Kategorija = kategorija;
        }
    }
}
