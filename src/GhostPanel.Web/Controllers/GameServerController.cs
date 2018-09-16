using System.Collections.Generic;
using System.Linq;
using GhostPanel.Core;
using GhostPanel.Core.Background;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.GameServerUtils;
using GhostPanel.Core.Managment;
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
        private ILogger _logger;
        private readonly ILoggerFactory _logFactory;
        private readonly IGameServerManager _serverManager;

        public GameServerController(IRepository repository, ServerManagerContainer serverManagerContainer, ILoggerFactory logger, IGameServerManager serverManager)
        {
            _repository = repository;
            _logger = logger.CreateLogger<GameServerController>();
            _logFactory = logger;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<GameServer> Get()
        {
            IEnumerable<GameServer> servers = _repository.List<GameServer>();
            return servers;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public GameServerManager Get(int id)
        {
            _logger.LogInformation("Attempting for get server with ID {id}", id);
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

                switch (command.ToLower())
                {
                    case "start":
                        result.StartServer();
                        break;
                    case "stop":
                        result.StopServer();
                        break;
                    case "restart":
                        result.StopServer();
                        result.StartServer();
                        break;
                    case "reinstall":
                        result.InstallGameServer();
                        break;
                }
                
            }
            return result;
        }

        // POST api/<controller>
        [HttpPost]
        public RequestResponse Post(GameServer gameServer)
        {

            
            return new RequestResponse()
            {
                message = "Game server now installing",
                status = "Success",
                payload = gameServer
            };
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
            _logger.LogInformation("Attempting for get server with ID {id}", id);
            var manager = _serverManagerContainer.GetManagerList().Where(s => s.gameServer.Id == id).SingleOrDefault();
            // TODO - Error check on return
            manager.DeleteGameServer();
            _serverManagerContainer.RemoveServerManager(manager);
        }
    }
}

