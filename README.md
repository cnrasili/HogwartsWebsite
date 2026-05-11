# Hogwarts School of Witchcraft and Wizardry — Official Website

An institutional website for Hogwarts School, built as if the school were a real-world educational institution. Presents the school's history, academic staff, courses, houses, announcements, and events. Data is persisted in a SQLite database and served via ASP.NET Core MVC with Razor views.

---

## Table of Contents

1. [Overview](#overview)
2. [Tech Stack](#tech-stack)
3. [Architecture](#architecture)
4. [Database Schema](#database-schema)
5. [Project Structure](#project-structure)
6. [Local Setup](#local-setup)
7. [Seed Data](#seed-data)

---

## Overview

| Page | Route | Data Source |
|---|---|---|
| Home | `/` | Latest 3 announcements + next 3 upcoming events |
| About & History | `/About` | Static |
| Houses | `/Houses` | Static |
| Academic Staff | `/Staff` | `Staff` table — headmaster pinned to top |
| Courses | `/Courses` | `Courses` table — grouped by category, instructor resolved from `Staff` |
| Announcements | `/Announcements` | `Announcements` table — sorted by date descending |
| Events | `/Events` | `Events` table — sorted by date ascending |
| Admissions | `/Admissions` | Static |

---

## Tech Stack

| Layer | Technology | Version |
|---|---|---|
| Framework | ASP.NET Core MVC | .NET 10 |
| ORM | Entity Framework Core | 10.0.7 |
| Database | SQLite | via EF Core |
| Styling | CSS3 + Bootstrap 5 | Bootstrap 5 (CDN) |
| Icons | Bootstrap Icons | CDN |
| Interactivity | Vanilla JavaScript | — |
| Version Control | Git + GitHub | — |

---

## Architecture

The application follows the standard ASP.NET Core MVC pattern. Each request is routed to a controller, the controller queries the database via EF Core, passes the result to a Razor view, and the view renders the HTML response.

```
┌─────────────────────────────────────────────────────────┐
│                        BROWSER                          │
└───────────────────────────┬─────────────────────────────┘
                            │ HTTP Request
┌───────────────────────────▼─────────────────────────────┐
│                ASP.NET Core MVC (.NET 10)                │
│                                                         │
│  Routing → Controller Action → EF Core Query → View     │
│                                                         │
│  HomeController          → HomeViewModel                │
│  StaffController         → IEnumerable<StaffMember>     │
│  CoursesController       → IEnumerable<Course>          │
│                             + ViewData["Instructors"]    │
│  AnnouncementsController → IEnumerable<Announcement>    │
│  EventsController        → IEnumerable<SchoolEvent>     │
│                                                         │
│  Static pages: About, Houses, Admissions                │
└───────────────────────────┬─────────────────────────────┘
                            │ EF Core / SQLite provider
┌───────────────────────────▼─────────────────────────────┐
│                    SQLite (hogwarts.db)                  │
│   Staff  |  Courses  |  Announcements  |  Events        │
└─────────────────────────────────────────────────────────┘
```

### Request flow

```
HTTP GET /Courses
  → CoursesController.Index()
      → _db.Courses.OrderBy(...).ToListAsync()
      → _db.Staff.Where(s => s.Subject != null).ToListAsync()
      → builds Dictionary<string, string> instructors via Subject/Name matching
      → ViewData["Instructors"] = instructors
      → return View(courses)
  → Views/Courses/Index.cshtml renders course cards with instructor names
  → HTML response
```

---

## Database Schema

EF Core manages the schema via migrations. The database file (`hogwarts.db`) is created automatically on first run.

### Staff

Stores all teaching and non-teaching staff. `IsHeadmaster = true` pins the record to the top of the Staff page. `Subject` is matched against course names to resolve the instructor display on the Courses page.

| Column | Type | Constraints | Notes |
|---|---|---|---|
| `Id` | INTEGER | PK, auto-increment | — |
| `FullName` | TEXT | NOT NULL | e.g. `Severus Snape` |
| `Title` | TEXT | NOT NULL | `Professor` / `Madam` / etc. |
| `Subject` | TEXT | nullable | e.g. `Potions`, `Flying & Quidditch Referee` |
| `Bio` | TEXT | NOT NULL | — |
| `PhotoPath` | TEXT | nullable | Filename only, e.g. `Severus-Snape.webp` |
| `IsHeadmaster` | INTEGER | NOT NULL | `1` = pinned to top of staff page |

### Courses

26 courses across five categories. `Category` drives the section grouping on the Courses page. Instructor resolution is done at query time in the controller — there is no FK to `Staff`.

| Column | Type | Constraints | Notes |
|---|---|---|---|
| `Id` | INTEGER | PK, auto-increment | — |
| `Name` | TEXT | NOT NULL | e.g. `Transfiguration` |
| `Description` | TEXT | NOT NULL | — |
| `YearLevels` | TEXT | NOT NULL | e.g. `Years 1–7`, `Year 6 (age 17+)` |
| `Category` | TEXT | NOT NULL | `Core` / `Elective` / `Advanced Elective` / `Other` / `Extra-Curricular` |

### Announcements

| Column | Type | Constraints | Notes |
|---|---|---|---|
| `Id` | INTEGER | PK, auto-increment | — |
| `Title` | TEXT | NOT NULL | — |
| `Body` | TEXT | NOT NULL | — |
| `Category` | TEXT | NOT NULL | `Academic` / `Events` / `Safety` / `Sports` / `Notice` |
| `PublishedDate` | TEXT | NOT NULL | Stored as ISO-8601 by SQLite |

### Events

| Column | Type | Constraints | Notes |
|---|---|---|---|
| `Id` | INTEGER | PK, auto-increment | — |
| `Title` | TEXT | NOT NULL | — |
| `Description` | TEXT | NOT NULL | — |
| `Location` | TEXT | NOT NULL | e.g. `The Great Hall`, `Quidditch Pitch` |
| `EventDate` | TEXT | NOT NULL | Stored as ISO-8601 by SQLite |

### Relationships

There are no foreign keys between tables. The `Staff.Subject` ↔ `Course.Name` relationship is resolved in application code using a bidirectional `Contains` check:

```csharp
s.Subject.Contains(course.Name, OrdinalIgnoreCase) ||
course.Name.Contains(s.Subject, OrdinalIgnoreCase)
```

This handles compound subject strings such as `Transfiguration & Deputy Headmistress` → `Transfiguration`, and `Advanced Arithmancy Studies` → `Arithmancy`.

---

## Project Structure

```
HogwartsWebsite/
│
├── Controllers/
│   ├── HomeController.cs           — Queries latest announcements + upcoming events → HomeViewModel
│   ├── AboutController.cs          — Static page; no DB query
│   ├── HousesController.cs         — Static page; no DB query
│   ├── StaffController.cs          — Queries all staff; headmaster sorted first
│   ├── CoursesController.cs        — Queries courses + resolves instructor from Staff
│   ├── AnnouncementsController.cs  — Queries all announcements sorted by date descending
│   ├── EventsController.cs         — Queries all events sorted by date ascending
│   └── AdmissionsController.cs     — Static page; no DB query
│
├── Data/
│   ├── AppDbContext.cs             — EF Core DbContext; registers Announcements, Events, Staff, Courses
│   └── SeedData.cs                 — Inserts all seed records on first run (idempotent per-table check)
│
├── Migrations/
│   ├── 20260511221023_InitialCreate.cs          — Creates all four tables
│   ├── 20260511221023_InitialCreate.Designer.cs — EF Core snapshot metadata
│   └── AppDbContextModelSnapshot.cs             — Current model snapshot
│
├── Models/
│   ├── Announcement.cs             — Announcement entity
│   ├── Course.cs                   — Course entity
│   ├── HomeViewModel.cs            — Composite model: LatestAnnouncements + UpcomingEvents
│   ├── SchoolEvent.cs              — SchoolEvent entity
│   └── StaffMember.cs              — StaffMember entity
│
├── Views/
│   ├── _ViewImports.cshtml         — Global @using HogwartsWebsite.Models and tag helpers
│   ├── _ViewStart.cshtml           — Sets _Layout as default for all views
│   ├── Shared/
│   │   └── _Layout.cshtml          — Shared navbar, footer, Bootstrap + BI CDN links
│   ├── Home/
│   │   └── Index.cshtml            — Hero, latest announcements strip, upcoming events strip
│   ├── About/
│   │   └── Index.cshtml            — School history, timeline, castle locations
│   ├── Houses/
│   │   └── Index.cshtml            — Four house cards (Gryffindor, Hufflepuff, Ravenclaw, Slytherin)
│   ├── Staff/
│   │   └── Index.cshtml            — Headmaster card + staff grid with photo, title, subject
│   ├── Courses/
│   │   └── Index.cshtml            — Five category sections; each card shows name, year levels, instructor, description
│   ├── Announcements/
│   │   └── Index.cshtml            — Announcement list with category badge and date
│   ├── Events/
│   │   └── Index.cshtml            — Event list with date, location
│   └── Admissions/
│       └── Index.cshtml            — Admissions information; static
│
├── wwwroot/
│   ├── css/
│   │   └── style.css               — All custom styles; no inline styles used anywhere in the project
│   ├── js/
│   │   └── main.js                 — Vanilla JS (navbar behaviour, minor UI)
│   └── images/                     — Staff photos (.jpg / .webp)
│
├── appsettings.json                — DefaultConnection: "Data Source=hogwarts.db"
├── appsettings.Development.json    — Development overrides
├── Program.cs                      — DI registration, EF Core setup, migration + seed on startup, middleware pipeline
├── HogwartsWebsite.csproj          — .NET 10 web SDK; EF Core 10.0.7 packages
└── .gitignore
```

---

## Local Setup

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### 1. Clone the repository

```bash
git clone https://github.com/cnrasili/HogwartsWebsite.git
cd HogwartsWebsite
```

### 2. Run the application

```bash
dotnet run
```

The app starts at `http://localhost:5016`.

On first run, `Program.cs` automatically:
1. Applies EF Core migrations — creates `hogwarts.db` in the project root.
2. Calls `SeedData.Initialize()` — inserts all seed records into empty tables.

No manual database setup or environment variables are required.

---

## Seed Data

All seed records are inserted by `Data/SeedData.cs` on the first run. Each table is checked with `.Any()` before insertion, so re-running the app does not duplicate data.

| Table | Records | Details |
|---|---|---|
| `Staff` | 10 | 1 Headmaster (Dumbledore), 9 professors/staff including Snape, McGonagall, Flitwick, Sprout, Hagrid, Trelawney, Binns, Vector, Hooch |
| `Courses` | 26 | 8 Core · 5 Elective · 3 Advanced Elective · 3 Other · 6 Extra-Curricular |
| `Announcements` | 6 | Categories: Academic (×2), Sports, Safety, Events (×2) |
| `Events` | 7 | Sorting Ceremony through End of Year Feast; academic year 2025–2026 |
