using api.Repositories;
using api.Services;
using api.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do JWT
var jwtKey = "Este�UmTokenDeSeguran�aMuitoForte!"; // Substitua por uma chave mais segura
builder.Services.AddSingleton(new JwtTokenGenerator(jwtKey));

// Registro de reposit�rios e servi�os
builder.Services.AddScoped<ICarRepository>(sp =>
    new CarRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserRepository>(sp =>
    new UserRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IBrandRepository>(sp =>
    new BrandRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IModelRepository>(sp =>
    new ModelRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ISellRepository>(sp =>
    new SellRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ISellService, SellService>();
builder.Services.AddScoped<IModelService, ModelService>();



// Configura��o de autentica��o e autoriza��o
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
        ValidIssuer = "stand_api",
        ValidAudience = "stand_api",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Configura��o do CORS (se necess�rio para frontend)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configura��o do Swagger (Documenta��o da API)
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Stand Management API",
        Description = "API para gest�o de carros num stand autom�vel.",
        Contact = new OpenApiContact
        {
            Name = "Suporte API Stand",
            Email = "suporte@standapi.com",
            Url = new Uri("https://standapi.com")
        },
        License = new OpenApiLicense
        {
            Name = "Licen�a MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // Incluir os coment�rios XML
    var xmlFile = Path.Combine(AppContext.BaseDirectory, "api.xml");
    if (File.Exists(xmlFile))
    {
        options.IncludeXmlComments(xmlFile);
    }

    // Configurar autentica��o com JWT no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no campo abaixo. Exemplo: Bearer <seu_token_jwt>"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

// Adicionar servi�os MVC
builder.Services.AddControllers();

var app = builder.Build();

// Configura��o do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
