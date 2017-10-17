namespace TvDbScraper.Database.Model
{
   public class TvSeries
   {
      public int Id { get; set; }
      public string SeriesName { get; set; }
      public string Status { get; set; }
      public string FirstAired { get; set; }
      public string Network { get; set; }
      public string Runtime { get; set; }
      public string Genre { get; set; }
      public string Overview { get; set; }
      public string Airs_DayOfWeek { get; set; }
      public string Airs_Time { get; set; }
      public string Rating { get; set; }
   }
}