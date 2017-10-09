namespace TvDbScraper.Model
{
   public class Series
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public short Rating { get; set; }
      public string Description { get; set; }

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
}