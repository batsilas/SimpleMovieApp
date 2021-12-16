using SimpleMovie.Core.Model;
using System.Linq;
using SimpleMovie.Core.Services.Options;
using SimpleMovie.Core.Data;

namespace SimpleMovie.Core.Services
{
    public class MovieService : IMovieService
    {
        private MovieDbContext context;

        public MovieService(MovieDbContext contextByProgram)
        {
            context = contextByProgram;
        }
        public Movie CreateMovie(
            CreateMovieOptions options)
        {
            if (options == null)
            {
                return null;
            }
            var Movie = new Movie()
            {
                Title = options.Title,
                ReleaseDate = options.ReleaseDate,
                Description = options.Description,
                Genre = options.Genre,
                Rating = options.Rating
            };

            context.Add(Movie);

            if (context.SaveChanges() > 0)
            {
                return Movie;
            }

            return null;
        }

        public IQueryable<Movie> SearchMovies(
            SearchMovieOptions options)
        {

            if (options == null)
            {
                return null;
            }

            var query = context
                .Set<Movie>()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.Title))
            {
                query = query.Where(m => m.Title == options.Title);
            }

            if (options.ReleaseDate != System.DateTime.MinValue)
            {
                query = query.Where(m => m.ReleaseDate == options.ReleaseDate);
            }

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                query = query.Where(m => m.Description == options.Description);
            }

            if (!string.IsNullOrWhiteSpace(options.Genre))
            {
                query = query.Where(m => m.Genre == options.Genre);
            }

            if (options.Rating != null)
            {
                query = query.Where(m => m.Rating == options.Rating);
            }

            if (options.MovieId != null)
            {
                query = query.Where(m => m.Id == options.MovieId.Value);
            }

            query = query.Take(500);

            return query;

        }

        public bool UpdateMovie(
            UpdateMovieOptions options)
        {

            if (options == null || options.MovieId == null)
            {
                return false;
            }

            var Movie = SearchMovies(new SearchMovieOptions()
            {
                MovieId = options.MovieId
            }).SingleOrDefault();

            if (!string.IsNullOrWhiteSpace(options.Title))
            {
                Movie.Title = options.Title;
            }

            if (options.ReleaseDate != System.DateTime.MinValue)
            {
                Movie.ReleaseDate = options.ReleaseDate;
            }
            
            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                Movie.Description = options.Description;
            }

            if (!string.IsNullOrWhiteSpace(options.Genre))
            {
                Movie.Genre = options.Genre;
            }

            if (options.Rating != null)
            {
                Movie.Rating = options.Rating;
            }

            if (context.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public bool DeleteMovie(
          DeleteMovieOptions options)
        {

            if (options == null || options.MovieId == null)
            {
                return false;
            }

            var Movie = SearchMovies(new SearchMovieOptions()
            {
                MovieId = options.MovieId
            }).SingleOrDefault();

            context.Movies.Remove(Movie);
            if (context.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public Movie GetMovieById(
            GetMovieByIdOptions options)
        {

            if (options == null)
            {
                return null;
            }

            var Movie = context
                .Set<Movie>()
                .Where(m => m.Id == options.MovieId)
                .SingleOrDefault();
            if (Movie != null)
            {
                return Movie;
            }
            return null;
        }

        public bool MovieExists(int? id) {
            if(id != null)
                return context.Movies.Any(e => e.Id == id);

            return false;
        }
    }
}
