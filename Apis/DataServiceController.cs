using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCorePostgreSQLDockerApp.Models;
using AspNetCorePostgreSQLDockerApp.Repository;
using Microsoft.AspNetCore.Http;

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
        public async Task<ActionResult> Customers()
        {
            var customers = await _repo.GetCustomersAsync();
            if (customers == null) {
              return NotFound();
            }
            return Ok(customers);
        }

        // GET api/dataservice/customers/5
        [HttpGet("customers/{id}")]
        public async Task<ActionResult> Customers(int id)
        {
            var customer = await _repo.GetCustomerAsync(id);
            if (customer == null) {
              return NotFound();
            }
            return Ok(customer);
        }

        // POST api/values
        [HttpPost("customers")]
        public async Task<ActionResult> PostCustomer([FromBody]Customer customer)
        {
          if (!ModelState.IsValid) {
            return BadRequest(this.ModelState);
          }

          var newCustomer = await _repo.InsertCustomerAsync(customer);
          if (newCustomer == null) {
            return BadRequest("Unable to insert customer");
          }
          var uri = Request.ToUri().ToString() + "/" + newCustomer.Id.ToString();
          return Created(uri, newCustomer);
        }

        // PUT api/dataservice/customers/5
        [HttpPut("customers/{id}")]
        public async Task<ActionResult> PutCustomer(int id, [FromBody]Customer customer)
        {
          if (!ModelState.IsValid) {
            return BadRequest(this.ModelState);
          }

          var status = await _repo.UpdateCustomerAsync(customer);
          if (!status) {
            return NoContent();
          }
          return Ok(status);
        }

        // DELETE api/dataservice/customers/5
        [HttpDelete("customers/{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
          var status = await _repo.DeleteCustomerAsync(id);
          if (!status) {
            return NotFound();
          }
          return Ok(status);
        }

        [HttpGet("states")]
        public async Task<ActionResult> States() {
          var states = await _repo.GetStatesAsync();
          if (states == null) {
            return NotFound();
          }
          return Ok(states);
        }

    }

    public static class HttpRequestExtensions
    {
        public static Uri ToUri(this HttpRequest request)
        {
            var hostComponents = request.Host.ToUriComponent().Split(':');

            var builder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = hostComponents[0],
                Path = request.Path,
                Query = request.QueryString.ToUriComponent()
            };

            if (hostComponents.Length == 2)
            {
                builder.Port = Convert.ToInt32(hostComponents[1]);
            }

            return builder.Uri;
        }
    }
}
