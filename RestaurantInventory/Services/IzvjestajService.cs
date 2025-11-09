using System;
using System.Linq;
using RestaurantInventory.Models;

namespace RestaurantInventory.Services
{
    public static class IzvjestajService
    {
        public static void Generisi(Izvjestaj izvjestaj, System.Collections.Generic.List<StavkaInventara> lista)
        {
            if (izvjestaj == null || lista == null) return;

            izvjestaj.Stavke.Clear();
            izvjestaj.UkupnaVrijednost = 0;

            foreach (var s in lista)
            {
                if (izvjestaj.PocetakPerioda.HasValue && izvjestaj.KrajPerioda.HasValue &&
                    (s.DatumNabavke < izvjestaj.PocetakPerioda.Value || s.DatumNabavke > izvjestaj.KrajPerioda.Value))
                    continue;

                if (izvjestaj.FilterKategorija.HasValue && s.Kategorija != izvjestaj.FilterKategorija.Value)
                    continue;

                if (izvjestaj.SamoKriticne && !StavkaInventaraService.JeKriticna(s))
                    continue;

                izvjestaj.Stavke.Add(s);
                izvjestaj.UkupnaVrijednost += s.Cijena * s.Kolicina;
            }
        }
    }
}
