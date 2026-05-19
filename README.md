# Hogwarts School of Witchcraft and Wizardry — Official Website

An institutional website for Hogwarts School, built as if the school were a real-world educational institution. Presents the school's history, academic staff, courses, houses, announcements, and events. Data is persisted in a SQLite database and served via ASP.NET Core MVC with Razor views. A password-protected admin panel allows full CRUD management of all dynamic content.

---

## Table of Contents

1. [Overview](#overview)
2. [Tech Stack](#tech-stack)
3. [Architecture](#architecture)
4. [Database Schema](#database-schema)
5. [Project Structure](#project-structure)
6. [Admin Panel](#admin-panel)
7. [Local Setup](#local-setup)
8. [Seed Data](#seed-data)

---

## Overview

### Public pages

| Page | Route | Data Source |
|---|---|---|
| Home | `/` | Latest 3 announcements + next 3 upcoming events |
| About & History | `/About` | Static |
| Houses | `/Houses` | Static |
| Academic Staff | `/Staff` | `Staff` table — headmaster pinned to top |
| Courses | `/Courses` | `Courses` table — grouped by category, instructor resolved from `Staff` |
| Announcements | `/Announcements` | `Announcements` table — latest 6 full cards + older entries in archive section |
| Events | `/Events` | `Events` table — split into upcoming and past relative to today |
| Admissions | `/Admissions` | Static |

### Admin pages

| Page | Route |
|---|---|
| Login | `/admin/login` |
| Dashboard | `/admin` |
| Announcements | `/admin/announcements` |
| Events | `/admin/events` |
| Staff | `/admin/staff` |
| Courses | `/admin/courses` |

---

## Tech Stack

| Layer | Technology | Version |
|---|---|---|
| Framework | ASP.NET Core MVC | .NET 10 |
| ORM | Entity Framework Core | 10.0.7 |
| Database | SQLite | via EF Core |
| Styling | CSS3 + Bootstrap 5 | Bootstrap 5.3.3 (CDN) |
| Icons | Bootstrap Icons | 1.11.3 (CDN) |
| Version Control | Git + GitHub | — |

---

## Architecture

The application follows the standard ASP.NET Core MVC pattern. Each request is routed to a controller, the controller queries the database via EF Core, passes the result to a Razor view, and the view renders the HTML response.

```
┌─────────────────────────────────────────────────────────────┐
│                          BROWSER                            │
└─────────────────────────┬───────────────────────────────────┘
                          │ HTTP Request
┌─────────────────────────▼───────────────────────────────────┐
│                  ASP.NET Core MVC (.NET 10)                  │
│                                                             │
│  Public site                  Admin panel                   │
│  ─────────────────────        ───────────────────────────   │
│  HomeController               AdminController (dashboard)   │
│  StaffController              AdminAnnouncementsController  │
│  CoursesController            AdminEventsController         │
│  AnnouncementsController      AdminStaffController          │
│  EventsController             AdminCoursesController        │
│  Static: About/Houses/        AdminAccountController        │
│          Admissions           (login / logout)              │
│                                                             │
│  Session auth: AdminAuthFilter protects all /admin routes   │
└─────────────────────────┬───────────────────────────────────┘
                          │ EF Core / SQLite provider
┌─────────────────────────▼───────────────────────────────────┐
│                     SQLite (hogwarts.db)                     │
│    Staff  |  Courses  |  Announcements  |  Events           │
└─────────────────────────────────────────────────────────────┘
```

### Request flow — public page

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

### Request flow — admin (authenticated)

```
HTTP GET /admin/announcements
  → AdminAuthFilter checks Session["AdminLoggedIn"] == "true"
      → not set: redirect to /admin/login
      → set: continue
  → AdminAnnouncementsController.Index()
      → _db.Announcements.OrderByDescending(...).ToListAsync()
      → return View(items)
  → Views/AdminAnnouncements/Index.cshtml renders management table
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
| `PhotoPath` | TEXT | nullable | Filename only, e.g. `Severus-Snape.webp` — resolved under `wwwroot/assets/images/` |
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
| `Category` | TEXT | NOT NULL | `Academic` / `Events` / `Safety` / `Sports` |
| `PublishedDate` | TEXT | NOT NULL | Stored as ISO-8601 by SQLite |

The Announcements view takes the first 6 records (ordered by date descending) as latest notices and renders the remainder in a collapsible archive section.

### Events

| Column | Type | Constraints | Notes |
|---|---|---|---|
| `Id` | INTEGER | PK, auto-increment | — |
| `Title` | TEXT | NOT NULL | — |
| `Description` | TEXT | NOT NULL | — |
| `Location` | TEXT | NOT NULL | e.g. `The Great Hall`, `Quidditch Pitch` |
| `EventDate` | TEXT | NOT NULL | Stored as ISO-8601 by SQLite |

The Events view splits records into **Upcoming** (`EventDate >= today`) and **Past** (`EventDate < today`) sections. Past events are rendered at reduced opacity.

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
│   ├── HomeController.cs                — Latest announcements + upcoming events → HomeViewModel; Error action
│   ├── AboutController.cs               — Static page; no DB query
│   ├── HousesController.cs              — Static page; no DB query
│   ├── StaffController.cs               — All staff; headmaster sorted first
│   ├── CoursesController.cs             — Courses + instructor resolution from Staff
│   ├── AnnouncementsController.cs       — All announcements ordered by date descending
│   ├── EventsController.cs              — All events; today passed via ViewData for view-side split
│   ├── AdmissionsController.cs          — Static page; no DB query
│   ├── AdminAccountController.cs        — Login / logout (route: /admin/login, /admin/logout)
│   ├── AdminController.cs               — Dashboard with entity counts (route: /admin)
│   ├── AdminAnnouncementsController.cs  — CRUD (route: /admin/announcements)
│   ├── AdminEventsController.cs         — CRUD (route: /admin/events)
│   ├── AdminStaffController.cs          — CRUD + photo upload (route: /admin/staff)
│   └── AdminCoursesController.cs        — CRUD (route: /admin/courses)
│
├── Filters/
│   └── AdminAuthFilter.cs               — IAuthorizationFilter; checks Session["AdminLoggedIn"]
│                                          AdminAuthAttribute wraps it as a TypeFilterAttribute
│
├── Data/
│   ├── AppDbContext.cs                  — EF Core DbContext; registers all four tables
│   └── SeedData.cs                      — Inserts all seed records on first run (idempotent per-table check)
│
├── Migrations/
│   ├── 20260511221023_InitialCreate.cs
│   ├── 20260511221023_InitialCreate.Designer.cs
│   └── AppDbContextModelSnapshot.cs
│
├── Models/
│   ├── Announcement.cs
│   ├── Course.cs
│   ├── HomeViewModel.cs                 — LatestAnnouncements + UpcomingEvents
│   ├── SchoolEvent.cs
│   └── StaffMember.cs
│
├── Views/
│   ├── _ViewImports.cshtml              — @using HogwartsWebsite.Models + tag helpers
│   ├── _ViewStart.cshtml                — Default layout: _Layout
│   ├── Shared/
│   │   ├── _Layout.cshtml               — Public site layout (navbar, footer, CDN links)
│   │   └── _AdminLayout.cshtml          — Admin panel layout (sidebar, topbar)
│   ├── Home/
│   │   ├── Index.cshtml                 — Carousel, news panel, quick links
│   │   └── Error.cshtml                 — 500 / error page
│   ├── About/Index.cshtml
│   ├── Houses/Index.cshtml
│   ├── Staff/Index.cshtml
│   ├── Courses/Index.cshtml
│   ├── Announcements/Index.cshtml
│   ├── Events/Index.cshtml
│   ├── Admissions/Index.cshtml
│   ├── AdminAccount/
│   │   └── Login.cshtml                 — Standalone login page (Layout = null)
│   ├── Admin/
│   │   └── Index.cshtml                 — Dashboard: entity count cards + quick links
│   ├── AdminAnnouncements/              — Index, Create, Edit, Delete
│   ├── AdminEvents/                     — Index, Create, Edit, Delete
│   ├── AdminStaff/                      — Index, Create, Edit (with photo preview), Delete
│   └── AdminCourses/                    — Index, Create, Edit, Delete
│
├── wwwroot/
│   ├── css/
│   │   └── style.css                    — All custom styles (public site + admin panel)
│   ├── js/
│   │   └── main.js                      — Shared scripts placeholder
│   └── assets/images/                   — Staff photos and site imagery (.jpg / .webp / .png)
│
├── appsettings.json                     — DB connection string + Admin:Username / Admin:Password
├── appsettings.Development.json
├── Program.cs                           — DI, EF Core, session, migration + seed on startup, middleware
├── HogwartsWebsite.csproj               — .NET 10 web SDK; EF Core 10.0.7 packages
└── .gitignore
```

---

## Admin Panel

The admin panel is protected by session-based authentication. ASP.NET Core Identity is not used.

### Credentials

Stored in `appsettings.json` under the `Admin` key:

```json
"Admin": {
  "Username": "admin",
  "Password": "hogwarts2026"
}
```

### Access

Navigate to `/admin/login` (or click the **Admin** link in the site footer).

### Features

| Module | Operations |
|---|---|
| Announcements | List · Create · Edit · Delete |
| Events | List · Create · Edit · Delete |
| Staff | List · Create · Edit · Delete · Photo upload |
| Courses | List · Create · Edit · Delete |

Staff photo uploads are saved directly to `wwwroot/assets/images/`. On edit, if no new file is selected the existing `PhotoPath` value is preserved via a hidden form field.

All POST actions are protected with `[ValidateAntiForgeryToken]`.

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

All seed records are inserted by `Data/SeedData.cs` on first run. Each table is checked with `.Any()` before insertion, so re-running the app does not duplicate data.

| Table | Records | Details |
|---|---|---|
| `Staff` | 11 | 1 Headmaster (Dumbledore) + 10 staff including Snape, McGonagall, Flitwick, Sprout, Hagrid, Trelawney, Binns, Vector, Hooch, Merrythought |
| `Courses` | 26 | 8 Core · 5 Elective · 3 Advanced Elective · 3 Other · 7 Extra-Curricular |
| `Announcements` | 14 | 6 current notices (Apr–May 2026) + 8 archive entries (Sep 2025–Apr 2026) |
| `Events` | 7 | Sorting Ceremony through End of Year Feast; academic year 2025–2026 |
