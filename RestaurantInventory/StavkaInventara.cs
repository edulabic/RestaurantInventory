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
        public int Kolicina { get; set; }
        public string Dobavljac { get; set; }
        public DateTime DatumNabavke { get; set; }
        public int MinKolicina { get; set; }
        public Kategorija Kategorija { get; set; }

        public StavkaInventara() { }
        public StavkaInventara(string naziv, double cijena, int kolicina, string dobavljac, DateTime datumNabavke, int minKolicina, Kategorija kategorija)
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

        // Metoda: ažurira količinu
        public void AzurirajKolicinu(int novaKolicina)
        {
            Kolicina = novaKolicina;
        }

        // (opcionalno) metoda za ispis informacija
        public void PrikaziInfo()
        {
            Console.WriteLine($"Naziv: {Naziv}, Količina: {Kolicina}, Min: {MinKolicina}, Kritična: {JeKriticna()}");
        }



    }
}
