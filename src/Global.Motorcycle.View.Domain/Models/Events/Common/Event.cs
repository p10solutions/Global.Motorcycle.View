using System.Text.Json;

namespace Global.Motorcycle.View.Domain.Models.Events.Common
{
    public abstract class Event
    {
        public override string ToString()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            return JsonSerializer.Serialize(this, this.GetType(), options);
        }
    }
}
