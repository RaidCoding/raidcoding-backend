using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RaidCoding.Data.Models;

namespace RaidCoding.Data;

public class RcDbContext(DbContextOptions<RcDbContext> options)
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RcDbContext).Assembly);

        modelBuilder.Entity<User>().ToTable("Users");
    }
}