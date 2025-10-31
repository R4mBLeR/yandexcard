using Yandex.Music.Api;
using Yandex.Music.Api.Common;
using YandexMusicGrabberAPI.Services;
using YandexMusicGrabberAPI.Utils;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IYandexMusicService>(provider =>
{
	var authStorage = new AuthStorage();
	var yandexMusic = new YandexMusicApi();
	var token = builder.Configuration["YandexToken"];
	var logger = provider.GetRequiredService<ILogger<YandexMusicService>>();

	// Авторизация
	try
	{
		yandexMusic.User.Authorize(authStorage, token);
	}
	catch (Exception ex)
	{
		logger.LogError("Yandex Token is invalid!");
	}
	

	// Создаем сервис с готовыми зависимостями
	return new YandexMusicService(authStorage, yandexMusic, logger);
});

builder.Services.AddScoped<YandexMusicService>(provider =>
	provider.GetRequiredService<IYandexMusicService>() as YandexMusicService);

var app = builder.Build();

app.UseMiddleware<YandexTokenMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
//YandexMusicService.InitializeWithLogger(token, loggerFactory);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
