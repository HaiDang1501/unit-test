using Domain;
using Domain.Users;
using Swashbuckle.AspNetCore.Annotations;
using WebUI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomainServices(builder.Configuration);

builder.Services.AddScoped<UserService>();


var app = builder.Build();

app.MapGet("/", () => "Hello World!").WithMetadata(new SwaggerOperationAttribute("summary001", "description001"));

app.MapPost(
    "user",
    (string name, string email) =>
    {
        var newUser = User.Create(
            name,
            email,
            true);
    }).WithOpenApi();

app.Run();

public partial class Program { }