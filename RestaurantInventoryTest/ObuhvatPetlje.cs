using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantInventory.Models;
using RestaurantInventory.Services;
using System;

[TestClass]
public class ObuhvatPetlje
{
    private Inventar inventar;
    private InventarService service;

    [TestInitialize]
    public void Setup()
    {
        inventar = new Inventar();
        service = new InventarService(inventar);

        inventar.Stavke.Add(new StavkaInventara
        {
            Naziv = "Brasno",
            Kategorija = Kategorija.Hrana,
            Dobavljac = "DobavljacA",
            Kolicina = 20,
            MinKolicina = 10,
            Cijena = 2
        });

        inventar.Stavke.Add(new StavkaInventara
        {
            Naziv = "Sok",
            Kategorija = Kategorija.Pice,
            Dobavljac = "DobavljacB",
            Kolicina = 5,
            MinKolicina = 10,
            Cijena = 3
        });
    }

    //Loop coverage
    [TestMethod]
    public void Pretrazi_PrazanInventar_PrazanRezultat() // 0 prolaza
    {
        inventar.Stavke.Clear();
        var filter = new Filter();

        var rezultat = service.Pretrazi(filter);
        Assert.AreEqual(0, rezultat.Count);
    }

    [TestMethod]
    public void Pretrazi_JednaStavka() // 1 prolaz
    {
        inventar.Stavke.Clear();
        inventar.Stavke.Add(new StavkaInventara
        {
            Naziv = "Mlijeko",
            Kategorija = Kategorija.Hrana,
            Dobavljac = "DobavljacC",
            Kolicina = 10,
            MinKolicina = 5,
            Cijena = 1
        });

        var filter = new Filter();
        var rezultat = service.Pretrazi(filter);
        Assert.AreEqual(1, rezultat.Count);
    }

    [TestMethod]
    public void Pretrazi_ViseStavki() // više prolaza
    {
        var filter = new Filter();
        var rezultat = service.Pretrazi(filter);
        Assert.AreEqual(2, rezultat.Count);
    }
}
