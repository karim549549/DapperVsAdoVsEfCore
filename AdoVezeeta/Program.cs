
using AdoDapper.Extensions;
using AdoVezeeta.BenchMarkers;
using BenchmarkDotNet.Running;
using DapperService.Extension;
using EfCoreSerivce.Extensions;

var builder = WebApplication.CreateBuilder(args);

BenchmarkRunner.Run<Benchmark>();

builder.Services.AddControllers();
builder.Services.AddEfCoreServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDapperServices(builder.Configuration);
builder.Services.AddLifeTimes(builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new Exception("Connection string is null"));
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
