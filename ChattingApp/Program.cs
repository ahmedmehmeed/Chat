using ChattingApp.Domain.Models;
using ChattingApp.Helper.Middlewares;
using ChattingApp.Installers;
using ChattingApp.Persistence.Interface;
using ChattingApp.Persistence.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.InstallServicesExtension(builder.Configuration);

builder.Services.AddControllers();
//builder.Services.AddScoped<IAccountService, AccountService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();




var app = builder.Build();

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
