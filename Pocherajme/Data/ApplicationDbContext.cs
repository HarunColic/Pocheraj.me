using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pocherajme.Models;

namespace Pocherajme.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, AppRole, int>
    {

        public DbSet<Application> Applications { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostImages> PostImages { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<TransportType> TransportTypes { get; set; }
        public DbSet<UserMeta> UserMetas { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=195.222.54.107\\sql2016;Integrated Security=True;Initial Catalog=pocherajme;User ID=p1829;Password=mV3RgtGGG");
        }
    }
}
