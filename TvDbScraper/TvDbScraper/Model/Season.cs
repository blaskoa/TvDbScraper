using System.Collections.Generic;

namespace TvDbScraper.Model
{
   public class Season
   {
      public string Name { get; set; }
      public int EpisodeCount { get; set; }
      public List<Episode> Episodes { get; set; }

      public Season()
      {
         Episodes = new List<Episode>();
      }
   }
}