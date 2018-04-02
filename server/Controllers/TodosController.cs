using System;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

using Server.Models;
using Server.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string databaseUri = ConfigurationManager.AppSettings["DatabaseURI"];
            var mongoService = new MongoDBService("net-core-todos", "Todos", databaseUri);
            var allTodos = await mongoService.GetAllTodos();

            return Ok(allTodos);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public void Get(int id)
        {
        }

        // POST api/<controller>
        [HttpPost]
        public async Task Post([FromBody]TodoModel todo)
        {
            string databaseUri = ConfigurationManager.AppSettings["DatabaseURI"];
            var mongoService = new MongoDBService("net-core-todos", "Todos", databaseUri);
            await mongoService.AddTodo(todo);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}