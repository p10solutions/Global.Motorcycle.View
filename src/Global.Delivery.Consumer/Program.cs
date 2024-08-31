using Global.Delivery.Consumer.Consumer.Updated;
using Global.Delivery.Consumer.Worker;
using Global.Deliveryman.Consumer.Consumer.Created;
using Global.Motorcycle.View.Infraestructure.IoC;

var builder = Host.CreateApplicationBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddHostedService<WorkerDeliverymanCreated>();
builder.Services.AddHostedService<WorkerDeliverymanUpdated>();
builder.Services.AddTransient<ICreatedDeliverymanConsumer, CreatedDeliverymanConsumer>();
builder.Services.AddTransient<IUpdatedDeliverymanConsumer, UpdatedDeliverymanConsumer>();
builder.Services.AddAutoMapper(typeof(CreatedDeliverymanEventMapper));
builder.Services.AddAutoMapper(typeof(UpdatedDeliverymanEventMapper));
builder.Services.AddProviders(configuration);

var host = builder.Build();

host.Run();
