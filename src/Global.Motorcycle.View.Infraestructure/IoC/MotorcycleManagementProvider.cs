using Global.Delivery.View.Application.Features.Deliverys.Commands.SaveDelivery;
using Global.Motorcycle.View.Application.Features.Locations.Commands.SaveLocation;
using Global.Motorcycle.View.Application.Features.Motorcycles.Commands.SaveMotorcycle;
using Global.Motorcycle.View.Application.Features.Motorcycles.Queries.GetMotorcycle;
using Global.Motorcycle.View.Domain.Contracts.Data.Repositories;
using Global.Motorcycle.View.Domain.Contracts.Notifications;
using Global.Motorcycle.View.Domain.Models.Data;
using Global.Motorcycle.View.Infraestructure.Data.Repositories;
using Global.Motorcycle.View.Infraestructure.Validation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Global.Motorcycle.View.Infraestructure.IoC
{
    public static class MotorcycleManagementProvider
    {
        public static IServiceCollection AddProviders(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastValidator<,>));
            services.AddScoped<INotificationsHandler, NotificationHandler>();
            services.AddTransient<IMotorcycleRepository, MotorcycleRepository>();
            services.AddAutoMapper(typeof(SaveMotorcycleMapper));
            services.AddAutoMapper(typeof(GetMotorcycleMapper));
            services.AddAutoMapper(typeof(SaveLocationMapper));
            services.AddAutoMapper(typeof(SaveDeliveryMapper));
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));

            return services;
        }
    }
}
