using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInventory
{
    internal class InventarService : IInventarService
    {
        private List<StavkaInventara> stavke = new List<StavkaInventara>();

        public void DodajStavku(StavkaInventara stavka)
        {
            try
            {
                if (stavka == null)
                    throw new ArgumentNullException(nameof(stavka));

                stavke.Add(stavka);
                Console.WriteLine($"Stavka '{stavka.Naziv}' uspješno dodana.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška pri dodavanju stavke: {ex.Message}");
            }
        }

        public void UkloniStavku(string naziv)
        {
            try
            {
                var s = stavke.FirstOrDefault(x => x.Naziv.Equals(naziv, StringComparison.OrdinalIgnoreCase));
                if (s != null)
                {
                    stavke.Remove(s);
                    Console.WriteLine($"Stavka '{naziv}' je uklonjena.");
                }
                else
                {
                    Console.WriteLine($"Stavka '{naziv}' nije pronađena.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška pri uklanjanju stavke: {ex.Message}");
            }
        }

        public void AzurirajStavku(string naziv, int novaKolicina)
        {
            try
            {
                var s = stavke.FirstOrDefault(x => x.Naziv.Equals(naziv, StringComparison.OrdinalIgnoreCase));
                if (s != null)
                {
                    s.AzurirajKolicinu(novaKolicina);
                    Console.WriteLine($"Količina za '{naziv}' ažurirana na {novaKolicina}.");
                }
                else
                {
                    Console.WriteLine($"Stavka '{naziv}' nije pronađena.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška pri ažuriranju stavke: {ex.Message}");
            }
        }

        public List<StavkaInventara> ProvjeriMinimalneZalihe()
        {
            try
            {
                var kriticne = stavke.Where(s => s.JeKriticna()).ToList();
                Console.WriteLine("Kritične stavke (ispod minimalne količine):");

                foreach (var s in kriticne)
                    Console.WriteLine($"- {s.Naziv} ({s.Kolicina}/{s.MinKolicina})");

                return kriticne;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška pri provjeri zaliha: {ex.Message}");
                return new List<StavkaInventara>();
            }
        }

        public List<StavkaInventara> Pretrazi(Filter filter)
        {
            try
            {
                var rezultat = filter.Primijeni(stavke);
                Console.WriteLine("Rezultati pretrage:");

                foreach (var s in rezultat)
                    Console.WriteLine($"- {s.Naziv}, {s.Kolicina} kom, {s.Cijena} KM");

                return rezultat;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška pri pretrazi: {ex.Message}");
                return new List<StavkaInventara>();
            }
        }

        public void PrikaziSveStavke()
        {
            if (stavke.Count == 0)
            {
                Console.WriteLine("Inventar je trenutno prazan.");
                return;
            }

            Console.WriteLine("\n===== Trenutni inventar =====");
            foreach (var s in stavke)
            {
                Console.WriteLine($"Naziv: {s.Naziv}, Kolicina: {s.Kolicina}, Cijena: {s.Cijena} KM, Kategorija: {s.Kategorija}");
            }
        }
    }
}
