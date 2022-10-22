using ECommerce.Application;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Enums;
using ECommerce.Infrastructure.Filters;
using ECommerce.Infrastructure.Services.Storage.Local;
using ECommerce.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200/")
        .AllowAnyHeader()
        .AllowAnyMethod()
));
builder.Services.AddControllers(options=>options.Filters.Add<ValidationFilter>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices();
builder.Services.AddStorage<LocalStorage>();
// builder.Services.AddStorage(StorageType.Local);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();