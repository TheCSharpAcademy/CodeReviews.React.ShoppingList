

## NOTE - I was playing around with and used the BMad Method when creating this application. While I didn’t write every line of code myself, I can read, understand, and trace how everything works end-to-end.
## I focused more on the architecture, data flow, and how the components interact—using AI to generate scaffolding and then refining it to fit the requirements and domain model. This meant I could move faster while still maintaining full control and understanding of the implementation.
## It also gave me the opportunity to review, validate, and adjust the code (naming, structure, and logic) so it aligns with the intended design, rather than just treating it as a black box.
## 

# 🛒 ShoppingList.NathanJenner

A full-stack shopping list application built with **ASP.NET Core 10** (Minimal API) and **React + TypeScript** (Vite), using **Entity Framework Core** with SQL Server and **Fluent UI** components.

---

## 🏗️ Tech Stack

| Layer    | Technology                              |
|----------|-----------------------------------------|
| Frontend | React 18, TypeScript, Vite, Fluent UI v9 |
| Backend  | ASP.NET Core 10 Minimal API             |
| Database | SQL Server, Entity Framework Core 10    |
| Drag & Drop | @hello-pangea/dnd                    |

---

## ✅ Prerequisites

Ensure the following are installed before getting started:

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js 20+](https://nodejs.org/)
- [SQL Server](https://www.microsoft.com/en-au/sql-server/sql-server-downloads) (Express or Developer edition)
- [EF Core CLI tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

Install the EF Core CLI if not already installed:

```sh
dotnet tool install --global dotnet-ef
```

## ⚙️ Configuration

Update the connection string in `ShoppingList.NathanJenner.Server/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
}
```

---

## 🗄️ Database Migration

### Create the initial migration

Run the following from the **solution root**:

```sh
cd .\ShoppingList.NathanJenner.Server
dotnet ef migrations add InitialCreate
```

This generates migration files under `ShoppingList.NathanJenner.Server/Migrations/`.

### Apply migrations

Migrations are automatically applied when the application starts via `db.Database.Migrate()` in `Program.cs`. Alternatively, apply them manually:

```sh
cd .\ShoppingList.NathanJenner.Server
dotnet ef database update
```

### Adding future migrations

After modifying any model in `Models/`, create a new migration:

```sh
cd .\ShoppingList.NathanJenner.Server
dotnet ef migrations add YourMigrationName
```

---

## 🚀 Running the Application

### Option 1 — Visual Studio

Open `ShoppingList.NathanJenner.sln` and press **F5**. The SPA proxy will automatically start the Vite dev server alongside the API.

### Option 2 — CLI

Install frontend dependencies:

```sh
cd ShoppingList.NathanJenner/ClientApp
npm install
```

Start the API application:

```sh
cd .\ShoppingList.NathanJenner.Server
dotnet run
```


Start the frontend application

```sh
cd .\ShoppingList.NathanJenner.client
npm run dev
```


> Note: Ensure SQL Server is running and the connection string is correctly set in `appsettings.json` before starting the backend.

---

## 📍 Host & Port

The app will be available at `https://localhost:63709`.

---

## 🌱 Seed Data

On first run, if the database is empty, the following items are automatically seeded:

| Name  | Quantity |
|-------|----------|
| Milk  | 2        |
| Bread | 1        |
| Eggs  | 12       |

---

## 📡 API Endpoints

Base URL: `/api/items`

| Method   | Endpoint          | Description                  |
|----------|-------------------|------------------------------|
| `GET`    | `/`               | Get all items (sorted)       |
| `POST`   | `/`               | Create a new item            |
| `PUT`    | `/{id}`           | Update an item               |
| `DELETE` | `/{id}`           | Delete an item               |
| `PATCH`  | `/{id}/toggle`    | Toggle purchased state       |
| `PUT`    | `/reorder`        | Reorder items by sort order  |

OpenAPI documentation is available in development at `https://localhost:7299/openapi/v1.json`.

---

## 📁 Project Structure

```
ShoppingList.NathanJenner/
├── ShoppingList.NathanJenner.Server/          # ASP.NET Core API
│   ├── Data/
│   │   └── AppDbContext.cs                    # EF Core DbContext
│   ├── Migrations/                            # EF Core migration files
│   ├── Models/
│   │   ├── Dtos.cs                            # CreateItemDto, UpdateItemDto, ReorderItemDto
│   │   └── ShoppingItem.cs                    # ShoppingItem entity
│   ├── appsettings.json                       # App configuration & connection string
│   ├── Program.cs                             # Minimal API endpoints & app startup
│   └── ShoppingList.NathanJenner.Server.http  # HTTP test requests
│
└── shoppinglist.nathanjenner.client/          # React + TypeScript frontend (Vite)
    ├── src/
    │   ├── api/
    │   │   └── itemsApi.ts                    # Typed fetch client for all API endpoints
    │   ├── components/
    │   │   ├── AddItemForm/
    │   │   │   └── AddItemForm.tsx            # Form to add a new shopping item
    │   │   ├── DraggableItemList/
    │   │   │   └── DraggableItemList.tsx      # Drag-and-drop sortable item list
    │   │   ├── EditItemDialog/
    │   │   │   └── EditItemDialog.tsx         # Modal dialog to edit an item
    │   │   └── ShoppingItemRow/
    │   │       └── ShoppingItemRow.tsx        # Individual item row with actions
    │   ├── hooks/
    │   │   └── useItems.ts                    # State management & API integration hook
    │   ├── types/
    │   │   └── item.ts                        # ShoppingItem & ShoppingItemInput types
    │   ├── App.tsx                            # Root application component
    │   ├── App.css                            # Global styles
    │   └── main.tsx                           # React entry point
    ├── public/                                # Static assets
    ├── vite.config.ts                         # Vite + HTTPS + API proxy config
    └── package.json                           # Frontend dependencies

