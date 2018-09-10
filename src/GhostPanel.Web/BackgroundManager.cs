using GhostPanel.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostPanel.Web
{
    public class BackgroundManager
    {
        public BackgroundManager(IRepository repository)
        {
            _repository = repository;
        }

        private readonly IRepository _repository;
    }

    public async Task Run()
    {

    }

    public async Task Stop()
    {

    }
}
