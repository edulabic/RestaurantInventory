# Restaurant Inventory

A C# / .NET 8 console application for managing a restaurant's inventory, paired with a comprehensive MSTest suite that demonstrates a range of software testing techniques (black-box, white-box, TDD, data-driven, and more).  Developed as a group project for the Software Verification and Validation course at Faculty of Electrical Engineering Sarajevo.

---

## Features

- **Add items** – name, description, category, quantity, minimum quantity, price, supplier and purchase date
- **View all items** – with sorting by price or quantity (ascending / descending)
- **Low-stock check** – lists items that are below their minimum quantity
- **Search / filter** – by name (case-insensitive), category, supplier, and price / quantity ranges
- **Remove items** by name
- **Update quantity** – whole numbers enforced for countable categories (furniture, dishes, cleaning supplies), decimals allowed for others
- **Generate reports** – by period and category, with item count and total inventory value
- **Order generation** – builds a list of items that need restocking and how much is missing
- **Expiry / criticality checks** – flags items as "critical" based on quantity, category and age (e.g. food older than 25 days)

### Categories

`Hrana` (Food) · `Pice` (Drinks) · `Posudje` (Dishes) · `SredstvaZaCiscenje` (Cleaning supplies) · `Namjestaj` (Furniture) · `Ostalo` (Other)

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 or any editor with .NET support (VS Code, Rider)

### Clone and run

```bash
git clone https://github.com/<your-username>/<your-repo>.git
cd <your-repo>

dotnet run --project RestaurantInventory
```

You will see the main menu:

```
===========================================
           INVENTAR RESTORANA
===========================================
1. Dodaj stavku
2. Prikazi sve stavke
3. Provjeri minimalne zalihe
4. Pretrazi (Filter)
5. Ukloni stavku
6. Ažuriraj količinu stavke
7. Generiši izvještaj
0. Izlaz
```

> Note: data is held in memory only and is not persisted between runs.

---

## Testing

The test project uses **MSTest 3.6.4** with the Microsoft Testing Platform and code coverage extension.

```bash
# Run all tests
dotnet test

# Run with code coverage
dotnet test --collect:"Code Coverage"
```

### Testing techniques demonstrated

| Technique | Where |
|---|---|
| **Black-box / functional tests** | `FilterServiceTests`, `InventarServiceTests`, `IzvjestajServiceTests`, `StavkaInventaraTests` |
| **White-box: statement & branch coverage** | `StavkaInventaraWhiteBox`, `FilterServiceWhiteBoxTest` |
| **Condition coverage** | `InventarService_ObuhvatLinija` (TC1–TC9) |
| **MC/DC** | `StavkaInventaraWhiteBox` (`MCDC_*` tests) |
| **Data-flow testing** | `StavkaInventaraWhiteBox` (`DataFlow_*` tests) |
| **Loop coverage** (0, 1, many iterations) | `ObuhvatPetlje`, `InventarService_ObuhvatLinija` |
| **Test-Driven Development (TDD)** | `TDDInventarServiceTests` (`GenerisiNarudzbe`), `TDDStavkaInventaraServiceTests` (`JePredIstekom`) |
| **Data-driven tests (`DataRow`, `DynamicData`)** | `FilterServiceTests`, `IzvjestajServiceTests`, `StavkaInventaraServiceTests` |
| **External data-driven tests (XML)** | `XmlTestDataReader` + `TestData.xml` |
| **Exception testing** | `[ExpectedException]` for null / duplicate / missing items |
| **Dummy / stub objects** | `UkloniStavku_UsingDummyObject_Works`, `Generisi_WithStubbedData_CriticalOnly` |
| **Parallel execution** | `MSTestSettings.cs` (method-level parallelization) |


## Tech Stack

- C# on **.NET 8**
- **MSTest 3.6.4**, Microsoft.NET.Test.Sdk 17.12.0
- Microsoft.Testing.Extensions.CodeCoverage / TrxReport

---

## License

This project was created for educational purposes.
