using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantInventory.Models;
using RestaurantInventory.Services;
using System;
using System.Collections.Generic;

namespace RestaurantInventoryTest
{
    [TestClass]
    public class IzvjestajServiceTests
    {
        private List<StavkaInventara> _data;

        [TestInitialize]
        public void Init()
        {
            _data = new List<StavkaInventara>
            {
                new StavkaInventara {
                    Naziv="Sok", Cijena=2, Kolicina=10, Kategorija=Kategorija.Pice,
                    DatumNabavke = DateTime.Now.AddDays(-3)
                },
                new StavkaInventara {
                    Naziv="Stolica", Cijena=30, Kolicina=5, Kategorija=Kategorija.Namjestaj,
                    DatumNabavke = DateTime.Now.AddDays(-50)
                }
            };
        }

        [TestMethod]
        public void Generisi_Filters_By_Date()
        {
            var iz = new Izvjestaj("test")
            {
                PocetakPerioda = DateTime.Now.AddDays(-10),
                KrajPerioda = DateTime.Now
            };

            IzvjestajService.Generisi(iz, _data);

            Assert.AreEqual(1, iz.Stavke.Count);
            Assert.AreEqual("Sok", iz.Stavke[0].Naziv);
        }

        [TestMethod]
        public void Generisi_Filters_By_Category()
        {
            var iz = new Izvjestaj("test")
            {
                FilterKategorija = Kategorija.Pice
            };

            IzvjestajService.Generisi(iz, _data);

            Assert.AreEqual(1, iz.Stavke.Count);
            Assert.AreEqual(Kategorija.Pice, iz.Stavke[0].Kategorija);
        }

        [TestMethod]
        public void Generisi_EmptyList_DoesNotThrow()
        {
            var iz = new Izvjestaj("test");
            IzvjestajService.Generisi(iz, new List<StavkaInventara>());
            Assert.AreEqual(0, iz.Stavke.Count);
        }

        [TestMethod]
        public void Generisi_Calculates_Total_Value()
        {
            var iz = new Izvjestaj("test");

            IzvjestajService.Generisi(iz, _data);

            Assert.AreEqual(2 * 10 + 30 * 5, iz.UkupnaVrijednost);
        }

        [TestMethod]
        public void Generisi_WithStubbedData_CriticalOnly()
        {
            var stubData = new List<StavkaInventara>
            {
                new StavkaInventara { Naziv="Stub", Cijena=1, Kolicina=1, DatumNabavke=DateTime.Now, MinKolicina=5, Kategorija=Kategorija.Hrana }
            };

            var iz = new Izvjestaj("test") { SamoKriticne = true };
            IzvjestajService.Generisi(iz, stubData);

            Assert.AreEqual(1, iz.Stavke.Count);
            Assert.AreEqual("Stub", iz.Stavke[0].Naziv);
        }

        [TestMethod]
        public void Generisi_NullIzvjestaj_DoesNotThrow()
        {
            IzvjestajService.Generisi(null, _data); // samo ne smije baciti exception
        }

        [TestMethod]
        public void Generisi_NullList_DoesNotThrow()
        {
            var iz = new Izvjestaj("test");
            IzvjestajService.Generisi(iz, null); // samo ne smije baciti exception
            Assert.AreEqual(0, iz.Stavke.Count);
        }

        [TestMethod]
        public void Generisi_SamoKriticne_False_IncludesAll()
        {
            var kriticna = new StavkaInventara { Naziv = "Kriticna", Cijena = 1, Kolicina = 1, MinKolicina = 5 };
            var neKriticna = new StavkaInventara { Naziv = "Normalna", Cijena = 1, Kolicina = 10, MinKolicina = 5 };

            var lista = new List<StavkaInventara> { kriticna, neKriticna };
            var iz = new Izvjestaj("test") { SamoKriticne = false };

            IzvjestajService.Generisi(iz, lista);

            Assert.AreEqual(2, iz.Stavke.Count);
        }
    }
}
