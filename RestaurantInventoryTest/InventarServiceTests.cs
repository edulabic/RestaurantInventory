using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantInventory.Models;
using RestaurantInventory.Services;
using System;
using System.Collections.Generic;

namespace RestaurantInventoryTest
{
    [TestClass]
    public class InventarServiceTests
    {
        private Inventar inventar;
        private InventarService service;

        [TestInitialize]
        public void Setup()
        {
            inventar = new Inventar();
            service = new InventarService(inventar);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DodajStavku_NullStavkaBacaArgumentException()
        {
            service.DodajStavku(null);
        }

        [TestMethod]
        public void DodajStavku_Uspjesno()
        {
            var stavka = new StavkaInventara("Mlijeko", 2.5, 10, "DobavljacA", DateTime.Today, 5, Kategorija.Hrana);
            service.DodajStavku(stavka);

            Assert.AreEqual(1, inventar.Stavke.Count);
            Assert.AreEqual(stavka, inventar.Stavke[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DodajStavku_IstovjetnaBacaException()
        {
            var stavka1 = new StavkaInventara("Mlijeko", 2.5, 10, "DobavljacA", DateTime.Today, 5, Kategorija.Hrana);
            var stavka2 = new StavkaInventara("Mlijeko", 3, 5, "DobavljacA", DateTime.Today, 2, Kategorija.Hrana);

            service.DodajStavku(stavka1);
            service.DodajStavku(stavka2); // exception
        }

        [TestMethod]
        public void UkloniStavku_RadiIspravno()
        {
            var stavka = new StavkaInventara("Mlijeko", 2.5, 10, "DobavljacA", DateTime.Today, 5, Kategorija.Hrana);
            service.DodajStavku(stavka);

            service.UkloniStavku("Mlijeko");

            Assert.AreEqual(0, inventar.Stavke.Count);
        }

        [DataTestMethod]
        [DataRow("Mlijeko", 10)]
        [DataRow("Sir", 5)]
        public void AzurirajStavku_MijenjaKolicinu(string naziv, double novaKolicina)
        {
            var stavka = new StavkaInventara(naziv, 2.5, 1, "DobavljacA", DateTime.Today, 1, Kategorija.Hrana);
            service.DodajStavku(stavka);

            service.AzurirajStavku(naziv, novaKolicina);

            Assert.AreEqual(novaKolicina, stavka.Kolicina);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AzurirajStavku_NePostojiBacaException()
        {
            service.AzurirajStavku("NePostoji", 5);
        }
        /*
        [TestMethod]
        public void ProvjeriMinimalneZalihe_VracaKriticne()
        {
            var kriticna = new StavkaInventara("Stolica", 20, 1, "FurniCo", DateTime.Today, 5, Kategorija.Namjestaj);
            var neKriticna = new StavkaInventara("Jabuka", 1.5, 50, "DobavljacA", DateTime.Today, 5, Kategorija.Hrana);

            inventar.Stavke.Add(kriticna);
            inventar.Stavke.Add(neKriticna);

            var rezultat = service.ProvjeriMinimalneZalihe();

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual("Stolica", rezultat[0].Naziv);
        }*/

        [DataTestMethod]
        [DynamicData(nameof(GetFilterData), DynamicDataSourceType.Method)]
        public void Pretrazi_FiltriraKorektno(Filter filter, int expectedCount)
        {
            var stavka1 = new StavkaInventara("Mlijeko", 2.5, 10, "DobavljacA", DateTime.Today, 5, Kategorija.Hrana);
            var stavka2 = new StavkaInventara("Sir", 3.5, 5, "DobavljacB", DateTime.Today, 2, Kategorija.Hrana);
            var stavka3 = new StavkaInventara("Voda", 1.0, 20, "DobavljacC", DateTime.Today, 5, Kategorija.Pice);

            service.DodajStavku(stavka1);
            service.DodajStavku(stavka2);
            service.DodajStavku(stavka3);

            var result = service.Pretrazi(filter);

            Assert.AreEqual(expectedCount, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Pretrazi_NullFilterBacaException()
        {
            service.Pretrazi(null);
        }

        public static IEnumerable<object[]> GetFilterData()
        {
            yield return new object[] { new Filter(naziv: "Mlijeko"), 1 };
            yield return new object[] { new Filter(kategorija: Kategorija.Pice), 1 };
            yield return new object[] { new Filter(minCijena: 2), 2 };
            yield return new object[] { new Filter(maxCijena: 2), 1 };
            yield return new object[] { new Filter(minKolicina: 5, maxKolicina: 15), 2 };
            yield return new object[] { new Filter(dobavljac: "DobavljacB"), 1 };
            yield return new object[] { new Filter(naziv: "NePostoji"), 0 };
            yield return new object[] { new Filter(kategorija: Kategorija.Hrana, minCijena: 3), 1 };
        }

        [TestMethod]
        public void UkloniStavku_UsingDummyObject_Works()
        {
            var dummy = new StavkaInventara
            {
                Naziv = "Dummy"
            };

            service.DodajStavku(dummy);
            service.UkloniStavku("Dummy");

            Assert.AreEqual(0, inventar.Stavke.Count);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetXmlTestData), DynamicDataSourceType.Method)]
        public void Pretrazi_IzXmlFajla_FiltriraKorektno(Filter filter, int expectedCount)
        {
            var stavka1 = new StavkaInventara("Mlijeko", 2.5, 10, "DobavljacA", DateTime.Today, 5, Kategorija.Hrana);
            var stavka2 = new StavkaInventara("Sir", 3.5, 5, "DobavljacB", DateTime.Today, 2, Kategorija.Hrana);
            var stavka3 = new StavkaInventara("Voda", 1.0, 20, "DobavljacC", DateTime.Today, 5, Kategorija.Pice);

            service.DodajStavku(stavka1);
            service.DodajStavku(stavka2);
            service.DodajStavku(stavka3);

            var result = service.Pretrazi(filter);

            Assert.AreEqual(expectedCount, result.Count);
        }

        public static IEnumerable<object[]> GetXmlTestData()
        {
            return XmlTestDataReader.GetPretragaTestData();
        }
    }
}
