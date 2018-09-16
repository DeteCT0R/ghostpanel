using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Core.Data.Model
{
    public class RequestResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public DataEntity payload { get; set; }
    }
}
