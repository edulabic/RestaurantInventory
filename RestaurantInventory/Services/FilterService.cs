using System.Collections.Generic;
using RestaurantInventory.Models;

namespace RestaurantInventory.Services
{
    public static class FilterService
    {
        /* public static List<StavkaInventara> PrimijeniFilter(List<StavkaInventara> stavke, Filter filter)
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
         }*/


        private static bool ProlaziNaziv(StavkaInventara s, Filter f)
        {
            if (string.IsNullOrEmpty(f.Naziv))
                return true;

            return s.Naziv.Equals(f.Naziv, StringComparison.OrdinalIgnoreCase);
        }
        private static bool ProlaziKategoriju(StavkaInventara s, Filter f)
        {
            if (f.Kategorija == Kategorija.None)
                return true;

            return s.Kategorija == f.Kategorija;
        }
        private static bool ProlaziDobavljaca(StavkaInventara s, Filter f)
        {
            if (string.IsNullOrEmpty(f.Dobavljac))
                return true;

            return s.Dobavljac.Equals(f.Dobavljac, StringComparison.OrdinalIgnoreCase);
        }
        private static bool ProlaziKolicinu(StavkaInventara s, Filter f)
        {
            if (f.MinKolicina.HasValue && s.Kolicina < f.MinKolicina.Value)
                return false;

            if (f.MaxKolicina.HasValue && s.Kolicina > f.MaxKolicina.Value)
                return false;

            return true;
        }
        private static bool ProlaziCijenu(StavkaInventara s, Filter f)
        {
            if (f.MinCijena.HasValue && s.Cijena < f.MinCijena.Value)
                return false;

            if (f.MaxCijena.HasValue && s.Cijena > f.MaxCijena.Value)
                return false;

            return true;
        }

        private static bool ProlaziSveFiltere(StavkaInventara s, Filter f) =>
            ProlaziNaziv(s, f) &&
            ProlaziKategoriju(s, f) &&
            ProlaziDobavljaca(s, f) &&
            ProlaziKolicinu(s, f) &&
            ProlaziCijenu(s, f);

        public static List<StavkaInventara> PrimijeniFilter(List<StavkaInventara> stavke, Filter filter)
        {
            if (filter == null) return stavke;

            var rezultat = new List<StavkaInventara>();
            foreach (var s in stavke)
                if (ProlaziSveFiltere(s, filter))
                    rezultat.Add(s);

            return rezultat;
        }


    }
}
