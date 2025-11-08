using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantInventory
{
    internal class Inventar
    {
        private List<StavkaInventara> stavke;       

        public Inventar()
        {
            stavke = new List<StavkaInventara>();           
        }

        // Dodavanje stavke
        public void DodajStavku(StavkaInventara stavka)
        {
            if (stavka == null || string.IsNullOrWhiteSpace(stavka.Naziv))
            {
                Console.WriteLine("Greška: stavka ne može imati prazan naziv!");
                return;
            }

            if (stavke.Any(s => s.Naziv.Equals(stavka.Naziv, StringComparison.OrdinalIgnoreCase) &&
                        s.Dobavljac.Equals(stavka.Dobavljac, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($"Greška: stavka '{stavka.Naziv}' od dobavljača '{stavka.Dobavljac}' već postoji!");
                return;
            }

            try
            {
                if (stavka.MinKolicina < 0)
                    throw new ArgumentOutOfRangeException(nameof(stavka.MinKolicina), "Minimalna količina ne može biti negativna.");

                if (stavka.DatumNabavke > DateTime.Now)
                    throw new InvalidOperationException("Datum nabavke ne može biti u budućnosti!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška pri dodavanju stavke: {ex.Message}");
                return;
            }

            if (stavka.Cijena < 0)
            {
                Console.WriteLine("Upozorenje: cijena ne moze biti negativna!");
                return;
            }

            stavke.Add(stavka);
            Console.WriteLine($"Stavka '{stavka.Naziv}' uspješno dodana.");
        }


        // Uklanjanje stavke
        public void UkloniStavku(string naziv)
        {
            var s = stavke.FirstOrDefault(x => x.Naziv.Equals(naziv, StringComparison.OrdinalIgnoreCase));

            if (s != null)
            {
                stavke.Remove(s);

                // Grane po kategoriji
                if (s.Kategorija == Kategorija.Hrana || s.Kategorija == Kategorija.Pice)
                {
                    if (s.Kolicina == 0)
                        Console.WriteLine($"[HITNO] Stavka '{naziv}' uklonjena - nema na stanju!");
                    else
                        Console.WriteLine($"Stavka '{naziv}' uklonjena!");
                }
                else if (s.Kategorija == Kategorija.Posudje)
                {
                    if ((DateTime.Now - s.DatumNabavke).TotalDays > 30)
                        Console.WriteLine($"Posuđe '{naziv}' uklonjeno, dugo na skladištu!");
                    else
                        Console.WriteLine($"Posuđe '{naziv}' uklonjeno!");
                }
                else if (s.Kategorija == Kategorija.Namjestaj)
                {
                    if (s.Kolicina < s.MinKolicina)
                        Console.WriteLine($"Namještaj '{naziv}' - ispod minimalne količine!");
                    else
                        Console.WriteLine($"Namještaj '{naziv}' uklonjen!");
                }
                else
                {
                    Console.WriteLine($"Ostalo '{naziv}' uklonjeno.");
                }
            }
            else
            {
                // Različite poruke kada stavka nije pronađena
                if (string.IsNullOrWhiteSpace(naziv))
                    Console.WriteLine("Greška: naziv stavke nije unesen!");
                else if (naziv.Length < 3)
                    Console.WriteLine($"Greška: naziv '{naziv}' je prekratak!");
                else
                    Console.WriteLine($"Stavka '{naziv}' nije pronađena.");
            }
        }


        // Ažuriranje količine
        public void AzurirajStavku(string naziv, double novaKolicina)
        {
            var s = stavke.FirstOrDefault(x => x.Naziv.Equals(naziv, StringComparison.OrdinalIgnoreCase));

            if (s != null)
            {
                // Provjere nove količine
                if (novaKolicina < 0)
                {
                    Console.WriteLine("Greška: nova količina ne može biti negativna!");
                    return;
                }

                // Ažuriranje količine
                s.AzurirajKolicinu(novaKolicina);

                // Provjera kategorije
                if (s.Kategorija == Kategorija.Hrana || s.Kategorija == Kategorija.Pice)
                {
                    if (novaKolicina < s.MinKolicina)
                        Console.WriteLine($"[HITNO] Stavka '{naziv}' ispod minimalne količine!");
                }
                else if (s.Kategorija == Kategorija.Posudje)
                {
                    if (novaKolicina == 0)
                        Console.WriteLine($"Posuđe '{naziv}' potpuno nestalo!");
                }
                else
                {
                    if ((DateTime.Now - s.DatumNabavke).TotalDays > 30)
                        Console.WriteLine($"Upozorenje: stavka '{naziv}' dugo je na skladištu!");
                }               
            }
            else
            {
                // Kada stavka nije pronađena
                if (string.IsNullOrWhiteSpace(naziv))
                    Console.WriteLine("Greška: naziv stavke nije unesen!");
                else if (naziv.Length < 3)
                    Console.WriteLine($"Greška: naziv '{naziv}' je prekratak!");
                else
                    Console.WriteLine($"Stavka '{naziv}' nije pronađena.");
            }
        }


        // Provjera kritičnih zaliha
        public List<StavkaInventara> ProvjeriMinimalneZalihe()
        {
            var kriticne = stavke.Where(s => s.JeKriticna()).ToList();

            if (kriticne.Count == 0)
            {
                Console.WriteLine("Sve stavke su iznad minimalne količine.");
                return kriticne;
            }

            Console.WriteLine("\n===== Kritične stavke =====");

            foreach (var s in kriticne)
            {
                // Provjera po kategorijama
                if (s.Kategorija == Kategorija.Hrana || s.Kategorija == Kategorija.Pice)
                {
                    if (s.Kolicina < s.MinKolicina && s.Kolicina!=0)                
                        Console.WriteLine($"{s.Naziv} ({s.Kolicina}/{s.MinKolicina}) - kritična, ali količina nije alarmantna");
                    else if (s.Kolicina==0)
                        Console.WriteLine($"[HITNO] {s.Naziv} ({s.Kolicina}/{s.MinKolicina}) - nema na stanju!");
                }
                else if (s.Kategorija == Kategorija.Posudje)
                {
                    if (s.Kolicina == 0)
                        Console.WriteLine($"{s.Naziv} - potpuno nestalo!");
                    else
                        Console.WriteLine($"{s.Naziv} ({s.Kolicina}/{s.MinKolicina}) - potrebno nadopuniti");
                }
                else
                {
                    if ((DateTime.Now - s.DatumNabavke).TotalDays > 30)
                        Console.WriteLine($"{s.Naziv} ({s.Kolicina}/{s.MinKolicina}) - kritično i dugo je na skladištu");
                    else
                        Console.WriteLine($"{s.Naziv} ({s.Kolicina}/{s.MinKolicina}) - pratiti stanje");
                }            
            }

            return kriticne;
        }


        // Pretraga pomoću filtera
        public List<StavkaInventara> Pretrazi(Filter filter)
        {
            if (filter == null)
            {
                Console.WriteLine("Greška: filter nije zadan!");
                throw new ArgumentNullException(nameof(filter));
            }

            if (filter.MinKolicina.HasValue && filter.MaxKolicina.HasValue &&
                filter.MinKolicina.Value > filter.MaxKolicina.Value)
            {
                Console.WriteLine("Upozorenje: minimalna količina veća od maksimalne!");
            }

            if (filter.MinCijena.HasValue && filter.MaxCijena.HasValue &&
                filter.MinCijena.Value > filter.MaxCijena.Value)
            {
                Console.WriteLine("Upozorenje: minimalna cijena veća od maksimalne!");
            }

            var rezultat = filter.Primijeni(stavke);

            if (!rezultat.Any())
            {
                Console.WriteLine("Nema stavki koje odgovaraju filteru.");              
            }
            else
            {
                Console.WriteLine("\nRezultati pretrage:");
                foreach (var s in rezultat)
                {
                    Console.WriteLine($"- {s.Naziv}, {s.Kolicina} kom, {s.Cijena} KM");
                }
            }         

            return rezultat;
        }


        // Prikaz svih stavki
        public void PrikaziSveStavke(Filter filter = null)
        {
            List<StavkaInventara> listaZaPrikaz;

            if (filter != null)
            {
                try
                {
                    listaZaPrikaz = Pretrazi(filter); 
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Greška prilikom filtriranja: {ex.Message}");
                    listaZaPrikaz = new List<StavkaInventara>(); 
                }
            }
            else
            {
                listaZaPrikaz = stavke;
            }

            if (!listaZaPrikaz.Any())
            {
                Console.WriteLine("Nema stavki za prikaz.");
                return;
            }

            Console.WriteLine("\n===== Trenutni inventar =====");
            foreach (var s in listaZaPrikaz)
            {
                if (s.Kolicina == 0)
                    Console.WriteLine($"[NEMA NA STANJU] {s.Naziv}");
                else if (s.Kolicina < s.MinKolicina)
                    Console.WriteLine($"[KRITIČNO] {s.Naziv} ({s.Kolicina}/{s.MinKolicina})");
                else
                    Console.WriteLine($"- {s.Naziv}, {s.Kolicina} kom, {s.Cijena} KM");
            }
        }


        // Pristup svim stavkama (za izvještaje)
        public List<StavkaInventara> VratiSveStavke() => stavke;     
    }
}
