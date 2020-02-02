using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DTO;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {

        private ApplicationDbContext dbContext;

        public CustomersController()
        {
            dbContext = new ApplicationDbContext();
        }

        //Get /api/customers
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return dbContext.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        // GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = dbContext.Customers.SingleOrDefault(i => i.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        //POST /Api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();

            customerDto.Id = customer.Id;

            return Created( new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        //PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);

            dbContext.SaveChanges();


        }

        //DELETE api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            dbContext.Customers.Remove(customerInDb);
            dbContext.SaveChanges();
        }
    }
}
