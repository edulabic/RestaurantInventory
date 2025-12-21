using RestaurantInventory.Models;
using RestaurantInventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInventoryTest
{
    [TestClass]
    public class FilterServiceWhiteBoxTest

    {
        private List<StavkaInventara> _stavke;

        [TestInitialize]
        public void Init()
        {
            _stavke = new List<StavkaInventara>
            {
                new StavkaInventara
                {
                    Naziv = "Jabuka",
                    Cijena = 1.5,
                    Kolicina = 50,
                    Dobavljac = "A1",
                    Kategorija = Kategorija.Hrana
                },
                new StavkaInventara
                {
                    Naziv = "Stolica",
                    Cijena = 30,
                    Kolicina = 10,
                    Dobavljac = "FurniCo",
                    Kategorija = Kategorija.Namjestaj
                },
                new StavkaInventara
                {
                    Naziv = "Sok",
                    Cijena = 2.5,
                    Kolicina = 100,
                    Dobavljac = "A1",
                    Kategorija = Kategorija.Pice
                }
            };
        }

        [TestMethod]
        public void PrimijeniFilter_FilterJeNull_VracaSveStavke()
        {
            var rezultat = FilterService.PrimijeniFilter(_stavke, null);

            Assert.AreEqual(3, rezultat.Count);
        }

        [TestMethod]
        public void PrimijeniFilter_SviUsloviProlaze_JabukaJeDodana()
        {
            var filter = new Filter
            {
                Naziv = "Jabuka",
                Kategorija = Kategorija.Hrana,
                Dobavljac = "A1",
                MinKolicina = 10
            };

            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);
            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual("Jabuka", rezultat[0].Naziv);
        }

        [TestMethod]
       public void PrimijeniFilter_NazivNeOdgovara_NistaNijeDodano()
        {
            var filter = new Filter
            {
                Naziv = "Banana"
            };
            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);
            Assert.AreEqual(0, rezultat.Count);
        }

        [TestMethod]
        public void PrimijeniFilter_KategorijaNeOdgovara_StavkaNijeDodana()
        {
            var filter = new Filter
            {
                Naziv = "Jabuka",
                Kategorija = Kategorija.Pice
            };
            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);
            Assert.AreEqual(0, rezultat.Count);
        }

        [TestMethod]
        public void PrimijeniFilter_DobavljacNeOdgovara_StavkaNijeDodana()
        {
            var filter = new Filter
            {
                Naziv = "Jabuka",
                Kategorija = Kategorija.Hrana,
                Dobavljac = "B2"
            };
            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);
            Assert.AreEqual(0, rezultat.Count);
        }

        [TestMethod]
        public void PrimijeniFilter_MinKolicinaPrevelika_StavkaNijeDodana()
        {
            var filter = new Filter
            {
                Naziv = "Jabuka",
                Kategorija = Kategorija.Hrana,
                Dobavljac = "A1",
                MinKolicina = 100
            };
            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);
            Assert.AreEqual(0, rezultat.Count);
        }

        [TestMethod]
        public void PrimijeniFilter_MaxKolicinaPremala_StavkaNijeDodana()
        {
            var filter = new Filter
            {
                Naziv = "Stolica",
                Kategorija = Kategorija.Namjestaj,
                Dobavljac = "FurniCo",
                MaxKolicina = 5
            };
            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);
            Assert.AreEqual(0, rezultat.Count);
        }

        [TestMethod]
        public void PrimijeniFilter_MinCijenaPrevelika_StavkaNijeDodana()
        {
            var filter = new Filter
            {
                Naziv = "Stolica",
                Kategorija = Kategorija.Namjestaj,
                Dobavljac = "FurniCo",
                MinCijena = 40
            };
            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);
            Assert.AreEqual(0, rezultat.Count);
        }


        [TestMethod]
        public void PrimijeniFilter_MaxCijenaPremala_StavkaNijeDodana()
        {
            var filter = new Filter
            {
                Naziv = "Stolica",
                Kategorija = Kategorija.Namjestaj,
                Dobavljac = "FurniCo",
                MaxCijena = 20
            };
            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);
            Assert.AreEqual(0, rezultat.Count);
        }

        [TestMethod]
        public void PrimijeniFilter_NazivJeNull_StavkeNisuFiltriranePoNazivu()
        {
            var filter = new Filter
            {
                Naziv = null
            };
            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);
            Assert.AreEqual(3, rezultat.Count);
        }

        [TestMethod]
        public void PrimijeniFilter_PraznaListaStavki_VracaPraznuListu()
        {
            var prazneStavke = new List<StavkaInventara>();

            Filter filter = null;

            var rezultat = FilterService.PrimijeniFilter(prazneStavke, filter);

            Assert.AreEqual(0, rezultat.Count);
        }

        [TestMethod]
        public void PrimijeniFilter_JednaStavka_VracaTuStavkuAkoProlaziFilter()
        {
            var jednaStavka = new List<StavkaInventara>
            {
                new StavkaInventara
                {
                    Naziv = "Kola",
                    Cijena = 1.0,
                    Kolicina = 20,
                    Dobavljac = "A1",
                    Kategorija = Kategorija.Pice
                }
            };
            var filter = new Filter
            {
                Naziv = "Kola",
                Kategorija = Kategorija.Pice,
                Dobavljac = "A1",
                MinKolicina = 10
            };
            var rezultat = FilterService.PrimijeniFilter(jednaStavka, filter);
            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual("Kola", rezultat[0].Naziv);
        }

        [TestMethod]
        public void PrimijeniFilter_PrazanNaziv_StavkeNisuFiltriranePoNazivu()
        {
            var filter = new Filter
            {
                Naziv = "", 
                Kategorija = Kategorija.None,
                Dobavljac = null
            };
            var rezultat = FilterService.PrimijeniFilter(_stavke, filter);

            Assert.AreEqual(3, rezultat.Count);
        }

    }
}
