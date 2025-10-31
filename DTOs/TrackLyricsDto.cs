using Yandex.Music.Api.Models.Common;
using YandexMusicGrabberAPI.Models;

namespace YandexMusicGrabberAPI.DTOs
{
	public class TrackLyricsDto
	{
		public string Id { get; set; }
		public string Lyrics { get; set; }

		public TrackLyricsDto(string id, string lyrics)
		{
			Id = id;
			Lyrics = lyrics;
		}
	}
}