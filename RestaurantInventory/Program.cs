using System;
using System.Linq;

namespace RestaurantInventory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IInventarService inventarService = new InventarService();

            while (true)
            {
                Console.WriteLine("\n===== INVENTAR RESTORANA =====");
                Console.WriteLine("1. Dodaj stavku");
                Console.WriteLine("2. Prikazi sve stavke");
                Console.WriteLine("3. Provjeri minimalne zalihe");
                Console.WriteLine("4. Pretrazi (Filter)");
                Console.WriteLine("5. Ukloni stavku");
                Console.WriteLine("6. Ažuriraj količinu stavke");
                Console.WriteLine("7. Generiši izvještaj");
                Console.WriteLine("0. Izlaz");
                Console.Write("Odaberi opciju: ");

                string izbor = Console.ReadLine();
                Console.WriteLine();

                switch (izbor)
                {
                    case "1":
                        DodajStavku(inventarService);
                        break;

                    case "2":
                        inventarService.PrikaziSveStavke();
                        break;

                    case "3":
                        inventarService.ProvjeriMinimalneZalihe();
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
                        GenerisiIzvjestaj(inventarService);
                        break;

                    case "0":
                        Console.WriteLine("Izlaz iz programa...");
                        return;

                    default:
                        Console.WriteLine("Nepoznata opcija!");
                        break;
                }
            }
        }

        static void DodajStavku(IInventarService inventarService)
        {
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
                Console.Write("Kolicina: ");
                string unosKolicina = Console.ReadLine();

                Console.Write("Minimalna kolicina: ");
                string unosMinKolicina = Console.ReadLine();

                if (kategorija == Kategorija.Namjestaj || kategorija==Kategorija.Posudje || kategorija==Kategorija.SredstvaZaCiscenje)
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
                    if (double.TryParse(unosKolicina, out double kDouble) && double.TryParse(unosMinKolicina, out double minDouble))
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

            Console.Write("Cijena: ");
            double cijena = double.Parse(Console.ReadLine());

            Console.Write("Dobavljac: ");
            string dobavljac = Console.ReadLine();

            var stavka = new StavkaInventara
            {
                Naziv = naziv,
                Kolicina = kolicina,
                MinKolicina = minKolicina,
                Cijena = cijena,
                Dobavljac = dobavljac,
                DatumNabavke = DateTime.Now,
                Kategorija = kategorija
            };

            inventarService.DodajStavku(stavka);
        }

        static Kategorija OdaberiKategoriju()
        {
            while (true)
            {
                Console.WriteLine("\nKategorije:");
                Console.WriteLine("1. Hrana");
                Console.WriteLine("2. Pice");
                Console.WriteLine("3. Posudje");
                Console.WriteLine("4. Sredstva za ciscenje");
                Console.WriteLine("5. Namjestaj");
                Console.WriteLine("6. Ostalo");
                Console.Write("Odaberi broj kategorije (1-6): ");

                if (int.TryParse(Console.ReadLine(), out int izbor))
                {
                    switch (izbor)
                    {
                        case 1: return Kategorija.Hrana;
                        case 2: return Kategorija.Pice;
                        case 3: return Kategorija.Posudje;
                        case 4: return Kategorija.SredstvaZaCiscenje;
                        case 5: return Kategorija.Namjestaj;
                        case 6: return Kategorija.Ostalo;
                        default:
                            Console.WriteLine("Neispravan izbor! Molim odaberite broj od 1 do 6.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Molim unesite broj!");
                }
            }
        }

        static void PretraziStavke(IInventarService inventarService)
        {
            var filter = new Filter();

            Console.Write("Naziv (prazno za preskociti): ");
            filter.Naziv = Console.ReadLine();

            Console.Write("Dobavljac (prazno za preskociti): ");
            filter.Dobavljac = Console.ReadLine();

            Console.Write("Minimalna cijena (prazno za preskociti): ");
            if (double.TryParse(Console.ReadLine(), out double minCijena))
                filter.MinCijena = minCijena;

            Console.Write("Maksimalna cijena (prazno za preskociti): ");
            if (double.TryParse(Console.ReadLine(), out double maxCijena))
                filter.MaxCijena = maxCijena;

            inventarService.Pretrazi(filter);
        }

        static void UkloniStavku(IInventarService inventarService)
        {
            Console.Write("Unesi naziv stavke za uklanjanje: ");
            string naziv = Console.ReadLine();
            inventarService.UkloniStavku(naziv);
        }

        static void AzurirajStavku(IInventarService inventarService)
        {
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

                if (stavka.Kategorija == Kategorija.Namjestaj)
                {
                    if (int.TryParse(unos, out int kInt))
                    {
                        novaKolicina = kInt;
                        validanUnos = true;
                    }
                    else
                    {
                        Console.WriteLine("Namještaj mora imati cijeli broj!");
                    }
                }
                else
                {
                    if (double.TryParse(unos, out double kDouble))
                    {
                        novaKolicina = kDouble;
                        validanUnos = true;
                    }
                    else
                    {
                        Console.WriteLine("Neispravan unos, unesite broj!");
                    }
                }
            }

            inventarService.AzurirajStavku(naziv, novaKolicina);
        }

        static void GenerisiIzvjestaj(IInventarService inventarService)
        {
            Console.Write("Naziv izvještaja: ");
            string tip = Console.ReadLine();

            var izvjestaj = new Izvjestaj(tip);

            Console.Write("Unesi početni datum (yyyy-MM-dd) ili prazno: ");
            string pocetak = Console.ReadLine();
            if (DateTime.TryParse(pocetak, out DateTime dtPocetak))
                izvjestaj.PocetakPerioda = dtPocetak;

            Console.Write("Unesi krajnji datum (yyyy-MM-dd) ili prazno: ");
            string kraj = Console.ReadLine();
            if (DateTime.TryParse(kraj, out DateTime dtKraj))
                izvjestaj.KrajPerioda = dtKraj;

            Console.Write("Unesi kategoriju ili prazno: ");
            string kat = Console.ReadLine();
            if (Enum.TryParse<Kategorija>(kat, out Kategorija k))
                izvjestaj.FilterKategorija = k;

            // Generisanje izvještaja na osnovu svih stavki
            var sveStavke = inventarService.Pretrazi(new Filter());
            izvjestaj.Generisi(sveStavke);
            izvjestaj.Prikazi();
        }

    }
}
