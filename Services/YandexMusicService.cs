using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using Yandex.Music.Api;
using Yandex.Music.Api.API;
using Yandex.Music.Api.Common;
using Yandex.Music.Api.Models.Search.Track;
using YandexMusicGrabberAPI.DTOs;
using YandexMusicGrabberAPI.Models;
using YandexMusicGrabberAPI.Services;

namespace YandexMusicGrabberAPI.Utils
{
    public class YandexMusicService : IYandexMusicService
	{
		private  AuthStorage _authStorage;
		private  YandexMusicApi _yandexMusic;
		private  YSearchAPI _ySearchApi;
		private YTrackAPI _yTrackApi;
		private ILogger<YandexMusicService> _logger;

		public YandexMusicService(AuthStorage authStorage, YandexMusicApi yandexMusic, ILogger<YandexMusicService> logger)
		{
			_authStorage = authStorage;
			_yandexMusic = yandexMusic;
			_ySearchApi = new YSearchAPI(yandexMusic);
			_yTrackApi = new YTrackAPI(yandexMusic);
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public  bool IsAuthorized()
		{
			return _authStorage.IsAuthorized;
		}

		public  async Task<TrackDto> GetTrackByName(string name)
		{
			var track = await getFirstTrackInfoAsync(name);
			TrackDto trackDto = new TrackDto(track.Id, track.Title, track.Artists[0].Name);
			return trackDto;
		}

		public async Task<TrackLyricsDto> GetTrackLyrics(string id)
		{
			try
			{
				var trackInfo = await _yTrackApi.GetSupplementAsync(_authStorage, id);
				if (trackInfo?.Result?.Lyrics?.FullLyrics != null)
				{
					// FullLyrics не null
					_logger.LogInformation(trackInfo.Result.Lyrics.FullLyrics);
					return new TrackLyricsDto(id, trackInfo.Result.Lyrics.FullLyrics);
				}
				else
				{
					// Обработка случая, когда текста нет
					_logger.LogWarning("Lyrics not found for track {TrackId}", id);
					return new TrackLyricsDto(id, null);
				}
			} catch (Exception ex)
			{
				return new TrackLyricsDto(id, null);
			}
			
		}

		private async Task<YSearchTrackModel> getFirstTrackInfoAsync(string name)
		{
			var data = await _ySearchApi.TrackAsync(_authStorage, name, 0, 1);
			return data.Result.Tracks.Results[0];
		}
	}
}
