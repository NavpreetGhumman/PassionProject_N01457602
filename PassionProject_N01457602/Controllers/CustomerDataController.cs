using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProject_N01457602.Models;
using System.Diagnostics;

namespace PassionProject_N01457602.Controllers
{
    public class CustomerDataController : ApiController
    {
        //This variable is our database access point
        private ApplicationDbContext db = new ApplicationDbContext();

        
            
            

            //This code is mostly scaffolded from the base models and database context
            //New > WebAPIController with Entity Framework Read/Write Actions
            //Choose model "Customer"
            //Choose context "ApplicationDb Context"
            //Note: The base scaffolded code needs many improvements for a fully
            //functioning MVP.


            /// <summary>
            /// Gets a list or Customers in the database alongside a status code (200 OK).
            /// </summary>
            /// <returns>A list of Customers including their ID, name, and Email.</returns>
            /// <example>
            /// GET: api/CustomerData/GetCustomers
            /// </example>
            [ResponseType(typeof(IEnumerable<CustomerDto>))]
            public IHttpActionResult GetCustomers()
            {
                List<Customer> Customers = db.Customers.ToList();
                List<CustomerDto>CustomerDtos = new List<CustomerDto> { };

                //Here you can choose which information is exposed to the API
                foreach (var Customer in Customers)
                {
                    CustomerDto NewCustomer = new CustomerDto
                    {
                       CustomerID = Customer.CustomerID,
                       CustomerName =Customer.CustomerName,
                       CustomerEmail = Customer.CustomerEmail,
                       CustomerPhone = Customer.CustomerPhone
                    };
                    CustomerDtos.Add(NewCustomer);
                }
                 return Ok(CustomerDtos);
            }


        /// <summary>
        /// Finds a particular Customer in the database with a 200 status code. If the customer is not found, return 404.
        /// </summary>
        /// <param name="id">The Customer id</param>
        /// <returns>Information about the Customer, including id, name, email nad phone</returns>
        // <example>
        // GET: api/CustomerData/FindCustomer/5
        // </example>
        [HttpGet]
        [ResponseType(typeof(CustomerDto))]
        public IHttpActionResult FindCustomer(int id)
        {
            //Find the data
            Customer Customer = db.Customers.Find(id);
            //if not found, return 404 status code.
            if (Customer == null)
            {
                return NotFound();
            }

            //put into a 'friendly object format'
            CustomerDto CustomerDto = new CustomerDto
            {
                CustomerID = Customer.CustomerID,
                CustomerName = Customer.CustomerName,
                CustomerEmail = Customer.CustomerEmail,
                CustomerPhone = Customer.CustomerPhone
            };


            //pass along data as 200 status code OK response
            return Ok(CustomerDto);
        }




        /// <summary>
        /// Updates a Customer in the database given information about the Cusomer
        /// </summary>
        /// <param name="id">The customer id</param>
        /// <param name="customer">A customer object. Received as POST data.</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/CustomerData/UpdateCustomer/5
        /// FORM DATA: customer JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateCustomer(int id, [FromBody] Customer Customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Customer.CustomerID)
            {
                return BadRequest();
            }

            db.Entry(Customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        /// <summary>
        /// Adds a Customer to the database.
        /// </summary>
        /// <param name="customer">A Customer object. Sent as POST form data.</param>
        /// <returns>status code 200 if successful. 400 if unsuccessful</returns>
        /// <example>
        /// POST: api/CustomerData/AddCustomer
        ///  FORM DATA: Customer JSON Object
        /// </example>
        [ResponseType(typeof(Customer))]
        [HttpPost]
        public IHttpActionResult AddCustomer([FromBody] Customer Customer)
        {
            //Will Validate according to data annotations specified on model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(Customer);
            db.SaveChanges();

            return Ok(Customer.CustomerID);
        }

        /// <summary>
        /// Deletes a Customer in the database
        /// </summary>
        /// <param name="id">The id of the Customer to delete.</param>
        /// <returns>200 if successful. 404 if not successful.</returns>
        /// <example>
        /// POST: api/CustomerData/DeleteCustomer/5
        /// </example>
        [HttpPost]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer Customer = db.Customers.Find(id);
            if (Customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(Customer);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Finds a Customer in the system. Internal use only.
        /// </summary>
        /// <param name="id">The Customer id</param>
        /// <returns>TRUE if the Customer exists, false otherwise.</returns>
        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.CustomerID == id) > 0;
        }
    }
}