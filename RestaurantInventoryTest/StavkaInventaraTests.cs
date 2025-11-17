using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantInventory.Models;
using System;

namespace RestaurantInventoryTest
{
    [TestClass]
    public class StavkaInventaraTests
    {
        [TestMethod]
        public void Konstruktor_PopunjavaSvaPolja()
        {
            var stavka = new StavkaInventara("Mlijeko", 2.5, 10, "DobavljacA", DateTime.Today, 5, Kategorija.Hrana);

            Assert.AreEqual("Mlijeko", stavka.Naziv);
            Assert.AreEqual(2.5, stavka.Cijena);
            Assert.AreEqual(10, stavka.Kolicina);
            Assert.AreEqual("DobavljacA", stavka.Dobavljac);
            Assert.AreEqual(DateTime.Today, stavka.DatumNabavke);
            Assert.AreEqual(5, stavka.MinKolicina);
            Assert.AreEqual(Kategorija.Hrana, stavka.Kategorija);
        }


        [DataTestMethod]
        [DataRow("Sir", 3.5, 15)]
        [DataRow("Voda", 1.0, 50)]
        [DataRow("Kafa", 5.0, 5)]
        public void Konstruktor_RazlicitiPodaci_PopunjavaPolja(string naziv, double cijena, double kolicina)
        {
            var stavka = new StavkaInventara(naziv, cijena, kolicina, "DobavljacB", DateTime.Today, 2, Kategorija.Pice);

            Assert.AreEqual(naziv, stavka.Naziv);
            Assert.AreEqual(cijena, stavka.Cijena);
            Assert.AreEqual(kolicina, stavka.Kolicina);
        }
    }
}
