using System;
using RestaurantInventory.Models;

namespace RestaurantInventory.Services
{
    public static class StavkaInventaraService
    {
        public static bool JeKriticna(StavkaInventara stavka)
        {
            if (stavka.Kolicina < stavka.MinKolicina)
            {
                if (stavka.DatumNabavke < DateTime.Now.AddDays(-30))
                    return true;

                if (stavka.Kategorija == Kategorija.Hrana || stavka.Kategorija == Kategorija.Pice)
                    return true;

                return true;
            }

            if ((DateTime.Now - stavka.DatumNabavke).TotalDays > 60)
                return true;

            return false;
        }

        public static void AzurirajKolicinu(StavkaInventara stavka, double novaKolicina)
        {
            if (novaKolicina < 0)
                throw new ArgumentException("Količina ne može biti negativna.");

            stavka.Kolicina = novaKolicina;
        }

        //Dodana metoda za provjeru isteka hrane i pića - TDD
        public static bool JePredIstekom(StavkaInventara stavka)
        {
            if (stavka.Kategorija != Kategorija.Hrana &&
                stavka.Kategorija != Kategorija.Pice)
                return false;

            return (DateTime.Now - stavka.DatumNabavke).TotalDays > 25;
        }

    }
}
