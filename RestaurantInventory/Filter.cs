using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInventory
{
    internal class Filter
    {
        public string Naziv { get; set; }
        public Kategorija Kategorija { get; set; }
        public string Dobavljac { get; set; }
        public double? MinKolicina { get; set; }
        public double? MaxKolicina { get; set; }
        public double? MinCijena { get; set; }
        public double? MaxCijena { get; set; }

        public Filter(string naziv = null, Kategorija kategorija = Kategorija.None, string dobavljac = null,
                      double? minKolicina = null, double? maxKolicina = null,
                      double? minCijena = null, double? maxCijena = null)
        {
            Naziv = naziv;
            Kategorija = kategorija;
            Dobavljac = dobavljac;
            MinKolicina = minKolicina;
            MaxKolicina = maxKolicina;
            MinCijena = minCijena;
            MaxCijena = maxCijena;
        }

        // Glavna metoda – primjenjuje filter na listu stavki
        public List<StavkaInventara> Primijeni(List<StavkaInventara> lista)
        {
            var rezultat = lista.AsQueryable();

            if (!string.IsNullOrEmpty(Naziv))
                rezultat = rezultat.Where(s => s.Naziv.Equals(Naziv, StringComparison.OrdinalIgnoreCase));

            if (Kategorija != Kategorija.None)
                rezultat = rezultat.Where(s => s.Kategorija == Kategorija);

            if (!string.IsNullOrEmpty(Dobavljac))
                rezultat = rezultat.Where(s => s.Dobavljac.Equals(Dobavljac, StringComparison.OrdinalIgnoreCase));

            if (MinKolicina.HasValue)
                rezultat = rezultat.Where(s => s.Kolicina >= MinKolicina.Value);

            if (MaxKolicina.HasValue)
                rezultat = rezultat.Where(s => s.Kolicina <= MaxKolicina.Value);

            if (MinCijena.HasValue)
                rezultat = rezultat.Where(s => s.Cijena >= MinCijena.Value);

            if (MaxCijena.HasValue)
                rezultat = rezultat.Where(s => s.Cijena <= MaxCijena.Value);

            return rezultat.ToList();
        }
    }
}
