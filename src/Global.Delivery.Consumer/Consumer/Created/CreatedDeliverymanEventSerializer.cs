using Confluent.Kafka;
using Global.Motorcycle.View.Domain.Models.Events.Deliveryman;
using System.Text;
using System.Text.Json;

namespace Global.Deliveryman.Consumer.Consumer.Created
{
    internal class CreatedDeliverymanEventSerializer : IDeserializer<CreatedDeliverymanEvent>
    {
        public CreatedDeliverymanEvent Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            var json = Encoding.ASCII.GetString(data.ToArray());

            return JsonSerializer.Deserialize<CreatedDeliverymanEvent>(json);
        }
    }
}
