using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // Step 1
        private ApplicationDbContext _context;

        // Step 2
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // Step 3
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var moviesList = _context.Movies.Include(g => g.Genre).ToList();

            return View(moviesList);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(g => g.Genre).SingleOrDefault(m => m.Id == id);

            return View(movie);
        }


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