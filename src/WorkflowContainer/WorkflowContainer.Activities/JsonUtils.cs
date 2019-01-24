using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            if (obj == null)
                return default(TOut);
            if (typeof(TOut) == typeof(string)) //if expected type is string
                return (TOut)obj;
            if (obj is string && (obj as string).TryParse(out JToken jToken)) //if obj is already JSON formatted string
                return jToken.ToObject<TOut>();
            var json = obj.ToJson();
            return json.ToObject<TOut>();
        }

        public static bool TryParse(this string json, out JToken token)
        {
            token = default(JToken);
            try
            {
                json = json.Trim('"'); //remove leading & trailing quotes if added while formatting
                token = JToken.Parse(json);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
