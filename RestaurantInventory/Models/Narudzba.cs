using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInventory.Models
{
    //Klasa dodana za potrebe TDD-a
    public class Narudzba
    {
        public string Naziv { get; set; }
        public string Dobavljac { get; set; }
        public double Nedostaje { get; set; }
    }
}
