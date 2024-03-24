using Microsoft.EntityFrameworkCore;

namespace Appointment.Data
{
    public class ApplicationDbContext : DbContext
    {
        //IConfiguration appConfig;

        public DbSet<Entities.Appointment> Appointments { get; set; }

        //public ApplicationDbContext(IConfiguration config)
        //{
        //    appConfig = config;
        //}

        ////Constructor calling the Base DbContext Class Constructor
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        //{
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Appointments;Trusted_Connection=True;");
        }
    }
}
