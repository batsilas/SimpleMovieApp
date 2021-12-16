using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpleMovie.Core.Data;
using SimpleMovie.Core.Model;
using SimpleMovie.Core.Services;
using SimpleMovie.Core.Services.Options;
using System.Web;

namespace SimpleMovie.Controllers
{
    public class MoviesController : Controller
    {
        private MovieDbContext _context;
        private IMovieService movieService_;
        public MoviesController(MovieDbContext context)
        {
            _context = context;
            movieService_ = new MovieService(_context);
        }
        // GET: Movies
        public IActionResult Index()
        {
            var movieList = movieService_
                .SearchMovies(new SearchMovieOptions())
                .ToList();

            return View(movieList);
        }

        // GET: Movies/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var movie = movieService_
              .SearchMovies(new SearchMovieOptions()
              {
                  MovieId = id
              }).FirstOrDefault();

            if (movie == null)
                return NotFound();

            GetMovieByIdOptions options = new GetMovieByIdOptions()
            {
                MovieId = movie.Id,
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Description = movie.Description,
                Genre = movie.Genre,
                Rating = movie.Rating
            };

            return View(options);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateMovieOptions options)
        {
            if (ModelState.IsValid)
            {
                movieService_.CreateMovie(new CreateMovieOptions(){
                    Title = options.Title,
                    ReleaseDate = options.ReleaseDate,
                    Description = options.Description,
                    Genre = options.Genre,
                    Rating = options.Rating
                });
                return RedirectToAction(nameof(Index));
            }

            return View(options);
        }
       
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var movie = movieService_
               .SearchMovies(new SearchMovieOptions() {
                   MovieId = id
               }).FirstOrDefault();


            if (movie == null)
                return NotFound();

            UpdateMovieOptions options = new UpdateMovieOptions() { 
                MovieId = movie.Id,
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Description = movie.Description,
                Genre = movie.Genre,
                Rating = movie.Rating
            };

            return View(options);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateMovieOptions options)
        {
            if (options.MovieId == 0)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var updated = movieService_.UpdateMovie(options);
                    if (!updated)
                        throw new DbUpdateConcurrencyException();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(options.MovieId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(options);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var movie = movieService_
                  .SearchMovies(new SearchMovieOptions()
                  {
                      MovieId = id
                  }).FirstOrDefault();


            if (movie == null)
                return NotFound();

            DeleteMovieOptions options = new DeleteMovieOptions()
            {
                MovieId = id,
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Description = movie.Description,
                Genre = movie.Genre,
                Rating = movie.Rating
            };

            return View(options);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(DeleteMovieOptions options)
        {
            if (options.MovieId == null)
                return NotFound();

            if (MovieExists(options.MovieId))
            {
                var deleted = movieService_.DeleteMovie(new DeleteMovieOptions()
                {
                    MovieId = options.MovieId
                });
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int? id)
        {
            return movieService_.MovieExists(id);
        }
    }
}
