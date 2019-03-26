using Microsoft.EntityFrameworkCore;

namespace SearchTooolbox.EF
{
    public class IMDB : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(local);Database=IMDB;Trusted_Connection=True;MultipleActiveResultSets=True;");
        }
        public virtual DbSet<Title> Titles { get; set; }
    }
}
