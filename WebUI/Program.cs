using Domain;
using Domain.Users;
using Swashbuckle.AspNetCore.Annotations;
using WebUI.Controllers;
using WebUI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDomainServices(builder.Configuration);

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<FollowerService>();


var app = builder.Build();

app.MapGet("/", () => "Hello World!").WithMetadata(new SwaggerOperationAttribute("summary001", "description001"));

//app.MapPost(
//    "user",
//    (string name, string email) =>
//    {
//        var newUser = User.Create(
//            name,
//            email,
//            true);
//    }).WithOpenApi();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapUserEndpoints();
app.MapFollowerEndpoints();

app.Run();


public partial class Program { }