using System;

namespace RestaurantInventory.Models
{
    public class Filter
    {
        public string Naziv { get; set; }
        public Kategorija Kategorija { get; set; }
        public string Dobavljac { get; set; }
        public double? MinKolicina { get; set; }
        public double? MaxKolicina { get; set; }
        public double? MinCijena { get; set; }
        public double? MaxCijena { get; set; }

        public Filter(string naziv = null, Kategorija kategorija = Kategorija.None,
                      string dobavljac = null, double? minKolicina = null, double? maxKolicina = null,
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
    }
}
