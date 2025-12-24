using RestaurantInventory.Models;
using RestaurantInventory.Services;
using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace RestaurantInventory
{
    using System.Collections;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    internal class Program
    {
        static void Main(string[] args)
        {
            
             Inventar inventar = new Inventar();
             var inventarService = new InventarService(inventar);

             while (true)
             {
                 Console.WriteLine();
                 Console.WriteLine("===========================================");
                 Console.WriteLine("           INVENTAR RESTORANA");
                 Console.WriteLine("===========================================");
                 Console.WriteLine("1. Dodaj stavku");
                 Console.WriteLine("2. Prikazi sve stavke");
                 Console.WriteLine("3. Provjeri minimalne zalihe");
                 Console.WriteLine("4. Pretrazi (Filter)");
                 Console.WriteLine("5. Ukloni stavku");
                 Console.WriteLine("6. Ažuriraj količinu stavke");
                 Console.WriteLine("7. Generiši izvještaj");
                 Console.WriteLine("0. Izlaz");
                 Console.WriteLine("-------------------------------------------");
                 Console.Write("Odaberi opciju: ");

                 string izbor = Console.ReadLine();
                 Console.WriteLine();

                 try
                 {
                     switch (izbor)
                     {
                         case "1":
                             DodajStavku(inventarService);
                             break;
                         case "2":
                             PrikazSaSortiranjem(inventar);
                             break;
                         case "3":
                             ProvjeriMinimalne(inventarService);
                             break;
                         case "4":
                             PretraziStavke(inventarService);
                             break;
                         case "5":
                             UkloniStavku(inventarService);
                             break;
                         case "6":
                             AzurirajStavku(inventarService);
                             break;
                         case "7":
                             GenerisiIzvjestaj(inventar);
                             break;
                         case "0":
                             Console.WriteLine("Izlaz iz programa...");
                             return;
                         default:
                             Console.WriteLine("Nepoznata opcija! Pokušaj ponovo.");
                             break;
                     }
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine($"Greška: {ex.Message}");
                 }
             }
         }

         static void DodajStavku(InventarService inventarService)
         {
             Console.WriteLine("-------------------------------------------");
             Console.WriteLine("          DODAVANJE NOVE STAVKE");
             Console.WriteLine("-------------------------------------------");

             Console.Write("Naziv: ");
             string naziv = Console.ReadLine();

             Console.Write("Opis: ");
             string opis = Console.ReadLine();

             Kategorija kategorija = OdaberiKategoriju();

             double kolicina = 0;
             double minKolicina = 0;
             bool validanUnos = false;

             while (!validanUnos)
             {
                 Console.Write("Količina: ");
                 string unosKolicina = Console.ReadLine();

                 Console.Write("Minimalna količina: ");
                 string unosMinKolicina = Console.ReadLine();

                 if (kategorija == Kategorija.Namjestaj || kategorija == Kategorija.Posudje || kategorija == Kategorija.SredstvaZaCiscenje)
                 {
                     if (int.TryParse(unosKolicina, out int kInt) && int.TryParse(unosMinKolicina, out int minInt))
                     {
                         kolicina = kInt;
                         minKolicina = minInt;
                         validanUnos = true;
                     }
                     else
                     {
                         Console.WriteLine("Unesite cijele brojeve!");
                     }
                 }
                 else
                 {
                     if (double.TryParse(unosKolicina, NumberStyles.Float, CultureInfo.InvariantCulture, out double kDouble) &&
                         double.TryParse(unosMinKolicina, NumberStyles.Float, CultureInfo.InvariantCulture, out double minDouble))
                     {
                         kolicina = kDouble;
                         minKolicina = minDouble;
                         validanUnos = true;
                     }
                     else
                     {
                         Console.WriteLine("Neispravan unos, unesite broj!");
                     }
                 }
             }

             Console.Write("Cijena (prazno = 0): ");
             string unos = Console.ReadLine();
             double cijena = string.IsNullOrWhiteSpace(unos) ? 0 : double.Parse(unos, CultureInfo.InvariantCulture);

             Console.Write("Dobavljač: ");
             string dobavljac = Console.ReadLine();

             var stavka = new StavkaInventara
             {
                 Naziv = naziv,
                 Opis = opis,
                 Kolicina = kolicina,
                 MinKolicina = minKolicina,
                 Cijena = cijena,
                 Dobavljac = dobavljac,
                 DatumNabavke = DateTime.Now,
                 Kategorija = kategorija
             };

             inventarService.DodajStavku(stavka);

             Console.WriteLine();
             Console.WriteLine("Stavka dodana.");
             Console.WriteLine("-------------------------------------------");
         }

         static Kategorija OdaberiKategoriju()
         {
             while (true)
             {
                 Console.WriteLine();
                 Console.WriteLine("Kategorije:");
                 Console.WriteLine("1. Hrana");
                 Console.WriteLine("2. Piće");
                 Console.WriteLine("3. Posuđe");
                 Console.WriteLine("4. Sredstva za čišćenje");
                 Console.WriteLine("5. Namještaj");
                 Console.WriteLine("6. Ostalo");
                 Console.Write("Odaberi broj kategorije (1-6): ");

                 if (int.TryParse(Console.ReadLine(), out int izbor))
                 {
                     return izbor switch
                     {
                         1 => Kategorija.Hrana,
                         2 => Kategorija.Pice,
                         3 => Kategorija.Posudje,
                         4 => Kategorija.SredstvaZaCiscenje,
                         5 => Kategorija.Namjestaj,
                         6 => Kategorija.Ostalo,
                         _ => throw new Exception("Neispravan izbor! Molim odaberite broj od 1 do 6.")
                     };
                 }
                 Console.WriteLine("Molim unesite broj!");
             }
         }

         static void PretraziStavke(InventarService inventarService)
         {
             Console.WriteLine("-------------------------------------------");
             Console.WriteLine("              PRETRAGA STAVKI");
             Console.WriteLine("-------------------------------------------");

             var filter = new Filter();

             Console.Write("Naziv (prazno za preskočiti): ");
             filter.Naziv = Console.ReadLine();

             Console.Write("Dobavljač (prazno za preskočiti): ");
             filter.Dobavljac = Console.ReadLine();

             Console.Write("Minimalna cijena (prazno za preskočiti): ");
             if (double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out double minCijena))
                 filter.MinCijena = minCijena;

             Console.Write("Maksimalna cijena (prazno za preskočiti): ");
             if (double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out double maxCijena))
                 filter.MaxCijena = maxCijena;

             var rezultat = inventarService.Pretrazi(filter);
             IspisiStavke(rezultat);
         }

         static void UkloniStavku(InventarService inventarService)
         {
             Console.WriteLine("-------------------------------------------");
             Console.WriteLine("              UKLANJANJE STAVKE");
             Console.WriteLine("-------------------------------------------");

             Console.Write("Unesi naziv stavke za uklanjanje: ");
             string naziv = Console.ReadLine();
             inventarService.UkloniStavku(naziv);
             Console.WriteLine("Ako je stavka postojala, uklonjena je.");
         }

         static void AzurirajStavku(InventarService inventarService)
         {
             Console.WriteLine("-------------------------------------------");
             Console.WriteLine("             AŽURIRANJE STAVKE");
             Console.WriteLine("-------------------------------------------");

             Console.Write("Unesi naziv stavke za ažuriranje: ");
             string naziv = Console.ReadLine();

             var stavka = inventarService.Pretrazi(new Filter(naziv: naziv)).FirstOrDefault();
             if (stavka == null)
             {
                 Console.WriteLine($"Stavka '{naziv}' nije pronađena.");
                 return;
             }

             double novaKolicina = 0;
             bool validanUnos = false;

             while (!validanUnos)
             {
                 Console.Write("Unesi novu količinu: ");
                 string unos = Console.ReadLine();

                 if (stavka.Kategorija == Kategorija.Namjestaj || stavka.Kategorija == Kategorija.Posudje || stavka.Kategorija == Kategorija.SredstvaZaCiscenje)
                 {
                     if (int.TryParse(unos, out int kInt))
                     {
                         novaKolicina = kInt;
                         validanUnos = true;
                     }
                     else
                         Console.WriteLine("Ova kategorija mora imati cijeli broj!");
                 }
                 else
                 {
                     if (double.TryParse(unos, NumberStyles.Float, CultureInfo.InvariantCulture, out double kDouble))
                     {
                         novaKolicina = kDouble;
                         validanUnos = true;
                     }
                     else
                         Console.WriteLine("Neispravan unos, unesite broj!");
                 }
             }

             inventarService.AzurirajStavku(naziv, novaKolicina);
             Console.WriteLine("Količina ažurirana.");
         }

         static void GenerisiIzvjestaj(Inventar inventar)
         {
             Console.WriteLine("-------------------------------------------");
             Console.WriteLine("          GENERISANJE IZVJEŠTAJA");
             Console.WriteLine("-------------------------------------------");

             Console.Write("Naziv izvještaja: ");
             string tip = Console.ReadLine();

             var izvjestaj = new Izvjestaj(tip);

             Console.Write("Unesi početni datum (yyyy-MM-dd) ili prazno: ");
             if (DateTime.TryParse(Console.ReadLine(), out DateTime dtPocetak))
                 izvjestaj.PocetakPerioda = dtPocetak;

             Console.Write("Unesi krajnji datum (yyyy-MM-dd) ili prazno: ");
             if (DateTime.TryParse(Console.ReadLine(), out DateTime dtKraj))
                 izvjestaj.KrajPerioda = dtKraj;

             Console.Write("Unesi kategoriju ili prazno: ");
             string kat = Console.ReadLine();
             if (!string.IsNullOrWhiteSpace(kat) && Enum.TryParse<Kategorija>(kat, out Kategorija k))
                 izvjestaj.FilterKategorija = k;

             var sveStavke = inventar.Stavke.ToList(); // sve stavke
             IzvjestajService.Generisi(izvjestaj, sveStavke);

             // ispiši izvještaj
             Console.WriteLine();
             Console.WriteLine($"IZVJEŠTAJ: {izvjestaj.Tip}");
             Console.WriteLine($"Period: {izvjestaj.PocetakPerioda?.ToString("yyyy-MM-dd") ?? "početak"} - {izvjestaj.KrajPerioda?.ToString("yyyy-MM-dd") ?? "kraj"}");
             Console.WriteLine($"Broj stavki: {izvjestaj.Stavke.Count}");
             Console.WriteLine($"Ukupna vrijednost: {izvjestaj.UkupnaVrijednost}");
             Console.WriteLine("-------------------------------------------");
             IspisiStavke(izvjestaj.Stavke);
         }

         static void PrikazSaSortiranjem(Inventar inventar)
         {
             Console.WriteLine("Izaberite kriterij sortiranja:");
             Console.WriteLine("1. Cijena");
             Console.WriteLine("2. Količina");
             string kriterijUnos = Console.ReadLine();

             SortKriterij kriterij = kriterijUnos switch
             {
                 "1" => SortKriterij.Cijena,
                 "2" => SortKriterij.Kolicina,
                 _ => SortKriterij.Cijena
             };

             Console.WriteLine("Izaberite redoslijed sortiranja:");
             Console.WriteLine("1. Rastuće");
             Console.WriteLine("2. Padajuće");
             string redoslijedUnos = Console.ReadLine();

             SortRedoslijed redoslijed = redoslijedUnos == "2" ? SortRedoslijed.Padajuće : SortRedoslijed.Rastuće;

             IEnumerable<StavkaInventara> lista = inventar.Stavke;

             if (kriterij == SortKriterij.Cijena)
                 lista = redoslijed == SortRedoslijed.Rastuće ? lista.OrderBy(s => s.Cijena) : lista.OrderByDescending(s => s.Cijena);
             else
                 lista = redoslijed == SortRedoslijed.Rastuće ? lista.OrderBy(s => s.Kolicina) : lista.OrderByDescending(s => s.Kolicina);

             IspisiStavke(lista.ToList());
         }

         static void ProvjeriMinimalne(InventarService inventarService)
         {
             var kriticne = inventarService.ProvjeriMinimalneZalihe();
             if (kriticne == null || kriticne.Count == 0)
             {
                 Console.WriteLine("Nema kritičnih stavki.");
                 return;
             }

             Console.WriteLine("Kritične stavke:");
             IspisiStavke(kriticne);
         }

         static void IspisiStavke(List<StavkaInventara> stavke)
         {
             if (stavke == null || stavke.Count == 0)
             {
                 Console.WriteLine("Nema stavki za prikaz.");
                 return;
             }

             Console.WriteLine("-------------------------------------------");
             foreach (var s in stavke)
             {
                 Console.WriteLine($"Naziv: {s.Naziv} | Opis: {s.Opis} | Količina: {s.Kolicina} | Min: {s.MinKolicina} | Cijena: {s.Cijena} | Dobavljač: {s.Dobavljac} | Datum: {s.DatumNabavke:yyyy-MM-dd} | Kategorija: {s.Kategorija}");
             }
             Console.WriteLine("-------------------------------------------");
        }
    }
        
}