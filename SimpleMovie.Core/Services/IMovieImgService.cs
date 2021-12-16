using SimpleMovie.Core.Model;
using System.Linq;
using SimpleMovie.Core.Services.Options;

namespace SimpleMovie.Core.Services
{
    public interface IMovieImgService
    {
        MovieImg CreateMovieImg(
            CreateMovieImgOptions options);

        IQueryable<MovieImg> SearchMovieImgs(
            SearchMovieImgOptions options);

        MovieImg UpdateMovieImg(
            UpdateMovieImgOptions options);

    }
}
