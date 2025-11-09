using System.Collections.Generic;

namespace RestaurantInventory.Models
{
    public class Inventar
    {
        public List<StavkaInventara> Stavke { get; set; }

        public Inventar()
        {
            Stavke = new List<StavkaInventara>();
        }
    }
}
