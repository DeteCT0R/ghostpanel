using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhostPanel.Communication.Mediator.Commands;
using GhostPanel.Communication.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public StatsController(ILogger logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ServerStatsWrapper> Get(int id)
        {
            var result = await _mediator.Send(new QueryServerCommand(id));
            return result;
        }
    }
}