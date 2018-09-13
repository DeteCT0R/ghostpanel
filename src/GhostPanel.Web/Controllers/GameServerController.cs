using System.Collections.Generic;
using System.Linq;
using GhostPanel.Core;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.GameServerUtils;
using GhostPanel.Management.Server;
using GhostPanel.Web.Background;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        private ILogger _logger;

        public GameServerController(IRepository repository, 
            IBackgroundService backgroundService, 
            ServerManagerContainer serverManagerContainer,
            ILogger<GameServerController> logger)
        {
            _repository = repository;
            _backgroundService = backgroundService;
            _serverManagerContainer = serverManagerContainer;
            _logger = logger;
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
            _logger.LogInformation("Running GET with ID {id}", id);
            //var result = _repository.Single(DataItemPolicy<GameServer>.ById(id));
            var result = _serverManagerContainer.GetManagerList().Where(s => s.gameServer.Id == id).SingleOrDefault();
            return result;
        }

        [HttpGet("{id:int}/{command}")]
        public GameServerManager Get(int id, string command)
        {
            _logger.LogInformation("Running GameServer action with ID {id} and action {action}", id, command);
            var result = _serverManagerContainer.GetManagerList().Where(s => s.gameServer.Id == id).SingleOrDefault();
            if (result != null)
            {
                if (command.ToLower() == "start")
                {
                    result.StartServer();
                } else if (command.ToLower() == "stop")
                {
                    result.StopServer();
                } else if (command.ToLower() == "restart")
                {
                    result.StopServer();
                    result.StartServer();
                } else
                {
                    _logger.LogError("Unknown command {command}", command);
                }
                
            }
            return result;
        }

        // POST api/<controller>
        [HttpPost]
        public GameServer Post(GameServer gameServer)
        {            
            _repository.Create(new List<GameServer>() { gameServer });
            GameServerManager manager = new GameServerManager(gameServer, new SteamCmd("anonymous", ""), _repository, _logger);
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

