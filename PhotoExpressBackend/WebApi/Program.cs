using Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using WebApi.Services.EventModification;
using WebApi.Services.Events;
using WebApi.Services.HigherEducationInstitutions;
using WebApi.Services.Notifications;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<photoExpressContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("photoExpressDB")));
builder.Services.AddScoped<IHigherEducationInstitutionRepository, HigherEducationInstitutionRepository>();
builder.Services.AddScoped<IEventsRepository, EventsRepository>();
builder.Services.AddScoped<IEventModificationRepository, EventModificationRepository>();
var smtpServer = builder.Configuration["smtpServer"];
var smtpPort = builder.Configuration["smtpPort"];
var originEmail = builder.Configuration["originEmail"];
var password = builder.Configuration["password"];
builder.Services.AddSingleton<IMessage>(srv=> new EmailSender(smtpServer,int.Parse(smtpPort),originEmail,password));


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
