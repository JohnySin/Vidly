using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            // This "Include" method goes with "using System.Data.Entity".
            // By default Entity Framework ONLY loads the customer objects, NOT their related objects.
            // So we need to inlude manually the related objects...that's what "Include" does. This is called "Eager Loading"
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (null == customer)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult New()
        {
            var viewModel = new CustomerFormViewModel()
            {
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer Customer)
        {
            if (Customer.Id == 0)
            {
                _context.Customers.Add(Customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == Customer.Id);

                // This works but it is not the best method. Opens security holes.
                // TryUpdateModel(customerInDb);

                // This is a better method...classic method actually :D
                customerInDb.Name = Customer.Name;
                customerInDb.BirthDate = Customer.BirthDate;
                customerInDb.MembershipTypeId = Customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = Customer.IsSubscribedToNewsletter;

                // To avoid the above lines use AutoMapper tool..it goes like this:
                // Mapper.Map(customer, customerInDb);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int Id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == Id);

            if (null == customer)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}