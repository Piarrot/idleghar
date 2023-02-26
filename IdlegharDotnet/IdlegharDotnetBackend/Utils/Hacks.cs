using System.Runtime.Serialization.Formatters.Binary;

namespace IdlegharDotnetBackend.Utils
{
    public class Hacks
    {
        /***
        *** Fast, Cheap and dirty way of cloning an object, for testing purposes
        *** ¡¡¡ DO NOT USE IN PRODUCTION !!!
        ***/
        public static T? TEMP_DO_NOT_USE_DeepClone<T>(T? obj)
        {
            if (obj == null) return default(T);

            var serialized = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}