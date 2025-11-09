using System.Collections.Generic;
using RestaurantInventory.Models;

namespace RestaurantInventory.Services
{
    public static class FilterService
    {
        public static List<StavkaInventara> PrimijeniFilter(List<StavkaInventara> stavke, Filter filter)
        {
            if (filter == null) return stavke;
            var rezultat = new List<StavkaInventara>();

            foreach (var s in stavke)
            {
                bool dodaj = true;

                if (!string.IsNullOrEmpty(filter.Naziv) && !s.Naziv.Equals(filter.Naziv, System.StringComparison.OrdinalIgnoreCase))
                    dodaj = false;

                if (filter.Kategorija != Kategorija.None && s.Kategorija != filter.Kategorija)
                    dodaj = false;

                if (!string.IsNullOrEmpty(filter.Dobavljac) && !s.Dobavljac.Equals(filter.Dobavljac, System.StringComparison.OrdinalIgnoreCase))
                    dodaj = false;

                if (filter.MinKolicina.HasValue && s.Kolicina < filter.MinKolicina.Value)
                    dodaj = false;

                if (filter.MaxKolicina.HasValue && s.Kolicina > filter.MaxKolicina.Value)
                    dodaj = false;

                if (filter.MinCijena.HasValue && s.Cijena < filter.MinCijena.Value)
                    dodaj = false;

                if (filter.MaxCijena.HasValue && s.Cijena > filter.MaxCijena.Value)
                    dodaj = false;

                if (dodaj)
                    rezultat.Add(s);
            }

            return rezultat;
        }
    }
}
