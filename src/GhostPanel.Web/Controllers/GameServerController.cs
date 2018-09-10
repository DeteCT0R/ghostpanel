using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GhostPanel.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameServerController : Controller
    {
        private readonly IRepository _repository;
        private readonly IBackgroundService _backgroundService;

        public GameServerController(IRepository repository, IBackgroundService backgroundService)
        {
            _repository = repository;
            _backgroundService = backgroundService;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<GameServer> Get()
        {
            IEnumerable<GameServer> test = _repository.List<GameServer>();
            return test;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public GameServer Get(int id)
        {
            _backgroundService.AddTask(new TestTask());
            var result = _repository.Single(DataItemPolicy<GameServer>.ById(id));
            return result;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post(GameServer gameServer)
        {
            _repository.Create(new List<GameServer>() { gameServer });
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
