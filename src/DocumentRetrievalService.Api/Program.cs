using AspNetCore.Swagger.Themes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

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