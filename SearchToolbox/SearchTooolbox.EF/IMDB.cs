using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace SearchTooolbox.EF
{
    public partial class IMDB : DbContext
    {
        public IMDB() : base(@"Server=(local);Database=IMDB;Trusted_Connection=True;MultipleActiveResultSets=True;")
        { }

        public virtual DbSet<Title> Titles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            SqlProviderServices instance = SqlProviderServices.Instance;

            modelBuilder.Entity<Title>()
                .Property(e => e.Code)
                .IsUnicode(false);
        }
    }
}
