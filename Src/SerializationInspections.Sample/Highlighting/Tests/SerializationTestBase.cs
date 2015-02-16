using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializationInspections.Sample.Highlighting.Tests
{
    public abstract class SerializationTestBase
    {
        protected static T SerializeAndDeserialize<T>(T input)
        {
            var binaryFormatter = new BinaryFormatter();

            using (var ms = new MemoryStream())
            {
                binaryFormatter.Serialize(ms, input);

                ms.Seek(0, SeekOrigin.Begin);
                return (T)binaryFormatter.Deserialize(ms);
            }
        }
    }
}