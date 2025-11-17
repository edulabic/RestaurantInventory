using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantInventory.Models;

namespace RestaurantInventory.Services
{
    public class InventarService
    {
        private readonly Inventar inventar;

        public InventarService(Inventar inv)
        {
            inventar = inv;
        }

        public void DodajStavku(StavkaInventara stavka)
        {
            if (stavka == null || string.IsNullOrWhiteSpace(stavka.Naziv))
                throw new ArgumentException("Stavka ne može biti prazna!");

            if (inventar.Stavke.Any(s => s.Naziv.Equals(stavka.Naziv, StringComparison.OrdinalIgnoreCase) &&
                                         s.Dobavljac.Equals(stavka.Dobavljac, StringComparison.OrdinalIgnoreCase)))
                throw new Exception($"Stavka '{stavka.Naziv}' od dobavljača '{stavka.Dobavljac}' već postoji.");

            inventar.Stavke.Add(stavka);
        }

        public void UkloniStavku(string naziv)
        {
            var s = inventar.Stavke.FirstOrDefault(x => x.Naziv.Equals(naziv, StringComparison.OrdinalIgnoreCase));
            if (s != null) inventar.Stavke.Remove(s);
        }

        public void AzurirajStavku(string naziv, double novaKolicina)
        {
            var s = inventar.Stavke.FirstOrDefault(x => x.Naziv.Equals(naziv, StringComparison.OrdinalIgnoreCase));
            if (s == null) throw new Exception($"Stavka '{naziv}' nije pronađena.");
            StavkaInventaraService.AzurirajKolicinu(s, novaKolicina);
        }

        public List<StavkaInventara> ProvjeriMinimalneZalihe()
        {
            return inventar.Stavke.Where(StavkaInventaraService.JeKriticna).ToList();
        }

        public List<StavkaInventara> Pretrazi(Filter filter)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            var rezultat = new List<StavkaInventara>();

            foreach (var s in inventar.Stavke)
            {
                bool dodaj = true;

                if (!string.IsNullOrEmpty(filter.Naziv) && !s.Naziv.Equals(filter.Naziv, StringComparison.OrdinalIgnoreCase))
                    dodaj = false;

                if (filter.Kategorija != Kategorija.None && s.Kategorija != filter.Kategorija)
                    dodaj = false;

                if (!string.IsNullOrEmpty(filter.Dobavljac) && !s.Dobavljac.Equals(filter.Dobavljac, StringComparison.OrdinalIgnoreCase))
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
        }

        // Dodana metoda za potrebe TDD-a
        public List<Narudzba> GenerisiNarudzbe()
        {
            var lista = new List<Narudzba>();

            foreach (var s in inventar.Stavke)
            {
                if (s.Kolicina < s.MinKolicina)
                {
                    lista.Add(new Narudzba
                    {
                        Naziv = s.Naziv,
                        Dobavljac = s.Dobavljac,
                        Nedostaje = s.MinKolicina - s.Kolicina
                    });
                }
            }

            return lista;
        }

    }
}
