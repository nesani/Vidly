using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext dbContext;
        public MoviesController()
        {
            dbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var movies = GetMovies();

            return View(movies);
        }

        public ActionResult New()
        {

            var viewModal = new NewMovieViewModal
            {
                Genre = GetGenres()
            };

            return View("MovieForm", viewModal);
        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                dbContext.Movies.Add(movie);
            }
            else
            {
                var movieInDB = dbContext.Movies.Single(m => m.Id == movie.Id);

                movieInDB.DateAdded = DateTime.Now;
                movieInDB.Genre = movie.Genre;
                movieInDB.Name = movie.Name;
                movieInDB.ReleaseDate = movie.ReleaseDate;
                movieInDB.Stock = movie.Stock;

            }
            dbContext.SaveChanges();

            return RedirectToAction("Index", "Movies");

        }

        public ActionResult Edit(int id)
        {
            var movie = GetMovies().SingleOrDefault(c => c.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var viewModel = new NewMovieViewModal()
            {
                Movie = movie,
                Genre = GetGenres()
            };


            return View("MovieForm", viewModel);
        }


        // GET: Movies
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek" };
            var customers = new List<Customer>()
            {
                new Customer { Id = 1, Name = "Nenad" },
                new Customer { Id = 2, Name = "Goran" },
                new Customer { Id = 3, Name = "Vuk" }
            };

            var model = new RandomMovieViewModel()
            {
                Movie = movie,
                Customer = customers
            };

            //ViewData["Movie"] = movie;
            //ViewBag.Movie = movie;

            return View(model);
            //return Content("Hello World");
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new {page = 1, sortBy ="name" });
        }

        //public ActionResult Edit(int id)
        //{
        //    return Content("id = " + id);
        //}

        // movies
        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //    {
        //        pageIndex = 1;
        //    }

        //    if (string.IsNullOrWhiteSpace(sortBy))
        //    {
        //        sortBy = "Name";
        //    }

        //    return Content(string.Format($"pageIndex={pageIndex}&sortBy={sortBy}"));
        //}

        [Route("movies/released/{year:regex(\\d{4}):range(2000,2020)}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        private IEnumerable<Movie> GetMovies()
        {
            return dbContext.Movies;
        }


        public ActionResult Details (int id)
        {
            var movies = GetMovies().FirstOrDefault( m => m.Id == id);

            return View(movies);
        }

        private List<string> GetGenres()
        {
            var genres = new List<string>()
            {
                "Historical",
                "Action",
                "Popular",
                "Sci-Fi",
                "Comedy"
            };

            return genres;
        }
    }
}