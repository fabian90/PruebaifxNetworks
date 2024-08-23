using FluentValidation.AspNetCore;
using ifxNetworks.Core.Interfaces.Repositories;
using ifxNetworks.Core.Interfaces.Services;
using ifxNetworks.Core.Options;
using ifxNetworks.Core.Services;
using ifxNetworks.Infrastructure.Data;
using ifxNetworks.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using ifxNetworks.API.IoC;
using System.Reflection;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

var allowSpecificOrigins = "AllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

string[] lOrigins = builder.Configuration.GetValue<string>("Origins").Split(","); ;

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins(lOrigins);// Permitir cualquier origen
                          policy.AllowAnyMethod();// Permitir cualquier método (GET, POST, PUT, DELETE, etc.)
                          policy.AllowAnyHeader();// Permitir cualquier encabezado
                      });
});
DependencyContainer.RegisterServices(builder.Services);
// Add services to the container.
// Configurar JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    options.AddPolicy("RequireUserRole", policy => policy.RequireClaim(ClaimTypes.Role, "AdmiUser"));
});

builder.Services.AddControllers();
//.AddFluentValidation(x => x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FabianVargasTovar.Api", Version = "v1" });

// Configurar el esquema de autenticación
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddFluentValidation(conf =>
{
    conf.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
    conf.AutomaticValidationEnabled = false;
});

string connString = builder.Configuration.GetConnectionString("ConexionApp");
builder.Services.AddDbContext<IdentityDBContext>(options =>
{
    options.UseSqlServer(connString);
    options.UseSqlServer(connString, b => b.MigrationsAssembly("ifxNetworks.API"));
});

//Configure Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Configure Password Options
builder.Services.Configure<PasswordOptions>(options => builder.Configuration.GetSection("PasswordOptions").Bind(options));

builder.Services.Configure<TokenOptions>(options => builder.Configuration.GetSection("Jwt").Bind(options));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FabianVargasTovar.Api v1"));
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(allowSpecificOrigins); // Aplicar la política CORS
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
