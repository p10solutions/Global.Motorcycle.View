using Global.Motorcycle.Consumer.Consumer.Created;
using Global.Motorcycle.Consumer.Consumer.Deleted;
using Global.Motorcycle.Consumer.Consumer.Updated;
using Global.Motorcycle.Consumer.Worker;
using Global.Motorcycle.View.Infraestructure.IoC;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<WorkerCreatedMotorcycle>();
builder.Services.AddHostedService<WorkerUpdatedMotorcycle>();
builder.Services.AddHostedService<WorkerDeletedMotorcycle>();

var configuration = builder.Configuration;

builder.Services.AddAutoMapper(typeof(CreatedMotorcycleEventMapper));
builder.Services.AddAutoMapper(typeof(UpdatedMotorcycleEventMapper));
builder.Services.AddAutoMapper(typeof(DeletedMotorcycleEventMapper));
builder.Services.AddTransient<ICreatedMotorcycleConsumer, CreatedMotorcycleConsumer>();
builder.Services.AddTransient<IUpdatedMotorcycleConsumer, UpdatedMotorcycleConsumer>();
builder.Services.AddTransient<IDeletedMotorcycleConsumer, DeletedMotorcycleConsumer>();
builder.Services.AddProviders(configuration);

var host = builder.Build();
host.Run();
