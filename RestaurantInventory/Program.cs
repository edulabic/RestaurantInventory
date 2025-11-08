using System;
using System.Linq;

namespace RestaurantInventory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Inventar inventar = new Inventar();

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

                switch (izbor)
                {
                    case "1":
                        DodajStavku(inventar);
                        break;

                    case "2":
                        inventar.PrikaziSveStavke();
                        break;

                    case "3":
                        inventar.ProvjeriMinimalneZalihe();
                        break;

                    case "4":
                        PretraziStavke(inventar);
                        break;

                    case "5":
                        UkloniStavku(inventar);
                        break;

                    case "6":
                        AzurirajStavku(inventar);
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
        }

        static void DodajStavku(Inventar inventar)
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


            Console.Write("Cijena (prazno = 0): ");
            string unos = Console.ReadLine();

            double cijena;

            if (string.IsNullOrWhiteSpace(unos))
            {
                cijena = 0;
            }
            else
            {
                cijena = double.Parse(unos);
            }


            Console.Write("Dobavljač: ");
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

            inventar.DodajStavku(stavka);

            Console.WriteLine();         
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

        static void PretraziStavke(Inventar inventar)
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
            if (double.TryParse(Console.ReadLine(), out double minCijena))
                filter.MinCijena = minCijena;

            Console.Write("Maksimalna cijena (prazno za preskočiti): ");
            if (double.TryParse(Console.ReadLine(), out double maxCijena))
                filter.MaxCijena = maxCijena;

            inventar.Pretrazi(filter);
        }

        static void UkloniStavku(Inventar inventar)
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("              UKLANJANJE STAVKE");
            Console.WriteLine("-------------------------------------------");

            Console.Write("Unesi naziv stavke za uklanjanje: ");
            string naziv = Console.ReadLine();
            inventar.UkloniStavku(naziv);
        }

        static void AzurirajStavku(Inventar inventar)
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("             AŽURIRANJE STAVKE");
            Console.WriteLine("-------------------------------------------");

            Console.Write("Unesi naziv stavke za ažuriranje: ");
            string naziv = Console.ReadLine();

            var stavka = inventar.Pretrazi(new Filter(naziv: naziv)).FirstOrDefault();
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

            inventar.AzurirajStavku(naziv, novaKolicina);         
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

            var sveStavke = inventar.Pretrazi(new Filter());
            izvjestaj.Generisi(sveStavke);
            izvjestaj.Prikazi();
        }
    }
}
