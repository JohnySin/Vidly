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
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };

            //var viewResult = new ViewResult();
            //viewResult.ViewData.Model;

            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }

        public ActionResult MoviesList()
        {
            var moviesList = GetMovies();

            return View(moviesList);
        }

        public ActionResult Details(int id)
        {
            var movie = GetMovies().Where(m => m.Id == id).SingleOrDefault();

            return View(movie);
        }

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie { Id = 1, Name = "Shrek" },
                new Movie { Id = 2, Name = "Matrix" },
                new Movie { Id = 3, Name = "Pirates of the caribbean" }
            };
        }


        /*
         * This is a custom route - The recomended way of routing
         * Lecture Correction: it should be ^\\d{2}$ and not \\d{2} as Mosh says 
         * ^ and $ mark the start and the end of string
         */
        [Route("movies/released/{year:range(2015, 2016)}/{month:regex(^\\d{2}$):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}