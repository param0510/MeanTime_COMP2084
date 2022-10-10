using MeanTime.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeanTime.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<App>? Apps { get; set; }
        public DbSet<Genre>? Genres { get; set; }
        public DbSet<AppDetail>? AppDetails { get; set; }
    }
}