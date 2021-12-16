using Microsoft.EntityFrameworkCore;
using SimpleMovie.Core.Model;

namespace SimpleMovie.Core.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
        {
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieImg> MovieImgs { get; set; }
        
    }
}
