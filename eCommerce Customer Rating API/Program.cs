using Microsoft.EntityFrameworkCore;
using eCommerce_Customer_Rating_API.Models;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<HubtelApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // c.SwaggerDoc("v1", new OpenApiInfo { Title = "eCommerce Customer Rating API" });
    c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Basic scheme. Basic <string>"
    });
    //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //{
    //    Name = "Authorization",
    //    Type = SecuritySchemeType.ApiKey,
    //    Scheme = "Bearer",
    //    In = ParameterLocation.Header,
    //    Description = "Bearer Authorization header using the Bearer scheme. Bearer <string>"
    //});

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                }
            },
            new String[] {}
        }
    });
});

builder.Services.AddAuthentication()
    .AddJwtBearer("JWTAuthentication", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"])),
        };
    })
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationAttribute>("BasicAuthentication", options => { });


builder.Services.AddMvc();

var app = builder.Build();

app.UseCors(options =>
options.WithOrigins("http://localhost:4200")
.AllowAnyMethod()
.AllowAnyHeader());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hubtel API v1");
    });

}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
