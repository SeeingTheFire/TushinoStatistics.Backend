using System.Text;
using Backend.Statistics;
using Backend.Statistics.Interfaces;
using Backend.Statistics.Services;
using DataBase.Statistics;
using DataBase.Statistics.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceCollectionExtension = Parser.Statistics.ServiceCollectionExtension;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", true, true);
builder.Configuration.AddJsonFile("appsettings." + builder.Environment.EnvironmentName + ".json", true, true);

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpClient();
builder.Services.AddControllers();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });

builder.Services.AddDbContext<ApplicationContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgresql"));
    opt.UseSnakeCaseNamingConvention();
});

ServiceCollectionExtension.AddRepositories(ServiceCollectionExtension.AddServices(builder.Services));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJs",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});



// builder.Services.AddSingleton(new QuickStorageConnectionProvider(builder.Configuration.GetConnectionString("Redis")));
// builder.Services.AddScoped<IQuickStorage, QuickStorageService>();
builder.Services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory<ApplicationContext>>();
builder.Services.AddScoped<ISquadService, SquadService>();
builder.Services.AddControllers(conf=>conf.Conventions.Insert(0, new RoutingConvention("Base Route Prefix", "api")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();
app.UseCors("AllowNextJs");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.Run();