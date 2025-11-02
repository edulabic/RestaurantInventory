using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInventory
{
    internal class DateRange
    {
        public DateTime Pocetak { get; set; }
        public DateTime Kraj { get; set; }

        public DateRange(DateTime pocetak, DateTime kraj)
        {
            Pocetak = pocetak;
            Kraj = kraj;
        }

        public bool SadrziDatum(DateTime datum)
        {
            return datum >= Pocetak && datum <= Kraj;
        }
    }
}
