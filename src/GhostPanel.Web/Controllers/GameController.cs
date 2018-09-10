using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GhostPanel.Web.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {

        private readonly IRepository _repository;

        public GameController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Game> Get()
        {
            IEnumerable<Game> result = _repository.List<Game>();
            return result;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Game Get(int id)
        {
            Game result = _repository.Single(DataItemPolicy<Game>.ById(id));
            return result;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
