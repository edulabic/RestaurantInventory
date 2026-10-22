using RestaurantInventory.Models;
using RestaurantInventory.Services;
using System;
using System.Collections.Generic;

namespace RestaurantInventory.Tests
{
    [TestClass]
    public class TDDInventarServiceTests
    {
        [TestMethod]
        public void GenerisiNarudzbe_VracaStavkuKojaJeIspodMinimuma()
        {
            var inventar = new Inventar();
            var service = new InventarService(inventar);

            inventar.Stavke.Add(new StavkaInventara
            {
                Naziv = "Kafa",
                Kolicina = 2,
                MinKolicina = 5,
                Dobavljac = "Klas",
                Kategorija = Kategorija.Pice,
                DatumNabavke = DateTime.Now
            });

            var narudzbe = service.GenerisiNarudzbe();

            Assert.AreEqual(1, narudzbe.Count);
            Assert.AreEqual("Kafa", narudzbe[0].Naziv);
            Assert.AreEqual(3, narudzbe[0].Nedostaje);
        }

        [TestMethod]
        public void GenerisiNarudzbe_IgnoriseStavkeIznadMinimuma()
        {
            var inventar = new Inventar();
            var service = new InventarService(inventar);

            inventar.Stavke.Add(new StavkaInventara
            {
                Naziv = "Secer",
                Kolicina = 20,
                MinKolicina = 10,
                Dobavljac = "AS"
            });

            var narudzbe = service.GenerisiNarudzbe();

            Assert.AreEqual(0, narudzbe.Count);
        }

        [TestMethod]
        public void GenerisiNarudzbe_RadiZaViseStavki()
        {
            var inventar = new Inventar();
            var service = new InventarService(inventar);

            inventar.Stavke.Add(new StavkaInventara
            {
                Naziv = "Ulje",
                Kolicina = 1,
                MinKolicina = 5,
                Dobavljac = "Bimal"
            });

            inventar.Stavke.Add(new StavkaInventara
            {
                Naziv = "Brasno",
                Kolicina = 10,
                MinKolicina = 8,
                Dobavljac = "Klas"
            });

            var narudzbe = service.GenerisiNarudzbe();

            Assert.AreEqual(1, narudzbe.Count);
            Assert.AreEqual("Ulje", narudzbe[0].Naziv);
        }
    }
}
