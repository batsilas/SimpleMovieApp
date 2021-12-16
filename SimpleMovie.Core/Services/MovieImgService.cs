using SimpleMovie.Core.Data;
using SimpleMovie.Core.Model;
using SimpleMovie.Core.Services.Options;
using System.Linq;

namespace SimpleMovie.Core.Services
{
    public class MovieImgService : IMovieImgService
    {
        private MovieDbContext context;
        private IMovieService MovieService;
        public MovieImgService(
            MovieDbContext contextByProgram,
            IMovieService custService)
        {
            context = contextByProgram;
            MovieService = custService;
        }

        public MovieImg CreateMovieImg(
            CreateMovieImgOptions options)
        {
            var Movie = MovieService.GetMovieById(
                new GetMovieByIdOptions()
                {
                    MovieId = options.MovieId
                });
            if (Movie == null)
            {
                return null;
            }
            var MovieImg = new MovieImg()
            {
                ByteArray = options.ByteArray
            };

            Movie.MovieImgs.Add(MovieImg);

            context.Add(MovieImg);

            if (context.SaveChanges() > 0)
            {
                return MovieImg;
            }

            return null;

        }

        public IQueryable<MovieImg> SearchMovieImgs(
            SearchMovieImgOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var query = context
                .Set<MovieImg>()
                .AsQueryable();

            if (options.MovieImgId != null)
            {
                query = query.Where(ord => ord.Id == options.MovieImgId);
            }

            return query;
        }


        public MovieImg UpdateMovieImg(
            UpdateMovieImgOptions options)
        {

            if (options == null)
            {
                return null;
            }

            var MovieImg = SearchMovieImgs(
                new SearchMovieImgOptions()
                {
                    MovieImgId = options.MovieImgId
                }).SingleOrDefault();

            context.SaveChanges();

            return MovieImg;
        }


    }
}
