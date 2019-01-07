using System;
using System.Collections.Generic;

namespace WorkflowContainer.Core
{
    public class WorkflowResult
    {
        public Guid InstanceId { get; set; }
        public IDictionary<string, object> Output { get; set; }
        public WorkflowStatus Status { get; set; }
    }
}
