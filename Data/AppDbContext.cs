using Microsoft.EntityFrameworkCore;
using HogwartsWebsite.Models;

namespace HogwartsWebsite.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Announcement> Announcements => Set<Announcement>();
    public DbSet<SchoolEvent> Events => Set<SchoolEvent>();
    public DbSet<StaffMember> Staff => Set<StaffMember>();
    public DbSet<Course> Courses => Set<Course>();
}
