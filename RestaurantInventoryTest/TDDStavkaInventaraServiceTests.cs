using RestaurantInventory.Models;
using RestaurantInventory.Services;
using System;

namespace RestaurantInventory.Tests
{
    [TestClass]
    public class TDDStavkaInventaraServiceTests
    {
        [TestMethod]
        public void JePredIstekom_VracaTrue_AkoJeHranaStarijaOd25Dana()
        {
            var s = new StavkaInventara
            {
                Naziv = "Mlijeko",
                Kategorija = Kategorija.Hrana,
                DatumNabavke = DateTime.Now.AddDays(-26)
            };

            Assert.IsTrue(StavkaInventaraService.JePredIstekom(s));
        }

        [TestMethod]
        public void JePredIstekom_VracaFalse_AkoHranaNijeStara()
        {
            var s = new StavkaInventara
            {
                Naziv = "Jogurt",
                Kategorija = Kategorija.Hrana,
                DatumNabavke = DateTime.Now.AddDays(-5)
            };

            Assert.IsFalse(StavkaInventaraService.JePredIstekom(s));
        }

        [TestMethod]
        public void JePredIstekom_VracaFalse_ZaNekategorisaneStavke()
        {
            var s = new StavkaInventara
            {
                Naziv = "Sto",
                Kategorija = Kategorija.Namjestaj,
                DatumNabavke = DateTime.Now.AddDays(-40)
            };

            Assert.IsFalse(StavkaInventaraService.JePredIstekom(s));
        }
    }
}
