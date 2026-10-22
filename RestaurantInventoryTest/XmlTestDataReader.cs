using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using RestaurantInventory.Models;

namespace RestaurantInventoryTest
{
    public static class XmlTestDataReader
    {
        public static IEnumerable<object[]> GetPretragaTestData()
        {
            string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "TestData.xml");
            XDocument doc = XDocument.Load(xmlPath);

            foreach (var test in doc.Descendants("Test"))
            {
                string naziv = test.Element("Naziv")?.Value;
                string kategorijaStr = test.Element("Kategorija")?.Value;
                string dobavljac = test.Element("Dobavljac")?.Value;
                string minCijenaStr = test.Element("MinCijena")?.Value;
                string maxCijenaStr = test.Element("MaxCijena")?.Value;
                string minKolicinaStr = test.Element("MinKolicina")?.Value;
                string maxKolicinaStr = test.Element("MaxKolicina")?.Value;
                int expectedCount = int.Parse(test.Element("ExpectedCount")?.Value ?? "0");

                Kategorija kategorija = string.IsNullOrEmpty(kategorijaStr)
                    ? Kategorija.None
                    : (Kategorija)System.Enum.Parse(typeof(Kategorija), kategorijaStr);

                double? minCijena = string.IsNullOrEmpty(minCijenaStr)
                    ? (double?)null
                    : double.Parse(minCijenaStr);

                double? maxCijena = string.IsNullOrEmpty(maxCijenaStr)
                    ? (double?)null
                    : double.Parse(maxCijenaStr);

                double? minKolicina = string.IsNullOrEmpty(minKolicinaStr)
                    ? (double?)null
                    : double.Parse(minKolicinaStr);

                double? maxKolicina = string.IsNullOrEmpty(maxKolicinaStr)
                    ? (double?)null
                    : double.Parse(maxKolicinaStr);

                var filter = new Filter(
                    naziv: string.IsNullOrEmpty(naziv) ? null : naziv,
                    kategorija: kategorija,
                    dobavljac: string.IsNullOrEmpty(dobavljac) ? null : dobavljac,
                    minCijena: minCijena,
                    maxCijena: maxCijena,
                    minKolicina: minKolicina,
                    maxKolicina: maxKolicina
                );

                yield return new object[] { filter, expectedCount };
            }
        }
    }
}