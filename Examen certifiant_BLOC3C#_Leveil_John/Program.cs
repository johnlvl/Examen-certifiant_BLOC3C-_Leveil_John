using Examen_certifiant_BLOC3C__Leveil_John.Data;
using Examen_certifiant_BLOC3C__Leveil_John.Services.AdminService;
using Examen_certifiant_BLOC3C__Leveil_John.Services.PaimentService;
using Examen_certifiant_BLOC3C__Leveil_John.Services.QrCodeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

// Ajout des services pour la gestion des sessions
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = "JO2024";
});

// Ajout de l'injection de dépendances pour IHttpContextAccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddControllersWithViews();
// Ajout du service AdminIdRequirementHandler en lui passant l'ID de l'administrateur
builder.Services.AddScoped<IAuthorizationHandler>(provider => new AdminService("6ce3ecae-1eb1-4181-86d2-9c4eaec24539"));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdministrateurUniquement", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new OperationAuthorizationRequirement { Name = "AdministrateurUniquement" });
    });
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Administrateur", "AdministrateurUniquement");
});

// Ajout de service PaiementService
builder.Services.AddScoped<PaimentService>();

// Ajout du service QrCodeService
builder.Services.AddTransient<QrCodeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Ajout de l'utilisation de la session
app.UseSession();

app.MapRazorPages();

app.Run();
