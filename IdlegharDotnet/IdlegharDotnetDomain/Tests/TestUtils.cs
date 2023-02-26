using System.Runtime.Serialization.Formatters.Binary;

namespace IdlegharDotnetDomain.Tests
{
    public static class TestUtils
    {
        /***
        *** Fast, Cheap and dirty way of cloning an object, for testing purposes
        *** ¡¡¡ DO NOT USE IN PRODUCTION !!!
        ***/
        public static object? DeepClone(object? obj)
        {
            if (obj == null) return null;

            object? objResult = null;

#pragma warning disable SYSLIB0011
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, obj);

                ms.Position = 0;
                objResult = bf.Deserialize(ms);
            }
#pragma warning restore SYSLIB0011

            return objResult;
        }

    }
}