

namespace AirportInformations.Common.Extensions
{
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;
    public static class SerializationExtensions
    {
        public static string ToJsonFormat<T>(this T objectToFormat)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));

            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, objectToFormat);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public static T ToObjectModelList<T>(this string jsonData)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData)))
            {
                var objectModelList = (T)serializer.ReadObject(stream);
                return objectModelList;
            }
        }
    }
}
