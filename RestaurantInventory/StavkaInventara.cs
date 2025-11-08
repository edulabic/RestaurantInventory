using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInventory
{
    internal class StavkaInventara
    {
        public String Naziv { get; set; }
        public double Cijena { get; set; }
        public double Kolicina { get; set; }
        public string Dobavljac { get; set; }
        public DateTime DatumNabavke { get; set; }
        public double MinKolicina { get; set; }
        public Kategorija Kategorija { get; set; }

        public StavkaInventara() { }
        public StavkaInventara(string naziv, double cijena, double kolicina, string dobavljac, DateTime datumNabavke, int minKolicina, Kategorija kategorija)
        {
            Naziv = naziv;
            Cijena = cijena;
            Kolicina = kolicina;
            Dobavljac = dobavljac;
            DatumNabavke = datumNabavke;
            MinKolicina = minKolicina;
            Kategorija = kategorija;
        }

        public bool JeKriticna()
        {          
            if (Kolicina < MinKolicina)
            {              
                if (DatumNabavke < DateTime.Now.AddDays(-30))
                {
                    if (Kategorija == Kategorija.Hrana || Kategorija == Kategorija.Pice)
                    {
                        Console.WriteLine($"[HRANA/PIĆE] '{Naziv}' kritično niska količina i stara nabavka!");
                        return true;
                    }
                    else if (Kategorija == Kategorija.Posudje)
                    {
                        Console.WriteLine($"[POSUĐE] '{Naziv}' kritično niska količina i dugo na skladištu!");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"[OSTALO] '{Naziv}' kritično niska količina i staro na skladištu!");
                        return true;
                    }
                }

                // Ako datum nije star, provjeri posebne kategorije
                if (Kategorija == Kategorija.Hrana || Kategorija == Kategorija.Pice)
                {
                    if (Kolicina == 0)
                    {
                        Console.WriteLine($"[HITNO] '{Naziv}' nema na stanju!");
                        return true;
                    }
                    else
                    {
                        return true;
                    }
                }

                return true;
            }
            else if ((DateTime.Now - DatumNabavke).TotalDays > 60)
            {
                // Stara nabavka, možda se pokvarilo
                Console.WriteLine($"'{Naziv}' dugo je na skladištu (>60 dana)!");
                return false;
            }
            else
            {                
                return false;
            }
        }


        // Metoda za ažuriranje količine       
        public void AzurirajKolicinu(double novaKolicina)
        {
            try
            {
                if (novaKolicina < 0)
                    throw new ArgumentException("Količina ne može biti negativna");

                if (novaKolicina == 0)
                    Console.WriteLine($"Upozorenje: količina '{Naziv}' postavljena na 0!");
                else if (novaKolicina > 1000)
                    Console.WriteLine($"Napomena: velika količina ({novaKolicina}) za '{Naziv}'");
              
                Kolicina = novaKolicina;

                // Provjera kritičnosti
                if (JeKriticna())
                {                     
                    if (Kategorija == Kategorija.Hrana || Kategorija == Kategorija.Pice)
                    {
                        if (Kolicina == 0)
                            Console.WriteLine($"[HITNO] '{Naziv}' nema na stanju!");
                    }
                }
                else if (Kolicina < MinKolicina * 1.5)
                {
                    Console.WriteLine($"'{Naziv}' količina je blizu kritične.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška pri ažuriranju količine '{Naziv}': {ex.Message}");
                throw;
            }
        }

        // Metoda za ispis informacija
        public void PrikaziInfo()
        {
            Console.WriteLine($"Naziv: {Naziv}, Količina: {Kolicina}, Min: {MinKolicina}, Kritična: {JeKriticna()}");
        }



    }
}
