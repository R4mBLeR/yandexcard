using Microsoft.AspNetCore.Mvc;
using Yandex.Music.Api.Models.Search.Track;
using Yandex.Music.Api.Models.Track;
using YandexMusicGrabberAPI.DTOs;
using YandexMusicGrabberAPI.Utils;

namespace YandexMusicGrabberAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TracksController : ControllerBase
	{
		private readonly YandexMusicService _yandexMusicService;
		private readonly ILogger<SearchController> _logger;

		public TracksController(YandexMusicService yandexMusicService, ILogger<SearchController> logger)
		{
			_logger = logger;
			_yandexMusicService = yandexMusicService;
		}


		// GET: /Track//Lyrics{id} - получить заказ по ID
		[HttpGet("{id}/lyrics", Name = "GetTrackLyrics")]
		public async Task<ActionResult<TrackLyricsDto>> GetTrackLyrics(string id)
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

			var lyricsDto = await _yandexMusicService.GetTrackLyrics(id);
			return Ok(lyricsDto);
		}
	}
}
