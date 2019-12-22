using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

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

        public ActionResult New()
        {

            var memtypes = GetMembershipTypes();
            var viewModel = new CustomerForViewModel()
            {
                MembershipTypes = memtypes
            };
            return View("CustomerForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var customer = GetCustomers().SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerForViewModel()
            {
                customer = customer,
                MembershipTypes = dbContext.MembershipTypes
            };


            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.Id == 0)
            {
                dbContext.Customers.Add(customer);
            }
            else
            {
                var customerInDB = dbContext.Customers.Single(m => m.Id == customer.Id);

                customerInDB.Name = customer.Name;
                customerInDB.Birthdate = customer.Birthdate;
                customerInDB.MembershipTypeId = customer.MembershipTypeId;
                customerInDB.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;

            }
            dbContext.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Details(int id)
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

        private IEnumerable<MembershipType> GetMembershipTypes()
        {
            return dbContext.MembershipTypes;
        }

    }
}