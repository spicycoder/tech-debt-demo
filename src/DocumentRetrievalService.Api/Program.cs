using AspNetCore.Swagger.Themes;
using DocumentRetrievalService.Api.Middleware;
using DocumentRetrievalService.Application.Common.Behaviors;
using DocumentRetrievalService.Application.Documents.Queries.GetDocuments;
using DocumentRetrievalService.Persistence;
using DocumentRetrievalService.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblyContaining<GetDocumentsQuery>();
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssemblyContaining<GetDocumentsQueryValidator>();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Auto-create database on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DocumentDbContext>();
    context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(
        Theme.Futuristic,
        options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "Document Retrieval Service - V1");
            options.EnableThemeSwitcher(ThemeSwitcherOptions.All());
            options.ShowBackToTopButton();
        });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }