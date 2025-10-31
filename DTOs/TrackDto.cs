namespace YandexMusicGrabberAPI.Models
{
	public class TrackDto
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Band { get; set; }

		public TrackDto(string id, string title, string band)
		{
			Id = id;
			Title = title;
			Band = band;
		}
	}
}
