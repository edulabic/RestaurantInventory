using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInventory
{
    internal class Inventar
    {
        private List<StavkaInventara> listaStavki;

        public Inventar()
        {
            listaStavki = new List<StavkaInventara>();
        }


        public void DodajStavku(StavkaInventara stavka)
        {
            try
            {
                if (stavka == null)
                    throw new ArgumentNullException("Stavka ne može biti null.");

                listaStavki.Add(stavka);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška pri dodavanju stavke: {ex.Message}");
            }
        }

        public void UkloniStavku(StavkaInventara stavka)
        {
            listaStavki.Remove(stavka);
        }

        public List<StavkaInventara> Pretrazi(Filter filter)
        {
            return filter.Primijeni(listaStavki);
        }


        public void GenerisiIzvjestaj(string tipIzvjestaja, (DateTime Pocetak, DateTime Kraj)? period = null)
        {
            Console.WriteLine($"--- IZVJEŠTAJ: {tipIzvjestaja} ---");

            foreach (var s in listaStavki)
            {
                if (period == null ||
                    (s.DatumNabavke >= period.Value.Pocetak && s.DatumNabavke <= period.Value.Kraj))
                {
                    s.PrikaziInfo();
                }
            }
        }

        // Vraća listu stavki ispod minimalne količine
        public List<StavkaInventara> ProvjeriMinimalneZalihe()
        {
            return listaStavki.Where(s => s.JeKriticna()).ToList();
        }

        // (opcionalno) metoda za prikaz svih stavki
        public void PrikaziSve()
        {
            Console.WriteLine("\n--- Trenutni inventar ---");
            foreach (var s in listaStavki)
                s.PrikaziInfo();
        }
    }
}
