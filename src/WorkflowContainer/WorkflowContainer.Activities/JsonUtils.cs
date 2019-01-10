using Newtonsoft.Json;

namespace WorkflowContainer.Activities
{
    public static class JsonUtils
    {
        public static string ToJson<T>(this T data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public static T ToObject<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T Clone<T>(T obj)
        {
            var json = obj.ToJson();
            return json.ToObject<T>();
        }

        public static TOut Cast<TOut>(object obj)
        {
            var json = obj.ToJson();
            return json.ToObject<TOut>();
        }
    }
}
