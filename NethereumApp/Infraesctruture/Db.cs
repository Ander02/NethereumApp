using Microsoft.EntityFrameworkCore;

namespace NethereumApp.Infraestructure
{
    public class Db : DbContext
    {
        #region Tables

        #endregion

        public Db(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder m)
        {

        }
    }
}
