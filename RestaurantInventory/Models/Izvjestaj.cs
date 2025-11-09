using RestaurantInventory.Models;
using System;
using System.Collections.Generic;

namespace RestaurantInventory.Models
{
    public class Izvjestaj
    {
        public string Tip { get; set; }
        public List<StavkaInventara> Stavke { get; set; }
        public double UkupnaVrijednost { get; set; }
        public DateTime? PocetakPerioda { get; set; }
        public DateTime? KrajPerioda { get; set; }
        public Kategorija? FilterKategorija { get; set; }
        public bool SamoKriticne { get; set; } = false;

        public Izvjestaj(string tip)
        {
            Tip = tip;
            Stavke = new List<StavkaInventara>();
            UkupnaVrijednost = 0;
        }
    }
}
