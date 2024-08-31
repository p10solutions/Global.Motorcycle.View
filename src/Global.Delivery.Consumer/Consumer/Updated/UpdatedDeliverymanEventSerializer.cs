using Confluent.Kafka;
using Global.Motorcycle.View.Domain.Models.Events.Deliveryman;
using System.Text;
using System.Text.Json;

namespace Global.Delivery.Consumer.Consumer.Updated
{
    internal class UpdatedDeliverymanEventSerializer : IDeserializer<UpdatedDeliverymanEvent>
    {
        public UpdatedDeliverymanEvent Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            var json = Encoding.ASCII.GetString(data.ToArray());

            return JsonSerializer.Deserialize<UpdatedDeliverymanEvent>(json);
        }
    }
}
