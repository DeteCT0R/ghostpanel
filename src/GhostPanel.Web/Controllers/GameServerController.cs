using System.Collections.Generic;
using System.IO;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;
using GhostPanel.Core.GameServerUtils;
using GhostPanel.Core.Managment;
using GhostPanel.Core.Providers;
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
        private readonly IDefaultDirectoryProvider _defaultDirs;

        public GameServerController(IRepository repository, ILoggerFactory logger, IGameServerManager serverManager, IDefaultDirectoryProvider defaultDirs)
        {
            _repository = repository;
            _defaultDirs = defaultDirs;
            _serverManager = serverManager;
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
        public GameServer Get(int id)
        {
            _logger.LogInformation("Attempting for get server with ID {id}", id);
            var result = _repository.Single(DataItemPolicy<GameServer>.ById(id));
            return result;
        }


        [HttpGet("{id:int}/{command}")]
        public GameServerManager Get(int id, string command)
        {

            _logger.LogInformation("Running GameServer action with ID {id} and action {action}", id, command);
            var gameServer = _repository.Single(DataItemPolicy<GameServer>.ById(id));
            if (gameServer != null)
            {

                switch (command.ToLower())
                {
                    case "start":
                        _serverManager.StartServer(gameServer);
                        break;
                    case "stop":
                        _serverManager.StopServer(gameServer);
                        break;
                    case "restart":
                        _serverManager.RestartServer(gameServer);
                        break;
                    case "reinstall":
                        _serverManager.InstallGameServer(gameServer);
                        break;
                }
                
            }

            return null;
        }

        // POST api/<controller>
        [HttpPost]
        public RequestResponse Post(GameServer gameServer)
        {
            gameServer.Status = ServerStatusStates.Installing;
            _repository.Create(gameServer);
            gameServer.HomeDirectory = Path.Combine(_defaultDirs.GetBaseInstallDirectory(), gameServer.Id.ToString());
            _repository.Update(gameServer);
            _serverManager.InstallGameServer(gameServer);
            
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
            var gameServer = _repository.Single(DataItemPolicy<GameServer>.ById(id));
            if (gameServer != null)
            {
                _serverManager.DeleteGameServer(gameServer);
            }
            
        }
    }
}

