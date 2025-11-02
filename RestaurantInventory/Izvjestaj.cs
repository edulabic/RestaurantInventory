using RestaurantInventory;

internal class Izvjestaj
{
    public string Tip { get; set; }
    public List<StavkaInventara> Stavke { get; private set; }
    public double UkupnaVrijednost { get; private set; }
    public DateTime? PocetakPerioda { get; set; }
    public DateTime? KrajPerioda { get; set; }
    public Kategorija? FilterKategorija { get; set; }

    public Izvjestaj(string tip)
    {
        Tip = tip;
        Stavke = new List<StavkaInventara>();
        UkupnaVrijednost = 0;
    }

    public void Generisi(List<StavkaInventara> lista)
    {
        var query = lista.AsQueryable();

        if (PocetakPerioda.HasValue && KrajPerioda.HasValue)
            query = query.Where(s => s.DatumNabavke >= PocetakPerioda.Value && s.DatumNabavke <= KrajPerioda.Value);

        if (FilterKategorija.HasValue)
            query = query.Where(s => s.Kategorija == FilterKategorija.Value);

        Stavke = query.ToList();
        UkupnaVrijednost = Stavke.Sum(s => s.Cijena * s.Kolicina);
    }

    public void Prikazi()
    {
        Console.WriteLine($"\n===== IZVJEŠTAJ: {Tip} =====");
        if (PocetakPerioda.HasValue && KrajPerioda.HasValue)
            Console.WriteLine($"Period: {PocetakPerioda.Value.ToShortDateString()} - {KrajPerioda.Value.ToShortDateString()}");
        if (FilterKategorija.HasValue)
            Console.WriteLine($"Kategorija: {FilterKategorija.Value}");
        Console.WriteLine($"Ukupna vrijednost: {UkupnaVrijednost:F2} KM");
        Console.WriteLine("Stavke u izvještaju:");
        foreach (var s in Stavke)
            s.PrikaziInfo();
    }
}
