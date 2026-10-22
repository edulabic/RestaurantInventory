using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantInventory.Models;
using RestaurantInventory.Services;
using System;

[TestClass]
public class InventarService_ObuhvatLinija_Condition
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

    // TC1
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Pretrazi_FilterNull_BacaIzuzetak()
    {
        service.Pretrazi(null);
    }

    // TC2
    [TestMethod]
    public void Pretrazi_BezFiltera_VracaSveStavke()
    {
        var rezultat = service.Pretrazi(new Filter());
        Assert.AreEqual(2, rezultat.Count);
    }

    // TC3
    [TestMethod]
    public void Pretrazi_NazivNeOdgovara_StavkaSeNeDodaje()
    {
        var filter = new Filter(naziv: "Nepostojece");
        var rezultat = service.Pretrazi(filter);
        Assert.AreEqual(0, rezultat.Count);
    }

    // TC4
    [TestMethod]
    public void Pretrazi_KategorijaNeOdgovara()
    {
        var filter = new Filter
        {
            Kategorija = Kategorija.Namjestaj
        };

        var rezultat = service.Pretrazi(filter);

        Assert.AreEqual(0, rezultat.Count);
    }

    // TC5
    [TestMethod]
    public void Pretrazi_MinKolicina_Filtrira()
    {
        var filter = new Filter(minKolicina: 15);
        var rezultat = service.Pretrazi(filter);
        Assert.AreEqual(1, rezultat.Count);
    }

    // TC6
    [TestMethod]
    public void Pretrazi_MaxCijena_Filtrira()
    {
        var filter = new Filter(maxCijena: 2);
        var rezultat = service.Pretrazi(filter);
        Assert.AreEqual(1, rezultat.Count);
    }
  
    // TC7 
    [TestMethod]
    public void Pretrazi_Dobavljac_TrueFalse()
    {
        var filterTrue = new Filter(dobavljac: "DobavljacA");
        var rezultatTrue = service.Pretrazi(filterTrue);
        Assert.AreEqual(1, rezultatTrue.Count); 

        var filterFalse = new Filter(dobavljac: "Nepostojece");
        var rezultatFalse = service.Pretrazi(filterFalse);
        Assert.AreEqual(0, rezultatFalse.Count); 
    }

    // TC8
    [TestMethod]
    public void Pretrazi_MaxKolicina_TrueFalse()
    {
        var filterTrue = new Filter(maxKolicina: 10);
        var rezultatTrue = service.Pretrazi(filterTrue);
        Assert.AreEqual(1, rezultatTrue.Count);

        var filterFalse = new Filter(maxKolicina: 1);
        var rezultatFalse = service.Pretrazi(filterFalse);
        Assert.AreEqual(0, rezultatFalse.Count);
    }

    // TC9 
    [TestMethod]
    public void Pretrazi_MinCijena_TrueFalse()
    {
        var filterTrue = new Filter(minCijena: 2); 
        var rezultatTrue = service.Pretrazi(filterTrue);
        Assert.AreEqual(2, rezultatTrue.Count); 

        var filterFalse = new Filter(minCijena: 5); 
        var rezultatFalse = service.Pretrazi(filterFalse);
        Assert.AreEqual(0, rezultatFalse.Count);
    }

    // TC10
    [TestMethod]
    public void Pretrazi_Dodaj_True()
    {       
        var filter = new Filter(); 
        var rezultat = service.Pretrazi(filter);
      
        Assert.AreEqual(2, rezultat.Count);
    } 
}
