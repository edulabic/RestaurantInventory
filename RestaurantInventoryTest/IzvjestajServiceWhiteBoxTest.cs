using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantInventory.Models;
using RestaurantInventory.Services;
using System;
using System.Collections.Generic;

namespace RestaurantInventoryTest
{
    [TestClass]
    public class IzvjestajServiceWhiteBoxTest
    {
        private Izvjestaj _izvjestaj;

        [TestInitialize]
        public void TestInit()
        {
            _izvjestaj = new Izvjestaj("Testni");
        }

        private StavkaInventara KreirajStavku(
            double cijena = 10,
            double kolicina = 10,
            Kategorija kategorija = Kategorija.Hrana,
            DateTime? datum = null,
            double minKolicina = 5)
        {
            return new StavkaInventara
            {
                Naziv = "Test",
                Cijena = cijena,
                Kolicina = kolicina,
                Kategorija = kategorija,
                Dobavljac = "Dobavljac",
                DatumNabavke = datum ?? DateTime.Now.AddDays(-10),
                MinKolicina = minKolicina
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        // Test za branch coverage: if (izvjestaj == null)
        public void Generisi_IzvjestajNull_BacaIzuzetak()
        {
            IzvjestajService.Generisi(null, new List<StavkaInventara>());
        }

        [TestMethod]
        // Test za branch i statement coverage: if (lista == null || lista.Count == 0)
        public void Generisi_PraznaLista_NemaStavki()
        {
            IzvjestajService.Generisi(_izvjestaj, new List<StavkaInventara>());

            Assert.AreEqual(0, _izvjestaj.Stavke.Count);
            Assert.AreEqual(0, _izvjestaj.UkupnaVrijednost);
        }

        [TestMethod]
        // Test branch coverage: if (s == null)
        public void Generisi_NullStavka_PreskaceStavku()
        {
            var lista = new List<StavkaInventara> { null };

            IzvjestajService.Generisi(_izvjestaj, lista);

            Assert.AreEqual(0, _izvjestaj.Stavke.Count);
        }

        [TestMethod]
        // Test statement coverage: dodavanje jedne stavke (foreach petlja, svi filteri prolaze)
        public void Generisi_JednaStavka_DodajeStavku()
        {
            var lista = new List<StavkaInventara>
            {
                KreirajStavku()
            };

            IzvjestajService.Generisi(_izvjestaj, lista);

            Assert.AreEqual(1, _izvjestaj.Stavke.Count);
        }

        [TestMethod]
        // Test statement i branch coverage: dodavanje više stavki
        public void Generisi_ViseStavki_DodajeSve()
        {
            var lista = new List<StavkaInventara>
            {
                KreirajStavku(),
                KreirajStavku(),
                KreirajStavku()
            };

            IzvjestajService.Generisi(_izvjestaj, lista);

            Assert.AreEqual(3, _izvjestaj.Stavke.Count);
        }

        [TestMethod]
        // Branch coverage
        public void Generisi_StavkaVanPerioda_Preskace()
        {
            _izvjestaj.PocetakPerioda = DateTime.Now.AddDays(-5);
            _izvjestaj.KrajPerioda = DateTime.Now;

            var lista = new List<StavkaInventara>
            {
                KreirajStavku(datum: DateTime.Now.AddDays(-20))
            };

            IzvjestajService.Generisi(_izvjestaj, lista);

            Assert.AreEqual(0, _izvjestaj.Stavke.Count);
        }

        [TestMethod]
        // Branch coverage: if (s.Kategorija != FilterKategorija)
        public void Generisi_PogresnaKategorija_Preskace()
        {
            _izvjestaj.FilterKategorija = Kategorija.Pice;

            var lista = new List<StavkaInventara>
            {
                KreirajStavku(kategorija: Kategorija.Hrana)
            };

            IzvjestajService.Generisi(_izvjestaj, lista);

            Assert.AreEqual(0, _izvjestaj.Stavke.Count);
        }

        [TestMethod]
        // Branch coverage: if (SamoKriticne && !JeKriticna(s))
        public void Generisi_SamoKriticne_NeKriticnaStavka_Preskace()
        {
            _izvjestaj.SamoKriticne = true;

            var lista = new List<StavkaInventara>
            {
                KreirajStavku(kolicina: 50, minKolicina: 10)
            };

            IzvjestajService.Generisi(_izvjestaj, lista);

            Assert.AreEqual(0, _izvjestaj.Stavke.Count);
        }

        [TestMethod]
        // Branch coverage: if (vrijednost > 1000) - bez popusta
        public void Generisi_VrijednostManjaOd1000_BezPopusta()
        {
            var lista = new List<StavkaInventara>
            {
                KreirajStavku(cijena: 10, kolicina: 10)
            };

            IzvjestajService.Generisi(_izvjestaj, lista);

            Assert.AreEqual(100, _izvjestaj.UkupnaVrijednost);
        }

        [TestMethod]
        // Branch coverage: if (vrijednost > 1000) - primjenjuje popust
        public void Generisi_VrijednostVecaOd1000_PrimjenjujePopust()
        {
            var lista = new List<StavkaInventara>
            {
                KreirajStavku(cijena: 200, kolicina: 10)
            };

            IzvjestajService.Generisi(_izvjestaj, lista);

            Assert.AreEqual(1900, _izvjestaj.UkupnaVrijednost);
        }

        [TestMethod]
        // Statement i branch coverage: prolazi sve filtere i dodaje stavku, provjera ukupne vrijednosti
        public void Generisi_StavkaProlaziSveFiltere_DodanaIUzimljeVrijednost()
        {
            _izvjestaj.FilterKategorija = Kategorija.Hrana;

            var lista = new List<StavkaInventara>
            {
                KreirajStavku()
            };

            IzvjestajService.Generisi(_izvjestaj, lista);

            Assert.AreEqual(1, _izvjestaj.Stavke.Count);
            Assert.IsTrue(_izvjestaj.UkupnaVrijednost > 0);
        }
    }
}
