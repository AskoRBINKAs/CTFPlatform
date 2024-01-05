using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using CTFPlatform;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(options=> options.UseNpgsql("Host=postgres;Port=5432;Database=CTF;Username=dbadmin;Password=secretkey"));
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!))
    };
});
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.Run();
