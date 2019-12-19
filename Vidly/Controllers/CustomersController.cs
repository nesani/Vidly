using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext dbContext;
        public CustomersController()
        {
            dbContext = new ApplicationDbContext();
        }
        // GET: Customers
        public ActionResult Index()
        {
            var customer = GetCustomers();

            return View(customer);
        }

        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();
        }

        public ActionResult Details (int id)
        {
            var customer = GetCustomers().SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return dbContext.Customers.Include(c => c.MembershipType);
        }

    }
}