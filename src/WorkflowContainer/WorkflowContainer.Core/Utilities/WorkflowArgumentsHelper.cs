using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WorkflowContainer.Core.Utilities
{
    static class WorkflowArgumentsHelper
    {
        public static Dictionary<string, object> DeserializeArguments(DynamicActivity workflow,
            IDictionary<string, object> arguments, bool shouldThrow = true)
        {
            if (workflow == null || workflow.Properties.Count == 0 || arguments == null)
            {
                return new Dictionary<string, object>();
            }

            var inputArgs = new Dictionary<string, object>();
            foreach (var argument in arguments.Where(a => a.Value != null))
            {
                var key = argument.Key;
                try
                {
                    if (workflow.Properties.Contains(key))
                    {
                        var argumentType = workflow.Properties[key].Type.GetGenericArguments()[0];
                        var argumentValueType = argument.Value.GetType();
                        if (argumentValueType != argumentType && !argumentType.IsAssignableFrom(argumentValueType))
                        {
                            JToken jtokenObject = JsonConvert.DeserializeObject<JToken>(JsonConvert.SerializeObject(argument.Value));
                            var typeInstance = jtokenObject.ToObject(argumentType);
                            inputArgs.Add(key, typeInstance);
                        }
                        else
                        {
                            inputArgs.Add(key, argument.Value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError("Error while parsing workflow arguments. Exception: " + ex.ToString());
                    if (shouldThrow)
                    {
                        throw new Exception($"Invalid argument for {key}, {ex.Message}");
                    }
                }
            }
            return inputArgs;
        }
    }
}
