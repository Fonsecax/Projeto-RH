using Microsoft.EntityFrameworkCore;
using RH.API.Models;

namespace RH.API.Contexts
{
    public class RHContext : DbContext 
    {
        public RHContext(DbContextOptions<RHContext>options) : base(options) { }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
    }
}
