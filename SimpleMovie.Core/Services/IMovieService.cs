using SimpleMovie.Core.Model;
using SimpleMovie.Core.Services.Options;
using System.Linq;

namespace SimpleMovie.Core.Services
{
    public interface IMovieService
    {
        Movie CreateMovie(
            CreateMovieOptions options);

        IQueryable<Movie> SearchMovies(
            SearchMovieOptions options);
        bool UpdateMovie(
            UpdateMovieOptions options);
        bool DeleteMovie(
            DeleteMovieOptions options);

        Movie GetMovieById(
            GetMovieByIdOptions options);

        bool MovieExists(int? id);
    }
}
