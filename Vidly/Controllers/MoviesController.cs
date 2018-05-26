using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity.Validation;

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
            var moviesList = _context.Movies.Include(m => m.Genre).ToList();

            return View(moviesList);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            return View(movie);
        }

        public ActionResult New()
        {
            var viewModel = new MovieFormViewModel()
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Movie Movie)
        {
            if (Movie.Id == 0)
            {
                _context.Movies.Add(Movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(c => c.Id == Movie.Id);

                movieInDb.Name = Movie.Name;
                movieInDb.DateReleased = Movie.DateReleased;
                movieInDb.Genre.Name = Movie.Genre.Name;
                movieInDb.NumberInStock = Movie.NumberInStock;
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           

            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Edit(int Id)
        {
            var movie = _context.Movies.SingleOrDefault(g => g.Id == Id);

            if (null == movie)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel()
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
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