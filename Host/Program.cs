using Application.Persistence.Mapper;
using Application.Persistence.Repository;
using AutoMapper;
using Infrasturcture.Persistence.Context;
using Infrasturcture.Persistence.Repository;
using Infrasturcture.Persistence.Service;
using Infrasturcture.Persistence.Service.SurveyResponses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Register Repository and UnitOfWork
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register SurveyResponseService
builder.Services.AddScoped<ISurveyResponseService, SurveyResponseService>();
builder.Services.AddScoped<ISurveyRouteService, SurveyRouteService>();
builder.Services.AddScoped<ISurveyParticipantService, SurveyParticipantService>();
builder.Services.AddScoped<ISurveyOptionService, SurveyOptionService>();

// AutoMapper Configuration
var mappingConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile()); // Assuming you have a MappingProfile
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
//DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")

    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
