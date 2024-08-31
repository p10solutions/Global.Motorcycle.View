using Global.Motorcycle.View.Application.Features.Common;
using Global.Motorcycle.View.Domain.Entities;
using MediatR;

namespace Global.Motorcycle.View.Application.Features.Locations.Commands.SaveLocation
{
    public class SaveLocationCommand: CommandBase<SaveLocationCommand>, IRequest<SaveLocationResponse>
    {
        public SaveLocationCommand() : base(new SaveLocationCommandValidator())
        {

        }

        public Guid Id { get; set; }
        public Guid DeliverymanId { get; set; }
        public Guid PlanId { get; set; }
        public Guid MotorcycleId { get; set; }
        public double? Amount { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool? Paid { get; set; }
        public double? Fee { get; set; }
        public int? DaysUse { get; set; }
        public PlanCommand? Plan { get; set; }
        public ELocationStatus Status { get; set; }
    }
}
