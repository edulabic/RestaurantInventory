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


        public static List<StavkaInventara> PrimijeniFilter(List<StavkaInventara> stavke, Filter filter)
        {
            if (filter == null) return stavke;
            var rezultat = new List<StavkaInventara>();
            bool checkNaziv = !string.IsNullOrEmpty(filter.Naziv);
            string nazivFilter = filter.Naziv;
            bool checkDobavljac = !string.IsNullOrEmpty(filter.Dobavljac);
            string dobavljacFilter = filter.Dobavljac;
            bool checkMinKolicina = filter.MinKolicina.HasValue;
            double minKolicina = filter.MinKolicina.GetValueOrDefault();
            bool checkMaxKolicina = filter.MaxKolicina.HasValue;
            double maxKolicina = filter.MaxKolicina.GetValueOrDefault();
            bool checkMinCijena = filter.MinCijena.HasValue;
            double minCijena = filter.MinCijena.GetValueOrDefault();
            bool checkMaxCijena = filter.MaxCijena.HasValue;
            double maxCijena = filter.MaxCijena.GetValueOrDefault();
            bool checkKategorija = filter.Kategorija != Kategorija.None;
            Kategorija kategorijaFilter = filter.Kategorija;
            foreach (var s in stavke)
            {
                if (checkNaziv && !s.Naziv.Equals(nazivFilter, StringComparison.OrdinalIgnoreCase))
                    continue;
                if (checkKategorija && s.Kategorija != kategorijaFilter)
                    continue;
                if (checkDobavljac && !s.Dobavljac.Equals(dobavljacFilter, StringComparison.OrdinalIgnoreCase))
                    continue;
                if (checkMinKolicina && s.Kolicina < minKolicina)
                    continue;
                if (checkMaxKolicina && s.Kolicina > maxKolicina)
                    continue;
                if (checkMinCijena && s.Cijena < minCijena)
                    continue;
                if (checkMaxCijena && s.Cijena > maxCijena)
                    continue;
                rezultat.Add(s);
            }

            return rezultat;
        }


    }
}
