using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhostPanel.Core;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;
using GhostPanel.Core.GameServerUtils;
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
        private readonly ServerManagerContainer _serverManagerContainer;

        public GameServerController(IRepository repository, IBackgroundService backgroundService, ServerManagerContainer serverManagerContainer)
        {
            _repository = repository;
            _backgroundService = backgroundService;
            _serverManagerContainer = serverManagerContainer;
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
        public GameServerManager Get(int id)
        {
            //var result = _repository.Single(DataItemPolicy<GameServer>.ById(id));
            var result = _serverManagerContainer.GetManagerList().Where(s => s.gameServer.Id == id).SingleOrDefault();
            return result;
        }

        // POST api/<controller>
        [HttpPost]
        public GameServer Post(GameServer gameServer)
        {            
            _repository.Create(new List<GameServer>() { gameServer });
            GameServerManager manager = new GameServerManager(gameServer, new SteamCmd("anonymous", ""), _repository);
            _serverManagerContainer.AddServerManager(manager);
            CreateServerTask task = new CreateServerTask(manager);
            _backgroundService.AddTask(task);
            return gameServer;
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
