using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInventory
{
    internal class StavkaInventara
    {
        public String Naziv { get; set; }
        public double Cijena { get; set; }
        public double Kolicina { get; set; }
        public string Dobavljac { get; set; }
        public DateTime DatumNabavke { get; set; }
        public double MinKolicina { get; set; }
        public Kategorija Kategorija { get; set; }

        public StavkaInventara() { }
        public StavkaInventara(string naziv, double cijena, double kolicina, string dobavljac, DateTime datumNabavke, int minKolicina, Kategorija kategorija)
        {
            Naziv = naziv;
            Cijena = cijena;
            Kolicina = kolicina;
            Dobavljac = dobavljac;
            DatumNabavke = datumNabavke;
            MinKolicina = minKolicina;
            Kategorija = kategorija;
        }

        public bool JeKriticna()
        {
            return Kolicina < MinKolicina;
        }

        // Metoda za ažuriranje količine
        public void AzurirajKolicinu(double novaKolicina)
        {
            Kolicina = novaKolicina;
        }

        // Metoda za ispis informacija
        public void PrikaziInfo()
        {
            Console.WriteLine($"Naziv: {Naziv}, Količina: {Kolicina}, Min: {MinKolicina}, Kritična: {JeKriticna()}");
        }



    }
}
