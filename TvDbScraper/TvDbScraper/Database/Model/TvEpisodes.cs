namespace TvDbScraper.Database.Model
{
   public class TvEpisodes
   {
      public int Id { get; set; }
      public int SeasonId { get; set; }
      public int EpisodeNumber { get; set; }
      public string EpisodeName { get; set; }
      public string FirstAired { get; set; }
      public string GuestStars { get; set; }
      public string Director { get; set; }
      public string Writer { get; set; }
      public string Overview { get; set; }
      public string ProductionCode { get; set; }
      public int SeriesId { get; set; }
   }
}