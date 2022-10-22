using ChattingApp.Domain.Models;
using ChattingApp.Helper.Middlewares;
using ChattingApp.Installers;
using ChattingApp.Persistence;
using ChattingApp.Persistence.DataSeed;
using ChattingApp.Persistence.IRepositories;
using ChattingApp.Persistence.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.InstallServicesExtension(builder.Configuration);

builder.Services.AddControllers().AddNewtonsoftJson(n => n.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



var app = builder.Build();
/// <summary>
/// Seed data
/// </summary>
/// 
//using var scope = app.Services.CreateScope();
//var services = scope.ServiceProvider;
//try
//{
//    var context = services.GetRequiredService<UserManager<AppUsers>>();
//    await SeedData.AddRoleToUsers(context);
//}
//catch (Exception)
//{

//    throw;
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseUnauthorizedMiddleware();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("ChattingPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
