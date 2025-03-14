using Application.Extensions;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    //.AddUIServices(builder.Configuration)
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
/*
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/
/*
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
*/

app.UseHttpsRedirection();

app.MapControllers();

app.Run();