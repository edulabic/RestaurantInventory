using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantInventory.Models;
using RestaurantInventory.Services;
using System.Collections.Generic;

namespace RestaurantInventoryTest
{
    [TestClass]
    public class FilterServiceTests
    {
        private List<StavkaInventara> _stavke;

        [TestInitialize]
        public void Init()
        {
            _stavke = new List<StavkaInventara>
            {
                new StavkaInventara { Naziv = "Jabuka", Cijena = 1.5, Kolicina = 50, Dobavljac = "A1", Kategorija = Kategorija.Hrana },
                new StavkaInventara { Naziv = "Stolica", Cijena = 30, Kolicina = 10, Dobavljac = "FurniCo", Kategorija = Kategorija.Namjestaj },
                new StavkaInventara { Naziv = "Sok", Cijena = 2.5, Kolicina = 100, Dobavljac = "A1", Kategorija = Kategorija.Pice }
            };
        }

        [TestMethod]
        public void Filter_By_Name_ReturnsOnlyMatchingItems()
        {
            var filter = new Filter(naziv: "Jabuka");
            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual("Jabuka", rezultat[0].Naziv);
        }

        [TestMethod]
        public void Filter_By_Category_ReturnsExpected()
        {
            var filter = new Filter(kategorija: Kategorija.Pice);
            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(Kategorija.Pice, rezultat[0].Kategorija);
        }

        [TestMethod]
        public void Filter_By_Supplier_ReturnsExpected()
        {
            var filter = new Filter(dobavljac: "A1");
            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);

            Assert.AreEqual(2, rezultat.Count);
        }

        [TestMethod]
        public void Filter_Min_Quantity_Works()
        {
            var filter = new Filter(minKolicina: 20);
            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);

            Assert.AreEqual(2, rezultat.Count);
        }

        [TestMethod]
        public void Filter_Max_Quantity_Works()
        {
            var filter = new Filter(maxKolicina: 50);
            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);

            Assert.AreEqual(2, rezultat.Count);
        }

        [TestMethod]
        public void Filter_Min_Price_Works()
        {
            var filter = new Filter(minCijena: 2);
            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);

            Assert.AreEqual(2, rezultat.Count);
        }

        [TestMethod]
        public void Filter_Max_Price_Works()
        {
            var filter = new Filter(maxCijena: 3);
            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);

            Assert.AreEqual(2, rezultat.Count);
        }

        [TestMethod]
        public void Filter_Null_ReturnsAll()
        {
            var rezultat = FilterService.PrimijeniFilter(_stavke, null);

            Assert.AreEqual(3, rezultat.Count);
        }

        [TestMethod]
        public void Filter_NoMatchingItems_ReturnsEmptyList()
        {
            var filter = new Filter(naziv: "NePostoji");
            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);

            Assert.AreEqual(0, rezultat.Count);
        }

        [TestMethod]
        public void Filter_Combination_Works()
        {
            var filter = new Filter(
                naziv: "Sok",
                kategorija: Kategorija.Pice,
                dobavljac: "A1",
                minKolicina: 50,
                maxKolicina: 200,
                minCijena: 2,
                maxCijena: 3
            );
            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual("Sok", rezultat[0].Naziv);
        }

        [DataTestMethod]
        [DataRow("jabuka", 1)] // case-insensitive test
        [DataRow("SOK", 1)]
        [DataRow("STOLICA", 1)]
        public void Filter_Name_IsCaseInsensitive(string naziv, int expectedCount)
        {
            var filter = new Filter(naziv: naziv);
            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);

            Assert.AreEqual(expectedCount, rezultat.Count);
        }

        public static IEnumerable<object[]> KolicinaTestPodaci =>
    new List<object[]>
    {
        new object[] { 0, 3 },   
        new object[] { 20, 2 },  
        new object[] { 60, 1 },  
        new object[] { 200, 0 }  
    };

        [DataTestMethod]
        [DynamicData(nameof(KolicinaTestPodaci))]
        public void Filter_MinQuantity_DataDriven(int minKolicina, int expected)
        {
            var filter = new Filter(minKolicina: minKolicina);
            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);

            Assert.AreEqual(expected, rezultat.Count);
        }

    }
}
