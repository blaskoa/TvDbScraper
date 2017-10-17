using System;
using System.Collections.Generic;
using System.Linq;
using TvDbScraper.Model;

namespace TvDbScraper.Database.Model
{
   public class DenormalizedTvEpisode
   {
      public const string DbSelect =
         @"select 
            e.id,
            e.seasonid,
            e.EpisodeNumber,
            e.EpisodeName,
            e.FirstAired as EpisodeAired,
            e.GuestStars,
            e.Director,
            e.Writer,
            e.Overview as EpisodeOverview,
            e.ProductionCode,
            e.seriesid,
            se.season,
            sr.SeriesName,
            sr.Status,
            sr.FirstAired as SeriesAired,
            sr.Network,
            sr.Runtime,
            sr.Genre,
            sr.Actors,
            sr.Overview as SeriesOverview,
            sr.Airs_DayOfWeek,
            sr.Airs_Time,
            sr.Rating
          from tvdb.tvepisodes e
            left join tvdb.tvseasons se on e.seasonid = se.id
            left join tvdb.tvseries sr on e.seriesid = sr.id
          where sr.id between @firstId and @lastId";

      public int Id { get; set; }
      public int SeasonId { get; set; }
      public int EpisodeNumber { get; set; }
      public string EpisodeName { get; set; }
      public string EpisodeAired { get; set; }
      public string GuestStars { get; set; }
      public string Director { get; set; }
      public string Writer { get; set; }
      public string EpisodeOverview { get; set; }
      public string ProductionCode { get; set; }
      public int SeriesId { get; set; }
      public int SeasonNumber { get; set; }
      public string SeriesName { get; set; }
      public string Status { get; set; }
      public string SeriesAired { get; set; }
      public string Network { get; set; }
      public string Runtime { get; set; }
      public string Genre { get; set; }
      public string SeriesOverview { get; set; }
      public string Airs_DayOfWeek { get; set; }
      public string Airs_Time { get; set; }
      public string Rating { get; set; }


      public DenormalizedEpisode ToDomainModel()
      {

         List<string> directors = new List<string>();
         List<string> genres = new List<string>();
         List<string> guestStars = new List<string>();
         List<string> writers = new List<string>();

         if (!string.IsNullOrWhiteSpace(Director))
         {
            directors = Director.Split('|').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToList();
         }
         if (!string.IsNullOrWhiteSpace(Genre))
         {
            genres = Genre.Split('|').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToList();
         }
         if (!string.IsNullOrWhiteSpace(GuestStars))
         {
            guestStars = GuestStars.Split('|').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToList();
         }
         if (!string.IsNullOrWhiteSpace(Writer))
         {
            writers = Writer.Split('|').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToList();
         }

         DateTime? seriesAirTime = ParseDateTimeSafe(Airs_Time);
         TimeSpan? seriesAirTimeSpan = null;
         if (seriesAirTime.HasValue)
         {
            seriesAirTimeSpan = seriesAirTime.Value.TimeOfDay;
         }
         
         DateTime? episodeDateAired = ParseDateTimeSafe(EpisodeAired);
         DateTime? firstTimeAired = ParseDateTimeSafe(SeriesAired);

         int? runtime = null;
         int runtimeNonNullable;
         bool parseResult = int.TryParse(Runtime, out runtimeNonNullable);
         if (parseResult)
         {
            runtime = runtimeNonNullable;
         }



         DenormalizedEpisode result = new DenormalizedEpisode
         {
            SeriesAirTime = seriesAirTimeSpan,
            Directors = directors,
            EpisodeDateAired = episodeDateAired,
            SeriesRating = Rating,
            SeriesPeriodicity = Airs_DayOfWeek,
            SeriesStatus = Status,
            SeriesGenres = genres,
            EpisodeName = EpisodeName,
            EpisodeId = Id,
            SeriesId = SeriesId,
            Network = Network,
            FirstTimeAired = firstTimeAired,
            RuntimeInMinutes = runtime,
            ProductionCode = ProductionCode,
            GuestStars = guestStars,
            Writers = writers,
            EpisodeNumber = EpisodeNumber,
            SeriesOverview = SeriesOverview,
            EpisodeOverview = EpisodeOverview,
            SeasonNumber = SeasonNumber,
            SeriesName = SeriesName
         };
         return result;
      }

      private DateTime? ParseDateTimeSafe(string value)
      {
         DateTime? result = null;
         DateTime date;
         bool parseResult = DateTime.TryParse(value, out date);
         if (parseResult)
         {
            result = date;
         }

         return result;
      }
   }
}