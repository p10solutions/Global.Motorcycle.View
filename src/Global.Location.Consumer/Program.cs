using Global.Location.Consumer.Consumer.Created;
using Global.Location.Consumer.Consumer.Updated;
using Global.Location.Consumer.Worker;
using Global.Motorcycle.View.Infraestructure.IoC;

var builder = Host.CreateApplicationBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddHostedService<WorkerCreatedLocation>();
builder.Services.AddHostedService<WorkerUpdatedLocation>();

builder.Services.AddAutoMapper(typeof(CreatedLocationEventMapper));
builder.Services.AddAutoMapper(typeof(UpdatedLocationEventMapper));
builder.Services.AddTransient<ICreatedLocationConsumer, CreatedLocationConsumer>();
builder.Services.AddTransient<IUpdatedLocationConsumer, UpdatedLocationConsumer>();
builder.Services.AddProviders(configuration);

var host = builder.Build();
host.Run();
