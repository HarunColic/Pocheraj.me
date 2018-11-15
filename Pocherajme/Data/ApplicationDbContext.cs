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
            //optionsBuilder.UseSqlServer("Server=localhost;Database=pocherajme;Trusted_Connection=True;MultipleActiveResultSets=true;User ID=cola;Password=sifra123");
            optionsBuilder.UseSqlServer("Server = tcp:pocheraj.database.windows.net,1433; Initial Catalog = pocherajme; Persist Security Info = False; User ID = pocheras; Password = Klinka123$; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
        }
    }
}
