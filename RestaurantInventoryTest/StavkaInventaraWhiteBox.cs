using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantInventory.Models;
using RestaurantInventory.Services;

namespace RestaurantInventoryTest
{
    [TestClass]
    public class StavkaInventaraWhiteBoxTest
    {
        private StavkaInventara _stavka;

        [TestInitialize]
        public void Init()
        {
            _stavka = new StavkaInventara
            {
                Naziv = "TestStavka",
                Cijena = 5,
                Dobavljac = "Test",
                MinKolicina = 10
            };
        }

        //statement - line coverage
        //izvršava sve linije koje vode do return true
        [TestMethod]
        public void KolicinaNula_StaraHrana_Kriticna()
        {
            _stavka.Kolicina = 0;
            _stavka.Kategorija = Kategorija.Hrana;
            _stavka.DatumNabavke = DateTime.Now.AddDays(-70);

            bool rezultat = StavkaInventaraService.JeKriticna(_stavka);

            Assert.IsTrue(rezultat);
        }

        //izvršava else grane i return false
        [TestMethod]
        public void DovoljnaKolicina_NovaStavka_NijeKriticna()
        {
            _stavka.Kolicina = 20;
            _stavka.Kategorija = Kategorija.Ostalo;
            _stavka.DatumNabavke = DateTime.Now.AddDays(-5);

            bool rezultat = StavkaInventaraService.JeKriticna(_stavka);

            Assert.IsFalse(rezultat);
        }

    
        //branch coverage
        // switch: hrana 
        [TestMethod]
        public void Branch_Hrana_IspodMinimalne_StarijaOd25_Kriticna()
        {
            _stavka.Kolicina = 5;
            _stavka.Kategorija = Kategorija.Hrana;
            _stavka.DatumNabavke = DateTime.Now.AddDays(-30);

            bool rezultat = StavkaInventaraService.JeKriticna(_stavka);

            Assert.IsTrue(rezultat);
        }

        // switch: default
        [TestMethod]
        public void Branch_DefaultKategorija_ShouldNotBeCritical()
        {
            _stavka.Kolicina = 9;             
            _stavka.Kategorija = Kategorija.Ostalo;
            _stavka.DatumNabavke = DateTime.Now.AddDays(-5);

            bool rezultat = StavkaInventaraService.JeKriticna(_stavka);

            Assert.IsFalse(rezultat);
        }


        // switch: pice
        [TestMethod]
        public void Branch_Pice_IspodMinimalne_Kriticno()
        {
            _stavka.Kolicina = 5;
            _stavka.Kategorija = Kategorija.Pice;
            _stavka.DatumNabavke = DateTime.Now.AddDays(-40);

            bool rezultat = StavkaInventaraService.JeKriticna(_stavka);

            Assert.IsTrue(rezultat);
        }

       

        // mcdc
        [TestMethod]
        public void MCDC_SviTrue()
        {
            _stavka.Kolicina = 5;
            _stavka.Kategorija = Kategorija.Hrana;
            _stavka.DatumNabavke = DateTime.Now.AddDays(-30);

            bool rezultat = StavkaInventaraService.JeKriticna(_stavka);

            Assert.IsTrue(rezultat);
        }

        // F (kategorija), ostalo T
        [TestMethod]
        public void MCDC_KategorijaFalse()
        {
            _stavka.Kolicina = 9;
            _stavka.Kategorija = Kategorija.Ostalo;
            _stavka.DatumNabavke = DateTime.Now.AddDays(-10);

            bool rezultat = StavkaInventaraService.JeKriticna(_stavka);

            Assert.IsFalse(rezultat);
        }


        // F (količina)
        [TestMethod]
        public void MCDC_KolicinaFalse()
        {
            _stavka.Kolicina = 15;
            _stavka.Kategorija = Kategorija.Hrana;
            _stavka.DatumNabavke = DateTime.Now.AddDays(-30);

            bool rezultat = StavkaInventaraService.JeKriticna(_stavka);

            Assert.IsFalse(rezultat);
        }

        // F (dani)
        [TestMethod]
        public void MCDC_DaniFalse()
        {
            _stavka.Kolicina = 10;
            _stavka.Kategorija = Kategorija.Hrana;
            _stavka.DatumNabavke = DateTime.Now.AddDays(-10);

            bool rezultat = StavkaInventaraService.JeKriticna(_stavka);

            Assert.IsFalse(rezultat);
        }



        //dataflow
        //varijabla rizik-=1
        [TestMethod]
        public void DataFlow_Rizik_Inicijalizacija_ElseGrana()
        {
            _stavka.Kolicina = 20;
            _stavka.Kategorija = Kategorija.Ostalo;
            _stavka.DatumNabavke = DateTime.Now.AddDays(-10);

            bool rezultat = StavkaInventaraService.JeKriticna(_stavka);

            Assert.IsFalse(rezultat);
        }

        // rizik+=3 
        [TestMethod]
        public void DataFlow_Rizik_KolicinaIspodMinimuma()
        {
            _stavka.Kolicina = 8;
            _stavka.Kategorija = Kategorija.Ostalo;
            _stavka.DatumNabavke = DateTime.Now.AddDays(-10);

            bool rezultat = StavkaInventaraService.JeKriticna(_stavka);

            Assert.IsFalse(rezultat);
        }

        //  rizik +=3, +=2
        [TestMethod]
        public void DataFlow_Rizik_VisestrskaRedefinicija_KolicinaNula()
        {
            _stavka.Kolicina = 0;
            _stavka.Kategorija = Kategorija.Ostalo;
            _stavka.DatumNabavke = DateTime.Now.AddDays(-10);

            bool rezultat = StavkaInventaraService.JeKriticna(_stavka);

            Assert.IsTrue(rezultat);
        }


        // dani>30
        [TestMethod]
        public void DataFlow_Dani_Preko30()
        {
            _stavka.Kolicina = 15;
            _stavka.Kategorija = Kategorija.Ostalo;
            _stavka.DatumNabavke = DateTime.Now.AddDays(-35);

            bool rezultat = StavkaInventaraService.JeKriticna(_stavka);

            Assert.IsFalse(rezultat);
        }

        // dani > 30 i dani > 60
        [TestMethod]
        public void DataFlow_Dani_VisestrukoKoristenje_Preko60()
        {
            _stavka.Kolicina = 15;
            _stavka.Kategorija = Kategorija.Hrana;
            _stavka.DatumNabavke = DateTime.Now.AddDays(-70);

            bool rezultat = StavkaInventaraService.JeKriticna(_stavka);

            Assert.IsTrue(rezultat);
        }


    }
}