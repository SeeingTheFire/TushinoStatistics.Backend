using DataBase.Statistics;
using Microsoft.EntityFrameworkCore;
using Parser.Statistics;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", true, true);
builder.Configuration.AddJsonFile("appsettings." + builder.Environment.EnvironmentName + ".json", true, true);

// Регистрация сервисов в DI контейнер
builder.Services.AddControllers();
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

builder.Services.Configure<RouteOptions>(
    options =>
    {
        options.AppendTrailingSlash = true;
        options.LowercaseUrls = true;
        options.LowercaseQueryStrings = true;
    });

builder.Services.AddHttpClient("Parser", client => { client.Timeout = TimeSpan.FromMinutes(30); })
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { MaxConnectionsPerServer = 10 });

builder.Services.AddServices()
    .AddRepositories();

// Контекст бд
builder.Services.AddDbContext<ApplicationContext>(opt =>
    {
        opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgresql"));
        opt.UseSnakeCaseNamingConvention();
    });

builder.Services.AddHostedService<MigrationHostedService>();

builder.Services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory<ApplicationContext>>();

// Фоновый сервис по обновлению статистики
builder.Services.AddHostedService<StatisticsHostedService>();

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
    
app.MapControllers();

app.Run();


