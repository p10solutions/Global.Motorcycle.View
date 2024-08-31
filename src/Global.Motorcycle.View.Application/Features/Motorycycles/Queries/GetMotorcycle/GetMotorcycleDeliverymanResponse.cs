﻿using Global.Motorcycle.View.Domain.Entities;

namespace Global.Motorcycle.View.Application.Features.Motorycycles.Queries.GetMotorcycle
{
    public class GetMotorcycleDeliverymanResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LicenseNumber { get; set; }
        public ELicenseType LicenseType { get; set; }
        public string? LicenseImage { get; set; }
    }
}
