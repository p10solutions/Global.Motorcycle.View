using Confluent.Kafka;
using System.Text;
using System.Text.Json;

namespace Global.Motorcycle.View.Infraestructure.Deserializer
{
    public class GuidDeserializer : IDeserializer<Guid>
    {
        public Guid Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            var json = Encoding.ASCII.GetString(data.ToArray());

            return JsonSerializer.Deserialize<Guid>(json);
        }
    }
}
