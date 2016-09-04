using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCorePostgreSQLDockerApp.Models;
using AspNetCorePostgreSQLDockerApp.Repository;

namespace AspNetCorePostgreSQLDockerApp.Apis
{
    [Route("api/[controller]")]
    public class DataServiceController : Controller
    {
        ICustomersRepository _repo;

        public DataServiceController(ICustomersRepository repo) {
          _repo = repo;
        }

        // GET api/dataservice/customers
        [HttpGet("customers")]
        public async Task<List<Customer>> Customers()
        {
            return await _repo.GetCustomersAsync();
        }

        // GET api/dataservice/customers/5
        [HttpGet("customers/{id}")]
        public async Task<Customer> Customers(int id)
        {
            return await _repo.GetCustomerAsync(id);
        }

        // POST api/values
        [HttpPost("customers")]
        public async Task<Customer> PostCustomer([FromBody]Customer customer)
        {
          return await _repo.InsertCustomerAsync(customer);
        }

        // PUT api/dataservice/customers/5
        [HttpPut("customers/{id}")]
        public async Task<bool> PutCustomer(int id, [FromBody]Customer customer)
        {
          return await _repo.UpdateCustomerAsync(customer);
        }

        // DELETE api/dataservice/customers/5
        [HttpDelete("customers/{id}")]
        public async Task<bool> DeleteCustomer(int id)
        {
          return await _repo.DeleteCustomerAsync(id);
        }

        [HttpGet("states")]
        public async Task<List<State>> States() {
          return await _repo.GetStatesAsync();
        }

    }
}
