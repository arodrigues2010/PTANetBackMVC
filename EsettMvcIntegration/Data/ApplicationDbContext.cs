using EsettMvcIntegration.Models;
using Microsoft.EntityFrameworkCore;

namespace EsettMvcIntegration.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FeeDataModel> FeeData { get; set; }
    }
}
