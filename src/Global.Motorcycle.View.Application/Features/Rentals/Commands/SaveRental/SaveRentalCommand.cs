using Global.Motorcycle.View.Application.Features.Common;
using Global.Motorcycle.View.Domain.Entities;
using MediatR;

namespace Global.Motorcycle.View.Application.Features.Rentals.Commands.SaveRental
{
    public class SaveRentalCommand : CommandBase<SaveRentalCommand>, IRequest<SaveRentalResponse>
    {
        public SaveRentalCommand() : base(new SaveRentalCommandValidator())
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
