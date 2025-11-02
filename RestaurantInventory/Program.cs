using System;

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

            Console.Write("Kolicina: ");
            int kolicina = int.Parse(Console.ReadLine());

            Console.Write("Minimalna kolicina: ");
            int minKolicina = int.Parse(Console.ReadLine());

            Console.Write("Cijena: ");
            double cijena = double.Parse(Console.ReadLine());

            Console.Write("Dobavljac: ");
            string dobavljac = Console.ReadLine();

            // Prikaz kategorija sa brojevima
            Kategorija kategorija = OdaberiKategoriju();

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
                Console.Write("Odaberi broj kategorije (1-5): ");

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
                            Console.WriteLine("Neispravan izbor! Molim odaberite broj od 1 do 5.");
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
                filter.MinCijena = (int)minCijena;

            Console.Write("Maksimalna cijena (prazno za preskociti): ");
            if (double.TryParse(Console.ReadLine(), out double maxCijena))
                filter.MaxCijena = (int)maxCijena;

            inventarService.Pretrazi(filter);
        }
    }
}
