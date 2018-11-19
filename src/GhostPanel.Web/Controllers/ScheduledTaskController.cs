using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GhostPanel.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduledTaskController : ControllerBase
    {

        private readonly IRepository _repository;

        public ScheduledTaskController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/ScheduledTask
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ScheduledTask/5
        [HttpGet("{id}", Name = "GetScheduledTask")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ScheduledTask
        [HttpPost]
        public ScheduledTask Post(ScheduledTask task)
        {
            _repository.Create(task);
            return task;
        }

        // PUT: api/ScheduledTask/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
