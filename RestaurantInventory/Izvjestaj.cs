using RestaurantInventory;
using System;
using System.Collections.Generic;
using System.Linq;

internal class Izvjestaj
{
    public string Tip { get; set; }
    public List<StavkaInventara> Stavke { get; private set; }
    public double UkupnaVrijednost { get; private set; }
    public DateTime? PocetakPerioda { get; set; }
    public DateTime? KrajPerioda { get; set; }
    public Kategorija? FilterKategorija { get; set; }
    public bool SamoKriticne { get; set; } = false;

    public Izvjestaj(string tip)
    {
        Tip = tip;
        Stavke = new List<StavkaInventara>();
        UkupnaVrijednost = 0;
    }

    public void Generisi(List<StavkaInventara> lista)
    {
        if (lista == null) throw new ArgumentNullException(nameof(lista));
        Stavke.Clear();
        UkupnaVrijednost = 0;

        foreach (var s in lista)
        {
            if (PocetakPerioda.HasValue && KrajPerioda.HasValue &&
                (s.DatumNabavke < PocetakPerioda.Value || s.DatumNabavke > KrajPerioda.Value))
                continue;

            if (FilterKategorija.HasValue && s.Kategorija != FilterKategorija.Value)
                continue;

            if (SamoKriticne && !s.JeKriticna())
                continue;

            Stavke.Add(s);
            UkupnaVrijednost += s.Cijena * s.Kolicina;
        }
    }

    public void Prikazi()
    {
        Console.WriteLine($"\n===== IZVJEŠTAJ: {Tip} =====");

        if (PocetakPerioda.HasValue && KrajPerioda.HasValue)
            Console.WriteLine($"Period: {PocetakPerioda.Value.ToShortDateString()} - {KrajPerioda.Value.ToShortDateString()}");

        if (FilterKategorija.HasValue)
            Console.WriteLine($"Kategorija: {FilterKategorija.Value}");

        Console.WriteLine($"Ukupna vrijednost: {UkupnaVrijednost:F2} KM");

        if (!Stavke.Any())
        {
            Console.WriteLine("Nema stavki u izvještaju.");
            return;
        }

        foreach (var s in Stavke)
        {
            string oznaka = s.JeKriticna() ? "[KRITIČNO]" : "";

            if (s.Kolicina == 0)
                oznaka += " [NEMA NA STANJU]";
            else if (s.Kolicina < s.MinKolicina)
                oznaka += " [NISKA KOLIČINA]";
            Console.WriteLine($"{oznaka} {s.Naziv} - {s.Kolicina} kom, {s.Cijena} KM");
        }

        Console.WriteLine($"\nUkupno stavki: {Stavke.Count}");
        Console.WriteLine($"Kritične stavke: {Stavke.Count(s => s.JeKriticna())}");
       

        var kriticne = Stavke.Where(s => s.JeKriticna()).ToList();
        if (kriticne.Count > 0)
        {
            Console.WriteLine("Lista kritičnih stavki:");
            foreach (var s in kriticne)
                Console.WriteLine($"- {s.Naziv} ({s.Kolicina} kom)");
        }
    }

}
