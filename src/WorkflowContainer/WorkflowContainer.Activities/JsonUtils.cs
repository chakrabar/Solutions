using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

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
            if (obj is TOut)
                return (TOut)obj;
            if (obj is string && (obj as string).TryParse(out JToken jToken)) //if obj is already JSON formatted string
                return jToken.ToObject<TOut>();
            var json = obj.ToJson(); //if obj data has same structure, but not actually the required type
            return json.ToObject<TOut>();
        }

        public static bool TryParse(this string json, out JToken token)
        {
            token = default(JToken);
            try
            {
                json = json.Replace("\\\"", "'"); //change all escaped-double-quote with single-quote
                json = json.Trim('"'); //remove leading & trailing quotes if added while formatting
                token = JToken.Parse(json);
                return true;
            }
            catch (System.Exception ex)
            {
                Trace.TraceError($"Could not cast object. Exception: " + ex.ToString());
                return false;
            }
        }
    }
}
