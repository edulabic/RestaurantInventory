using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantInventory.Models;
using RestaurantInventory.Services;
using System;

namespace RestaurantInventoryTest
{
    [TestClass]
    public class StavkaInventaraServiceTests
    {
        [TestMethod]
        public void JeKriticna_QuantityLow_And_OldDate_Is_Critical()
        {
            var s = new StavkaInventara
            {
                Kolicina = 2,
                MinKolicina = 5,
                Kategorija = Kategorija.Hrana,
                DatumNabavke = DateTime.Now.AddDays(-40)
            };

            var rezultat = StavkaInventaraService.JeKriticna(s);

            Assert.IsTrue(rezultat);
        }

        [TestMethod]
        public void JeKriticna_TooOld_ReturnsTrue()
        {
            var s = new StavkaInventara
            {
                Kolicina = 50,
                MinKolicina = 5,
                DatumNabavke = DateTime.Now.AddDays(-100)
            };

            var rezultat = StavkaInventaraService.JeKriticna(s);

            Assert.IsTrue(rezultat);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AzurirajKolicinu_Negative_Throws()
        {
            var s = new StavkaInventara();

            StavkaInventaraService.AzurirajKolicinu(s, -1);

            // Assert se očekuje od ExpectedException
        }

        [TestMethod]
        public void AzurirajKolicinu_Valid_Updates()
        {
            var s = new StavkaInventara { Kolicina = 10 };

            StavkaInventaraService.AzurirajKolicinu(s, 25);

            Assert.AreEqual(25, s.Kolicina);
        }

        [TestMethod]
        public void JeKriticna_NotCritical_ReturnsFalse()
        {
            var s = new StavkaInventara
            {
                Kolicina = 10,
                MinKolicina = 5,
                DatumNabavke = DateTime.Now.AddDays(-5)
            };

            var rezultat = StavkaInventaraService.JeKriticna(s);

            Assert.IsFalse(rezultat);
        }

        [DataTestMethod]
        [DataRow(1, 5, -40, true)]   
        [DataRow(10, 5, -5, false)]  
        [DataRow(50, 5, -100, true)] 
        [DataRow(5, 5, -1, false)]   
        public void JeKriticna_DataDriven(double kolicina, double min, int daysOld, bool expected)
        {
            var s = new StavkaInventara
            {
                Kolicina = kolicina,
                MinKolicina = min,
                DatumNabavke = DateTime.Now.AddDays(daysOld)
            };

            var rezultat = StavkaInventaraService.JeKriticna(s);

            Assert.AreEqual(expected, rezultat);
        }

        [TestMethod]
        public void AzurirajKolicinu_UsesDummyObject_NoExceptions()
        {
            var dummy = new StavkaInventara();

            StavkaInventaraService.AzurirajKolicinu(dummy, 1);

            Assert.AreEqual(1, dummy.Kolicina);
        }

        [DataTestMethod]
        [DataRow(10, 5, 5)]     
        [DataRow(10, 20, 20)]   
        [DataRow(0, 0, 0)]      
        public void AzurirajKolicinu_DataDriven(double pocetna, double nova, double expected)
        {
            var s = new StavkaInventara { Kolicina = pocetna };

            StavkaInventaraService.AzurirajKolicinu(s, nova);

            Assert.AreEqual(expected, s.Kolicina);
        }

    }
}
