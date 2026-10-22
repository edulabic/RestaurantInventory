using System;
using System.Linq;
using RestaurantInventory.Models;

namespace RestaurantInventory.Services
{
    public static class IzvjestajService
    {
        /*public static void Generisi(Izvjestaj izvjestaj, List<StavkaInventara> lista)
         {
             if (izvjestaj == null)
                 throw new ArgumentNullException();

             if (lista == null || lista.Count == 0)
                 return;

             izvjestaj.Stavke.Clear();
             izvjestaj.UkupnaVrijednost = 0;

             foreach (var s in lista)
             {
                 if (s == null)
                     continue;

                 if (izvjestaj.PocetakPerioda.HasValue && izvjestaj.KrajPerioda.HasValue)
                 {
                     if (s.DatumNabavke < izvjestaj.PocetakPerioda.Value ||
                         s.DatumNabavke > izvjestaj.KrajPerioda.Value)
                         continue;
                 }

                 if (izvjestaj.FilterKategorija.HasValue &&
                     s.Kategorija != izvjestaj.FilterKategorija.Value)
                     continue;

                 if (izvjestaj.SamoKriticne &&
                     !StavkaInventaraService.JeKriticna(s))
                     continue;

                 double vrijednost = s.Cijena * s.Kolicina;

                 if (vrijednost > 1000)
                     vrijednost *= 0.95;

                 izvjestaj.Stavke.Add(s);
                 izvjestaj.UkupnaVrijednost += vrijednost;
             }
         }*/

        /*public static void Generisi(Izvjestaj izvjestaj, List<StavkaInventara> lista)
        {
            if (izvjestaj == null)
                throw new ArgumentNullException();

            if (lista == null || lista.Count == 0)
                return;

            izvjestaj.Stavke.Clear();
            izvjestaj.UkupnaVrijednost = 0;

            var pocetak = izvjestaj.PocetakPerioda;
            var kraj = izvjestaj.KrajPerioda;
            var filterKategorija = izvjestaj.FilterKategorija;
            var samoKriticne = izvjestaj.SamoKriticne;

            foreach (var s in lista)
            {
                if (s == null) continue;

                if (pocetak.HasValue && kraj.HasValue &&
                    (s.DatumNabavke < pocetak.Value || s.DatumNabavke > kraj.Value))
                    continue;

                if (filterKategorija.HasValue && s.Kategorija != filterKategorija.Value)
                    continue;

                if (samoKriticne && !StavkaInventaraService.JeKriticna(s))
                    continue;

                double vrijednost = s.Cijena * s.Kolicina;
                if (vrijednost > 1000) vrijednost *= 0.95;

                izvjestaj.Stavke.Add(s);
                izvjestaj.UkupnaVrijednost += vrijednost;
            }
        }*/
        /*public static void Generisi(Izvjestaj izvjestaj, List<StavkaInventara> lista)
        {
            if (izvjestaj == null)
                throw new ArgumentNullException();

            if (lista == null || lista.Count == 0)
                return;

            izvjestaj.Stavke.Clear();
            izvjestaj.UkupnaVrijednost = 0;

            var pocetak = izvjestaj.PocetakPerioda;
            var kraj = izvjestaj.KrajPerioda;
            var filterKategorija = izvjestaj.FilterKategorija;
            var samoKriticne = izvjestaj.SamoKriticne;

            if (samoKriticne)
            {
                if (filterKategorija.HasValue)
                {
                    // Samo kritične + filter kategorija
                    foreach (var s in lista)
                    {
                        if (s == null) continue;
                        if (pocetak.HasValue && kraj.HasValue &&
                            (s.DatumNabavke < pocetak.Value || s.DatumNabavke > kraj.Value))
                            continue;
                        if (s.Kategorija != filterKategorija.Value) continue;
                        if (!StavkaInventaraService.JeKriticna(s)) continue;

                        double vrijednost = s.Cijena * s.Kolicina;
                        if (vrijednost > 1000) vrijednost *= 0.95;

                        izvjestaj.Stavke.Add(s);
                        izvjestaj.UkupnaVrijednost += vrijednost;
                    }
                }
                else
                {
                    // Samo kritične, bez filter kategorije
                    foreach (var s in lista)
                    {
                        if (s == null) continue;
                        if (pocetak.HasValue && kraj.HasValue &&
                            (s.DatumNabavke < pocetak.Value || s.DatumNabavke > kraj.Value))
                            continue;
                        if (!StavkaInventaraService.JeKriticna(s)) continue;

                        double vrijednost = s.Cijena * s.Kolicina;
                        if (vrijednost > 1000) vrijednost *= 0.95;

                        izvjestaj.Stavke.Add(s);
                        izvjestaj.UkupnaVrijednost += vrijednost;
                    }
                }
            }
            else
            {
                if (filterKategorija.HasValue)
                {
                    // Filter kategorija, ne kritične
                    foreach (var s in lista)
                    {
                        if (s == null) continue;
                        if (pocetak.HasValue && kraj.HasValue &&
                            (s.DatumNabavke < pocetak.Value || s.DatumNabavke > kraj.Value))
                            continue;
                        if (s.Kategorija != filterKategorija.Value) continue;

                        double vrijednost = s.Cijena * s.Kolicina;
                        if (vrijednost > 1000) vrijednost *= 0.95;

                        izvjestaj.Stavke.Add(s);
                        izvjestaj.UkupnaVrijednost += vrijednost;
                    }
                }
                else
                {
                    // Ni filter kategorija ni kritične
                    foreach (var s in lista)
                    {
                        if (s == null) continue;
                        if (pocetak.HasValue && kraj.HasValue &&
                            (s.DatumNabavke < pocetak.Value || s.DatumNabavke > kraj.Value))
                            continue;

                        double vrijednost = s.Cijena * s.Kolicina;
                        if (vrijednost > 1000) vrijednost *= 0.95;

                        izvjestaj.Stavke.Add(s);
                        izvjestaj.UkupnaVrijednost += vrijednost;
                    }
                }
            }
        }*/
        /*public static void Generisi(Izvjestaj izvjestaj, List<StavkaInventara> lista)
        {
            if (izvjestaj == null)
                throw new ArgumentNullException();
            if (lista == null || lista.Count == 0)
                return;

            izvjestaj.Stavke.Clear();
            izvjestaj.UkupnaVrijednost = 0;

            var pocetak = izvjestaj.PocetakPerioda;
            var kraj = izvjestaj.KrajPerioda;
            var filterKategorija = izvjestaj.FilterKategorija;
            var samoKriticne = izvjestaj.SamoKriticne;

            bool provjeraDatuma = pocetak.HasValue && kraj.HasValue;
            bool provjeraKategorija = filterKategorija.HasValue;

            // Loop unswitching + minimiziranje rada unutar petlje
            if (samoKriticne)
            {
                if (provjeraKategorija)
                {
                    foreach (var s in lista)
                    {
                        if (s == null) continue;
                        if (provjeraDatuma && (s.DatumNabavke < pocetak.Value || s.DatumNabavke > kraj.Value))
                            continue;
                        if (s.Kategorija != filterKategorija.Value) continue;
                        if (!StavkaInventaraService.JeKriticna(s)) continue;

                        float vrijednost = (float)(s.Cijena * s.Kolicina);
                        vrijednost = vrijednost > 1000f ? vrijednost * 0.95f : vrijednost;

                        izvjestaj.Stavke.Add(s);
                        izvjestaj.UkupnaVrijednost += vrijednost;
                    }
                }
                else
                {
                    foreach (var s in lista)
                    {
                        if (s == null) continue;
                        if (provjeraDatuma && (s.DatumNabavke < pocetak.Value || s.DatumNabavke > kraj.Value))
                            continue;
                        if (!StavkaInventaraService.JeKriticna(s)) continue;

                        float vrijednost = (float)(s.Cijena * s.Kolicina);
                        vrijednost = vrijednost > 1000f ? vrijednost * 0.95f : vrijednost;

                        izvjestaj.Stavke.Add(s);
                        izvjestaj.UkupnaVrijednost += vrijednost;
                    }
                }
            }
            else
            {
                if (provjeraKategorija)
                {
                    foreach (var s in lista)
                    {
                        if (s == null) continue;
                        if (provjeraDatuma && (s.DatumNabavke < pocetak.Value || s.DatumNabavke > kraj.Value))
                            continue;
                        if (s.Kategorija != filterKategorija.Value) continue;

                        float vrijednost = (float)(s.Cijena * s.Kolicina);
                        vrijednost = vrijednost > 1000f ? vrijednost * 0.95f : vrijednost;

                        izvjestaj.Stavke.Add(s);
                        izvjestaj.UkupnaVrijednost += vrijednost;
                    }
                }
                else
                {
                    foreach (var s in lista)
                    {
                        if (s == null) continue;
                        if (provjeraDatuma && (s.DatumNabavke < pocetak.Value || s.DatumNabavke > kraj.Value))
                            continue;

                        float vrijednost = (float)(s.Cijena * s.Kolicina);
                        vrijednost = vrijednost > 1000f ? vrijednost * 0.95f : vrijednost;

                        izvjestaj.Stavke.Add(s);
                        izvjestaj.UkupnaVrijednost += vrijednost;
                    }
                }
            }
        }*/
        const float POPUST_GRANICA = 1000f;
        const float POPUST_FAKTOR = 0.95f;

        public static void Generisi(Izvjestaj izvjestaj, List<StavkaInventara> lista)
        {
            if (izvjestaj == null) throw new ArgumentNullException();
            if (lista == null || lista.Count == 0) return;

            izvjestaj.Stavke.Clear();
            izvjestaj.UkupnaVrijednost = 0;

            var pocetak = izvjestaj.PocetakPerioda;
            var kraj = izvjestaj.KrajPerioda;
            var filterKategorija = izvjestaj.FilterKategorija;
            var samoKriticne = izvjestaj.SamoKriticne;

            foreach (var s in lista)
            {
                if (!JeValidnaStavka(s, pocetak, kraj, filterKategorija, samoKriticne))
                    continue;

                float vrijednost = (float)(s.Cijena * s.Kolicina);
                vrijednost = vrijednost > POPUST_GRANICA ? vrijednost * POPUST_FAKTOR : vrijednost;

                izvjestaj.Stavke.Add(s);
                izvjestaj.UkupnaVrijednost += vrijednost;
            }
        }

        private static bool JeValidnaStavka(
            StavkaInventara s,
            DateTime? pocetak,
            DateTime? kraj,
            Kategorija? filterKategorija,
            bool samoKriticne)
        {
            if (s == null) return false;
            if (pocetak.HasValue && kraj.HasValue && (s.DatumNabavke < pocetak.Value || s.DatumNabavke > kraj.Value))
                return false;
            if (filterKategorija.HasValue && s.Kategorija != filterKategorija.Value)
                return false;
            if (samoKriticne && !StavkaInventaraService.JeKriticna(s))
                return false;
            return true;
        }



    }
}
