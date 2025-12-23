using System;
using RestaurantInventory.Models;

namespace RestaurantInventory.Services
{
    public static class StavkaInventaraService
    {
        /*   public static bool JeKriticna(StavkaInventara stavka)
           {


               int rizik = 0;

               if (stavka.Kolicina < stavka.MinKolicina)
               {
                   rizik += 3;

                   if (stavka.Kolicina == 0)
                       rizik += 2;
                   else if (stavka.Kolicina < stavka.MinKolicina / 2)
                       rizik += 1;
               }
               else
               {
                   rizik -= 1;
               }

               double dani = (DateTime.Now - stavka.DatumNabavke).TotalDays;



               if (dani > 30)
                   rizik += 2;
               else if (dani > 60)
                   rizik += 3;



               switch (stavka.Kategorija)
               {
                   case Kategorija.Hrana:
                       rizik += 2;
                       break;

                   case Kategorija.Pice:
                       rizik += 1;
                       break;

                   default:
                       rizik += 0;
                       break;
               }

               bool jeHranaIliPice = stavka.Kategorija == Kategorija.Hrana
                      || stavka.Kategorija == Kategorija.Pice;

               bool ispodMinimuma = stavka.Kolicina < stavka.MinKolicina;
               bool starijaOd25 = dani > 25;

               if (jeHranaIliPice && ispodMinimuma && starijaOd25)
               {
                   rizik += 2;
               }


               if (rizik >= 5)
                   return true;

               return false;
           }
        */

        public static bool JeKriticna(StavkaInventara stavka)
        {
            int rizik = 0;

            rizik += IzracunajRizikKolicine(stavka);

            double dani = (DateTime.Now - stavka.DatumNabavke).TotalDays;

            rizik += IzracunajRizikStarosti(dani);
            rizik += IzracunajRizikKategorije(stavka.Kategorija);

            if (JePosebnoRizicna(stavka, dani))
                rizik += 2;

            return rizik >= 5;
        }
        private static int IzracunajRizikKolicine(StavkaInventara stavka)
        {
            if (stavka.Kolicina >= stavka.MinKolicina)
                return -1;

            if (stavka.Kolicina == 0)
                return 5;

            if (stavka.Kolicina < stavka.MinKolicina / 2)
                return 4;

            return 3;
        }
        private static int IzracunajRizikStarosti(double dani)
        {
            if (dani > 60)
                return 5;

            if (dani > 30)
                return 2;

            return 0;
        }
        private static int IzracunajRizikKategorije(Kategorija kategorija)
        {
            return kategorija switch
            {
                Kategorija.Hrana => 2,
                Kategorija.Pice => 1,
                _ => 0
            };
        }


        private static bool JePosebnoRizicna(StavkaInventara stavka, double dani)
        {
            return (stavka.Kategorija == Kategorija.Hrana ||
                    stavka.Kategorija == Kategorija.Pice)
                    && stavka.Kolicina < stavka.MinKolicina
                    && dani > 25;
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
