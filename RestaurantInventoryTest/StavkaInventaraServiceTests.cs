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
    }
}
