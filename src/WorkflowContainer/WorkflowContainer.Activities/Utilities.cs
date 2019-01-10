using Newtonsoft.Json;

namespace WorkflowContainer.Activities
{
    public static class Utilities
    {
        public static string ToJson<T>(this T data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
