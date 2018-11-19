using GhostPanel.Core.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GhostPanel.Core.Data.Specifications
{
    public class ScheduledTaskPolicy : DataItemPolicy<ScheduledTask>
    {
        protected ScheduledTaskPolicy(Expression<Func<ScheduledTask, bool>> expression) : base(expression)
        {
        }

        public static ScheduledTaskPolicy ByServerId(int id)
        {
            return new ScheduledTaskPolicy(x => x.GameServerId == id);
        }
    }
}
