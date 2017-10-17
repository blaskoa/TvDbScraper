using System;
using System.Collections.Generic;

namespace TvDbScraper.Model
{
   public class DenormalizedEpisode
   {
      public int EpisodeId { get; set; }
      public int EpisodeNumber { get; set; }
      public string EpisodeName { get; set; }
      public DateTime? EpisodeDateAired { get; set; }
      public List<string> GuestStars { get; set; }
      public List<string> Directors { get; set; }
      public List<string> Writers { get; set; }
      public string ProductionCode { get; set; }
      public string EpisodeOverview { get; set; }
      public int SeriesId { get; set; }
      public string SeriesName { get; set; }
      public int SeasonNumber { get; set; }
      public string SeriesStatus { get; set; }
      public string SeriesOverview { get; set; }
      public List<string> SeriesGenres { get; set; }
      public DateTime? FirstTimeAired { get; set; }
      public TimeSpan? SeriesAirTime { get; set; }
      public string SeriesPeriodicity { get; set; }
      public string Network { get; set; }
      public int? RuntimeInMinutes { get; set; }
      public string SeriesRating { get; set; }
   }
}