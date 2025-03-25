using Barbearia.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.API.DBContext
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options) {}

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Agendamento> Agendamentos { get; set; }

        public DbSet<Servico> Servicos { get; set; }

    }
}
