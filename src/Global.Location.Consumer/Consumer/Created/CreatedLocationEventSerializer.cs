using Confluent.Kafka;
using Global.Motorcycle.View.Application.Features.Locations.Commands.SaveLocation;
using System.Text;
using System.Text.Json;

namespace Global.Location.Consumer.Consumer.Created
{
    internal class CreatedLocationEventSerializer : IDeserializer<CreatedLocationEvent>
    {
        public CreatedLocationEvent Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            var json = Encoding.ASCII.GetString(data.ToArray());

            return JsonSerializer.Deserialize<CreatedLocationEvent>(json);
        }
    }
}
