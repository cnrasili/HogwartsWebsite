# Hogwarts School of Witchcraft and Wizardry — Official Website

An institutional website for Hogwarts School, built as if the school were a real-world educational institution. Presents the school's history, academic staff, courses, houses, announcements, and events.

## Technology Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core MVC (.NET 10) |
| Database | SQLite via Entity Framework Core 10 |
| Styling | CSS3 + Bootstrap 5 (CDN) |
| Icons | Bootstrap Icons (CDN) |
| Interactivity | Vanilla JavaScript |
| Version Control | Git + GitHub |

## Pages

| Page | Route |
|---|---|
| Home | `/` |
| About & History | `/About` |
| Houses | `/Houses` |
| Academic Staff | `/Staff` |
| Courses | `/Courses` |
| Announcements | `/Announcements` |
| Events | `/Events` |
| Admissions | `/Admissions` |

## Project Structure

```
HogwartsWebsite/
├── Controllers/
├── Data/
│   ├── AppDbContext.cs
│   └── SeedData.cs
├── Migrations/
├── Models/
├── Views/
│   └── Shared/
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── images/
├── Program.cs
├── appsettings.json
└── HogwartsWebsite.csproj
```

## Running Locally

Requires [.NET 10 SDK](https://dotnet.microsoft.com/download).

```
dotnet run
```

The app starts at `http://localhost:5016`. The database is created and seeded automatically on first run.

## License

See [LICENSE](LICENSE).
