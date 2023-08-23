using DataAccessLayer.Contect;
using Intern.WebAPI.Consumer;
using Intern.WebAPI.Hubs;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();
//Masstransit Configurasyonu//
//
var massTransitHost = configuration["MassTransit:Host"];
var massTransitUsername = configuration["MassTransit:Username"];
var massTransitPassword = configuration["MassTransit:Password"];

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(policy => policy.AddDefaultPolicy(builder => builder.SetIsOriginAllowed(_ => true).AllowAnyHeader().AllowCredentials().AllowAnyMethod().WithExposedHeaders("X-Pagination")));

builder.Services.AddSingleton<MyHub>();

// Add MassTransit and configure RabbitMQ
builder.Services.AddMassTransit(cfg =>
{
    cfg.AddConsumer<ConsumerRMQ>();

    cfg.UsingRabbitMq((context, config) =>
    {
        config.ReceiveEndpoint("Consumer.Random", e =>
        {
            e.PrefetchCount = 1;
            e.ConfigureConsumer<ConsumerRMQ>(context);
        });

        config.Host(massTransitHost, "/", h =>
        {
            h.Username(massTransitUsername);
            h.Password(massTransitPassword);
        });
        //config.Host("192.168.1.159", "/", h =>
        //{
        //    h.Username("altis");
        //    h.Password("altis");
        //});
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Urls.Add("http://*:5000/");

app.UseCors();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoint =>
{
    endpoint.MapHub<MyHub>("/myhub");
});

app.MapControllers();
app.Run();
