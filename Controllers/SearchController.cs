using Microsoft.AspNetCore.Mvc;
using Yandex.Music.Api.Models.Search.Track;
using YandexMusicGrabberAPI.Models;
using YandexMusicGrabberAPI.Utils;

namespace YandexMusicGrabberAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
		private readonly YandexMusicService _yandexMusicService;
		private readonly ILogger<SearchController> _logger;

		public SearchController(YandexMusicService yandexMusicService, ILogger<SearchController> logger)
		{
			_logger = logger;
			_yandexMusicService = yandexMusicService;
		}


		// GET: /orders/{id} - получить заказ по ID
		[HttpGet("{name}", Name = "GetTrackByName")]
		public async Task<ActionResult<TrackDto>> GetTrackByName(string name)
		{
			if (!_yandexMusicService.IsAuthorized())
			{
				return Unauthorized(new
				{
					message = "Yandex Music authentication required",
					details = "Please provide valid OAuth token",
					code = "YANDEX_AUTH_REQUIRED"
				});
			}

			var track = await _yandexMusicService.GetTrackByName(name);
			return Ok(track);
		}
	}
}
