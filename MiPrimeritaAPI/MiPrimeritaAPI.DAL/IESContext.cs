using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MiPrimeritaAPI.DAL.Tables;

namespace MiPrimeritaAPI.DAL
{
    public class IESContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected readonly IConfiguration Configuration;

        public IESContext() {}

        public IESContext(IConfiguration configuration)
        {

            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to mysql with connection string from app settings
            //var connectionString = Configuration.GetConnectionString("ConexionAPidgey");
            options.UseMySql("server=localhost;port=3306;database=PrimeraApi;user=root;password=root;", 
                ServerVersion.AutoDetect("server=localhost;port=3306;database=PrimeraApi;user=root;password=root;"));
        }
    }
}
