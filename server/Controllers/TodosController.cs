using System;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;

using Server.Models;
using Server.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {

        private static string databaseUri = ConfigurationManager.AppSettings["DatabaseURI"];
        private MongoDBService mongoService = new MongoDBService("net-core-todos", "Todos", databaseUri);

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var allTodos = await mongoService.GetAllTodos();
                return Ok(allTodos);
            }

            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var todo = await mongoService.GetTodo(id);
                return Ok(todo);
            }

            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task Post([FromBody]TodoModel todo)
        {
            try
            {
                await mongoService.AddTodo(todo);
                var message = new { Message = "Todo Added!" };
                Ok(message);
            }

            catch (ApplicationException ex)
            {
                BadRequest(ex.Message);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task Update(string id, [FromBody]TodoModel todo)
        {
            try
            {
                await mongoService.UpdateTodo(id, todo);
                var message = new { Message = "Todo Updated!" };
                Ok(message);
            }

            catch (ApplicationException ex)
            {
                BadRequest(ex.Message);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            try
            {
                await mongoService.DeleteTodo(id);
                var message = new { Message = "Todo Deleted!" };
                Ok(message);
            }

            catch (ApplicationException ex)
            {
                BadRequest(ex.Message);
            }
        }
    }
}