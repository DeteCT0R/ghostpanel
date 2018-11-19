using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GhostPanel.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameServerConfigController : ControllerBase
    {
        private readonly IRepository _repository;

        public GameServerConfigController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/GameServerConfig
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/GameServerConfig/5
        [HttpGet("{id}", Name = "GetGameServerConfig")]
        public IEnumerable<GameServerConfigFile> GetById(int id)
        {
            var result = _repository.List(GameServerConfigFilePolicy.ByServerId(id));
            return result;
        }

        // POST: api/GameServerConfig
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/GameServerConfig/5
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
