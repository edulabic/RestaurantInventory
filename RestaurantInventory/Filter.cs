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
            var rezultat = new List<StavkaInventara>();

            foreach (var s in lista)
            {
                bool dodaj = true;

                // Provjera naziva
                if (!string.IsNullOrEmpty(Naziv))
                {
                    if (!s.Naziv.Equals(Naziv, StringComparison.OrdinalIgnoreCase))
                        dodaj = false;
                }

                // Provjera kategorije
                if (Kategorija != Kategorija.None)
                {
                    if (s.Kategorija != Kategorija)
                        dodaj = false;
                }

                // Provjera dobavljača
                if (!string.IsNullOrEmpty(Dobavljac))
                {
                    if (!s.Dobavljac.Equals(Dobavljac, StringComparison.OrdinalIgnoreCase))
                        dodaj = false;
                }

                // Provjera minimalne količine
                if (MinKolicina.HasValue)
                {
                    if (s.Kolicina < MinKolicina.Value)
                        dodaj = false;
                }

                // Provjera maksimalne količine
                if (MaxKolicina.HasValue)
                {
                    if (s.Kolicina > MaxKolicina.Value)
                        dodaj = false;
                }

                // Provjera minimalne cijene
                if (MinCijena.HasValue)
                {
                    if (s.Cijena < MinCijena.Value)
                        dodaj = false;
                }

                // Provjera maksimalne cijene
                if (MaxCijena.HasValue)
                {
                    if (s.Cijena > MaxCijena.Value)
                        dodaj = false;
                }

                // Ako je prošao sve provjere, dodaj u rezultat
                if (dodaj)
                {
                    rezultat.Add(s);
                }
            }

            return rezultat;
        }
    }
}
