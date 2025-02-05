using Administration.Models;
using Administration.Services;
using Administration.Services.Auth;
using Administration.Services.Client;
using Administration.Services.Document;
using Administration.Services.Employe;
using Administration.Services.FactureAchat;
using Administration.Services.FactureVente;
using Administration.Services.Fournisseur;
using Administration.Services.LigneFacture;
using Administration.Services.Retenue;
using Administration.Services.Service;
using Administration.Services.Societe;
using Administration.Services.TypeDocument;
using Administration.Services.Utilisateur;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null; // Garde les noms exacts
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

// Variable de connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDBContext>();

// Ajout du service
builder.Services.AddTransient<IService_Service, Service_Service>();
builder.Services.AddTransient<IUtilisateur_Service, Utilisateur_Service>();
builder.Services.AddTransient<ISociete_Service, Societe_Service>();
builder.Services.AddTransient<IFournisseur_Service, Fournisseur_Service>();
builder.Services.AddTransient<IEmploye_Service, Employe_Service>();
builder.Services.AddTransient<IClient_Service, Client_Service>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<IFactureVente_Service, FactureVente_Service>();
builder.Services.AddTransient<ILigneFactureService, LigneFactureService>();
builder.Services.AddTransient<IFactureAchat_Service, FactureAchat_Service>();
builder.Services.AddTransient<IRetenue_Service, Retenue_Service>();
builder.Services.AddTransient<IDocument_Service, Document_Service>();
builder.Services.AddTransient<ITypeDoc_Service, TypeDoc_Service>();

builder.Services.AddCors();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Administration API",
        Description = "Tester les APIs de l'application Gestion Des Documents Administratifs",
        TermsOfService = new Uri("https://www.google.com"),
        Contact = new OpenApiContact
        {
            Name = "Perfaxis",
            Email = "anoirtaktak@hotmail.fr",
            Url = new Uri("https://www.google.com")
        },
        License = new OpenApiLicense
        {
            Name = "Licence",
            Url = new Uri("https://www.google.com")
        }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\n Exemple \"Bearer 12345abcdef\""
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
                },
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

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

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseAuthentication();  // Assurez-vous que cette ligne est avant UseAuthorization

app.UseAuthorization();

app.MapControllers();

app.Run();
