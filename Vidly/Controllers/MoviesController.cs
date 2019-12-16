using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        public ActionResult Index()
        {
            var movies = GetMovies();

            return View(movies);
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

        public ActionResult Edit(int id)
        {
            return Content("id = " + id);
        }

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
            return new List<Movie>
            {
                new Movie { Id = 1, Name = "Shrek" },
                new Movie { Id = 2, Name = "Wall-e" }
            };
        }
    }
}