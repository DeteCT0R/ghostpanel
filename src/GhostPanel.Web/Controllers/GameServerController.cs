using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GhostPanel.Core.Commands;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;
using GhostPanel.Core.GameServerUtils;
using GhostPanel.Core.Managment;
using GhostPanel.Core.Providers;
using MediatR;
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
        private readonly IMediator _mediator;

        public GameServerController(IRepository repository,
            ILoggerFactory logger,
            IGameServerManager serverManager,
            IDefaultDirectoryProvider defaultDirs,
            IMediator mediator)
        {
            _repository = repository;
            _defaultDirs = defaultDirs;
            _mediator = mediator;
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
        public async Task<CommandResponseBase> Get(int id, string command)
        {

            _logger.LogInformation("Running GameServer action with ID {id} and action {action}", id, command);

            var gameServer = _repository.Single(DataItemPolicy<GameServer>.ById(id));
            
            if (gameServer != null)
            {

                switch (command.ToLower())
                {
                    case "start":
                        return await _mediator.Send(new RestartServerCommand(id));
                    case "stop":
                        return await _mediator.Send(new StopServerCommand(id));
                    case "restart":
                        return await _mediator.Send(new RestartServerCommand(id));

                    default:
                        var result =  new CommandResponseBase()
                        {
                            status = CommandResponseStatusEnum.Error,
                            message = $"Unknown command {command}"
                        };
                        return result;
                }
                
            }

            var errorResult = new CommandResponseBase()
            {
                status = CommandResponseStatusEnum.Error,
                message = $"Unable to locate server with ID {id}"
            };

            return errorResult;
        }

        // POST api/<controller>
        [HttpPost]
        public RequestResponse Post(GameServer gameServer)
        {
            _mediator.Send(new CreateServerCommand(gameServer));

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
                _serverManager.RemoveGameServer(gameServer);
            }
            
        }
    }
}

