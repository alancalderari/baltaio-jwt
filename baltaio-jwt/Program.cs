using System.Security.Claims;
using System.Text;
using baltaio_jwt;
using baltaio_jwt.Extensions;
using baltaio_jwt.Models;
using baltaio_jwt.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.PrivateKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("admin", policy => policy.RequireRole("admin"));
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login", (TokenService service)
        =>
    {
        var user = new User(
            Id:1,
            Name:"André Baltieri",
            Image:"https://balta.io/imgs/andrebaltieri.jpg",
            Email:"xyz@dacc.com",
            Password:"abc123",
            Roles: new string[] {"Admin", "Manager"});


        return service.Create(user);
    }
);
app.MapGet("/restrito", (ClaimsPrincipal user) => new
{
    id = user.Id(),
    name = user.Name(),
    email = user.Email(),
    givenName = user.GivenName(),
    image = user.Image(),
}).RequireAuthorization();

app.MapGet("/admin", () => "tu tem acesso").RequireAuthorization("admin");

app.Run();