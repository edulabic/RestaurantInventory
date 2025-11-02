using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInventory
{
    internal class Izvjestaj
    {
        public string Tip { get; set; }
        public List<StavkaInventara> Stavke { get; private set; }
        public double UkupnaVrijednost { get; private set; }
        public DateRange Period { get; set; }

        public Izvjestaj(string tip, DateRange period)
        {
            Tip = tip;
            Period = period;
            Stavke = new List<StavkaInventara>();
            UkupnaVrijednost = 0;
        }

        // Generiše izvještaj na osnovu liste stavki
        public void Generisi(List<StavkaInventara> lista)
        {
            Stavke = lista
                .Where(s => s.DatumNabavke >= Period.Pocetak && s.DatumNabavke <= Period.Kraj)
                .ToList();

            UkupnaVrijednost = Stavke.Sum(s => s.Cijena * s.Kolicina);
        }

        // Ispis izvještaja
        public void Prikazi()
        {
            Console.WriteLine($"\n--- IZVJEŠTAJ: {Tip} ---");
            Console.WriteLine($"Period: {Period.Pocetak.ToShortDateString()} - {Period.Kraj.ToShortDateString()}");
            Console.WriteLine($"Ukupna vrijednost: {UkupnaVrijednost:F2} KM");
            Console.WriteLine("Stavke u izvještaju:");
            foreach (var s in Stavke)
                s.PrikaziInfo();
        }
    }
}
