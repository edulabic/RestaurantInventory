using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInventory
{
    internal interface IInventarService
    {
        void DodajStavku(StavkaInventara stavka);
        void UkloniStavku(string naziv);
        void AzurirajStavku(string naziv, double novaKolicina);
        List<StavkaInventara> ProvjeriMinimalneZalihe();
        List<StavkaInventara> Pretrazi(Filter filter);
        void PrikaziSveStavke();
    }

}
