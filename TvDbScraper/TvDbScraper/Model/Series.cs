using System;
using System.Collections.Generic;

namespace TvDbScraper.Model
{
   public class Series : BaseModel
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public short Rating { get; set; }
      public string Overview { get; set; }
      public HashSet<SeriesGenre> SeriesGenres { get; set; }
      public DateTime FirstTimeAired { get; set; }
      public DateTime AirTime { get; set; }
      public SeriesPeriodicity SeriesPeriodicity { get; set; }
      public string Network { get; set; }
      public int RuntimeInMinutes { get; set; }
      public SeriesRating SeriesRating { get; set; }
      public List<Season> Seasons { get; set; }
   }

   public enum SeriesStatus
   {
      Unknown = 0,
      Continuing = 1,
      Ended = 2
   }

   public enum SeriesGenre
   {
      Action,
      Animation,
      Comedy,
      Documentary,
      Family,
      Food,
      HomeAndGarden,
      MiniSeries,
      News,
      Romance,
      Soap,
      Sport,
      TalkShow,
      Travel,
      Adventure,
      Children,
      Crime,
      Drama,
      Fantasy,
      GameShow,
      Horror,
      Mystery,
      Reality,
      ScienceFiction,
      SpecialInterest,
      Suspense,
      Thriller,
      Western
   }

   public enum SeriesPeriodicity
   {
      Undefined,
      Monday,
      Tuesday,
      Wednesday,
      Thursday,
      Friday,
      Saturday,
      Sunday,
      Daily
   }

   public enum SeriesRating
   {
      Undefined,
      Y,
      YSeven,
      G,
      Pg,
      Fourteen,
      Ma
   }
}