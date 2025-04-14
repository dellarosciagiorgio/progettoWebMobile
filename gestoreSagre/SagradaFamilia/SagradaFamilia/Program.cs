using Application.Extensions;
using Application.Factories;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services
    .AddUiServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices();


// Add services to the container.


builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseCors("DevelopmentCORS");

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            Exception errorToRet = contextFeature.Error;
            if (contextFeature.Error is Microsoft.EntityFrameworkCore.DbUpdateException dbEx)
            {
                errorToRet = dbEx.InnerException ?? dbEx;
            }
            var res = ResponseFactory
                .WithError(errorToRet);
            await context.Response.WriteAsJsonAsync(
                res
                );
        }
    });
});

app.UseHttpsRedirection();

app.UseDefaultFiles(); // cerca index.html
app.UseStaticFiles();  // serve da wwwroot/

app.MapControllers();  // mie API

// fallback per routing client-side (SPA)
app.MapFallbackToFile("index.html");

app.Run();