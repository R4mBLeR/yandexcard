using System.Text.Json;
using YandexMusicGrabberAPI.Services;

public class YandexTokenMiddleware
{
	private readonly RequestDelegate _next;

	public YandexTokenMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context, IYandexMusicService yandexService)
	{
		// Проверяем только API запросы
		if (context.Request.Path.StartsWithSegments("/api"))
		{
			// Проверяем валидность токена
			if (!yandexService.IsAuthorized())
			{ 
				context.Response.StatusCode = 401;
				context.Response.ContentType = "application/json";
				var errorResponse = new
				{
					message = "Yandex Music authentication required",
					details = "Please provide valid OAuth token",
					code = "YANDEX_AUTH_REQUIRED"
				};

				await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
				return;
			}
		}

		await _next(context);
	}
}