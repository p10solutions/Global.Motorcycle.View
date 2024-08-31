using Confluent.Kafka;
using System.Text.Json;
using System.Text;
using Global.Motorcycle.View.Domain.Models.Events.Motorcycles;

namespace Global.Motorcycle.Consumer.Consumer.Deleted
{
    internal class DeletedMotorcycleEventSerializer : IDeserializer<DeletedMotorcycleEvent>
    {
        public DeletedMotorcycleEvent Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            var json = Encoding.ASCII.GetString(data.ToArray());

            return JsonSerializer.Deserialize<DeletedMotorcycleEvent>(json);
        }
    }
}
