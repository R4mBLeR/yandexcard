using Yandex.Music.Api;
using Yandex.Music.Api.API;
using Yandex.Music.Api.Common;
using YandexMusicGrabberAPI.DTOs;
using YandexMusicGrabberAPI.Models;

namespace YandexMusicGrabberAPI.Services
{
	public interface IYandexMusicService
	{

		bool IsAuthorized();

		//Task<List<TrackDto>> GetTracksByName(string name);

		Task<TrackDto> GetTrackByName(string name);

		Task<TrackLyricsDto> GetTrackLyrics(string name);
	}
}
