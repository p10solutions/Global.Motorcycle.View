using Confluent.Kafka;
using Global.Motorcycle.View.Application.Features.Locations.Commands.SaveLocation;
using System.Text;
using System.Text.Json;

namespace Global.Location.Consumer.Consumer.Updated
{
    internal class UpdatedLocationEventSerializer : IDeserializer<UpdatedLocationEvent>
    {
        public UpdatedLocationEvent Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            var json = Encoding.ASCII.GetString(data.ToArray());

            return JsonSerializer.Deserialize<UpdatedLocationEvent>(json);
        }
    }
}
