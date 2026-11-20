# CS2 Tracker

A tracker for CS2 skin arbitrage. Record the skins you buy, compare their current prices on YouPin and CSFloat, close them when you sell, and see the profit and return for each trade.

Built with **ASP.NET Core** (MVC and Web API), **ASP.NET Core Identity**, **Entity Framework Core**, and **SQL Server**.

<img width="1919" height="921" alt="image" src="https://github.com/user-attachments/assets/a2d8e16f-66f0-4f71-a3e5-d790541cb089" />


---

## Quick start

1. Install the **.NET 10 SDK** and **SQL Server LocalDB** (included with Visual Studio's "Data storage and processing" workload).
2. Install the EF Core tool (once):

   ```bash
   dotnet tool install --global dotnet-ef
   ```

3. Create the database (the default connection string uses LocalDB, so no setup is needed):

   ```bash
   dotnet ef database update --project CS2Tracker_Data --startup-project CS2Tracker_Api
   ```

4. Start the web app:

   ```bash
   dotnet run --project CS2Tracker_MVC
   ```

5. Open the address printed in the terminal, **register an account**, create a sheet, and add your first entry.

The Web API is optional and runs separately (`dotnet run --project CS2Tracker_Api` in a second terminal). It needs a JWT key first, see [Configuration](#configuration). Using a different database? See [Configuration](#configuration) too.

---

## Background: what is being tracked?

> You don't need to know the game to understand the project.

- **CS2 (Counter-Strike 2)** is a popular online game. Players can buy, sell and trade cosmetic items called **skins** (weapon appearances, stickers, charms, patches, pins).
- These items have **real-money value** on online marketplaces, and their prices move up and down like a small commodity market.
- Some players **trade** them: buy an item cheap, hold it, and sell it later at a higher price. This is a form of **arbitrage**.
- To judge whether trading is actually profitable, a trader needs to know the **purchase price**, the **sale price** and **how long each item was held**. Marketplaces don't show this, and spreadsheets get messy quickly. This application solves that.

### Glossary

| Term | Meaning |
|------|---------|
| **Skin** | A cosmetic appearance for a weapon or item. Each one has a market price. |
| **Marketplace** | A website where skins are bought and sold. This app uses prices from **YouPin** and **CSFloat**. |
| **Variant** | The special version of a skin, for example normal or StatTrak. |
| **Condition** | How worn the skin looks, from Factory New (best) to Battle-Scarred (worst). Affects price. |
| **Float** | The exact wear value (0 to 1) behind the condition. Optional here. |
| **Trade hold** | Steam locks a newly acquired item from being traded for 7 days, so it can't be resold sooner. |
| **Arbitrage sheet** | A sheet for short-term trades: buy on one market, sell for a profit. |
| **Investment sheet** | A sheet for skins held for the longer term. |
| **Unrealized profit** | Profit on an item you still own, based on its current market price. |

---

## Features

- **Accounts:** register and log in with ASP.NET Core Identity. The web app uses cookies, and the API uses JWT bearer tokens.
- **Sheets:** group your trades into sheets. A sheet is either *Arbitrage* or *Investment*.
- **Adding entries:** record a skin with its buy date, buy price, variant, condition, and optional float.
- **Editing entries:** open entries can change their buy date and buy price. The item name, variant, condition, and float are fixed once the entry is added. Closed entries can't be edited.
- **Closing a trade:** enter the sell date and sell price. The sell date must be at least 7 days after the buy date (the Steam trade hold) and can't be in the future.
- **Deleting entries:** removes the entry and its skin record, after a confirmation prompt.
- **Live prices:** the sheet page shows the current YouPin and CSFloat asking prices, pulled from the [OpenSkin API](https://openskin.dev/). Each price links to its marketplace listing. Prices are cached for 10 minutes.
- **Calculated values:** profit, return, hold days, and daily return are calculated from stored prices and dates, not stored. Open entries show unrealized profit, based on the higher of the YouPin and CSFloat prices.

---

## Project structure

- **CS2Tracker_Common**: enums, validation constants, helpers, `ServiceResult`
- **CS2Tracker_Models**: entities `Sheet`, `Entry`, `SkinInfo`
- **CS2Tracker_Data**: `DbContext`, migrations, design-time factory
- **CS2Tracker_DTOs**: request and response objects for the API and the web app
- **CS2Tracker_Core**: services for entries, sheets, users, and marketplace prices
- **CS2Tracker_Api**: Web API with JWT authentication
- **CS2Tracker_MVC**: web app with cookie authentication and Razor views

The web app and the API both use the services in `CS2Tracker_Core`, so business rules such as the trade hold are enforced in one place.

---

## Prerequisites

- .NET 10 SDK
- SQL Server LocalDB (included with Visual Studio's "Data storage and processing" workload), or another SQL Server instance you can reach
- `dotnet-ef` tool: `dotnet tool install --global dotnet-ef`

---

## Configuration

The committed default connection string points to LocalDB:

```
Server=(localdb)\MSSQLLocalDB;Database=CS2Tracker;Trusted_Connection=True;TrustServerCertificate=True;
```

Don't commit a machine name, server address, or password. To use a different database, set the connection string as an environment variable. Environment variables override every `appsettings*.json` file, and they work for the API, the web app, and migrations.

**PowerShell (current session only):**

```powershell
$env:ConnectionStrings__DefaultConnection = "Server=MY-PC;Database=CS2Tracker;Trusted_Connection=True;TrustServerCertificate=True;"
```

**Bash:**

```bash
export ConnectionStrings__DefaultConnection="Server=MY-PC;Database=CS2Tracker;Trusted_Connection=True;TrustServerCertificate=True;"
```

### User secrets

User secrets keep your local values outside the repository. The web app already has a secrets ID. The API needs one first:

```bash
dotnet user-secrets init --project CS2Tracker_Api
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=MY-PC;Database=CS2Tracker;Trusted_Connection=True;TrustServerCertificate=True;" --project CS2Tracker_Api
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=MY-PC;Database=CS2Tracker;Trusted_Connection=True;TrustServerCertificate=True;" --project CS2Tracker_MVC
```

User secrets only apply in the Development environment. Migrations read environment variables, so set `ConnectionStrings__DefaultConnection` for those.

### JWT key (API)

The `Jwt:Key` value in `CS2Tracker_Api/appsettings.json` is a placeholder. Set a real key of at least 32 characters:

```bash
dotnet user-secrets set "Jwt:Key" "<at-least-32-random-characters>" --project CS2Tracker_Api
```

```bash
export Jwt__Key="<at-least-32-random-characters>"
```

### Configuration files

- **`*/appsettings.json`**: committed. Defaults only, with no machine-specific values.
- **`*/appsettings.Development.json`**: committed (API and MVC). Logging overrides only, no secrets.
- **User secrets**: not committed. Your local connection string and JWT key.
- **Environment variables**: not committed. Overrides for other machines, CI, and migrations.

---

## Running

The [Quick start](#quick-start) covers the first run. After that:

```bash
# Web app
dotnet run --project CS2Tracker_MVC

# Web API (separate terminal)
dotnet run --project CS2Tracker_Api
```

### Adding a migration

```bash
dotnet ef migrations add <MigrationName> --project CS2Tracker_Data --startup-project CS2Tracker_Api
```

<!-- If the solution has a test project, add:
### Running the tests
dotnet test
-->

---

## Known limitations

- **The Web API is untested and will be updated later.** Endpoints, request formats, and JWT handling may change. Use the web app for now.
- Only the base name is validated. Wear and variant are not checked against the skin, so a real skin with a wear or variant it doesn't come in is still accepted.
- Profit doesn't subtract marketplace fees, so it's an upper bound. The unrealized profit label says so.
- Each entry is one buy and one sell. Partial sells aren't supported.
- Closed entries can't be edited. To correct one, delete it and add it again.
- Prices come from a third-party API and can be out of date. Each price shows when the API last updated it, but the app doesn't display that yet.

---

## Acknowledgements

Market prices are provided by [OpenSkin](https://openskin.dev/), which aggregates prices from YouPin, CSFloat, Steam, Skinport, and Buff.

---

## Author

Krum

## License

MIT. See the [LICENSE](LICENSE) file.
