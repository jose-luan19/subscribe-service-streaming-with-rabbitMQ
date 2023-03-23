using desafioBack.Infra;
using desafioBack.RabitMQ;
using desafioBack.Services;
using Infra.Repository;
using Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddScoped<ISubService, SubService>();
builder.Services.AddDbContext<DbContextClass>();
builder.Services.AddScoped<IRabitMQProducer, RabitMQProducer>();

builder.Services.AddScoped<IARepository<User>, UserRepository>();
builder.Services.AddScoped<IARepository<Status>, StatusRepository>();
builder.Services.AddScoped<IARepository<Subscription>, SubscriptionRepository>();
builder.Services.AddScoped<IARepository<EventHistory>, EventHistoryRepository>();

builder.Services.AddHostedService<MessageConsumer>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();