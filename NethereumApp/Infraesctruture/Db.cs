using Microsoft.EntityFrameworkCore;
using NethereumApp.Domain;

namespace NethereumApp.Infraestructure
{
    public class Db : DbContext
    {
        #region Tables
        public DbSet<EthereumContractInfo> EthereumContractInfo { get; set; }
        #endregion

        public Db(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder m)
        {
            m.Entity<EthereumContractInfo>().ToTable(nameof(EthereumContractInfo));
            m.Entity<EthereumContractInfo>().HasKey(e => e.Id);
        }
    }
}
