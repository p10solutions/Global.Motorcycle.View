using Confluent.Kafka;
using System.Text.Json;
using System.Text;
using Global.Motorcycle.View.Domain.Models.Events.Motorcycles;

namespace Global.Motorcycle.Consumer.Consumer.Updated
{
    internal class UpdatedMotorcycleEventSerializer : IDeserializer<UpdatedMotorcycleEvent>
    {
        public UpdatedMotorcycleEvent Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            var json = Encoding.ASCII.GetString(data.ToArray());

            return JsonSerializer.Deserialize<UpdatedMotorcycleEvent>(json);
        }
    }
}
