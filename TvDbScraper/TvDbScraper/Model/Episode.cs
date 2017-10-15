using System;
using System.Collections.Generic;

namespace TvDbScraper.Model
{
   public class Episode
   {
      public int Id { get; set; }
      public int EpisodeNumber { get; set; }
      public string EpisodeName { get; set; }
      public DateTime? DateAired { get; set; }
      public List<string> GuestStars { get; set; }
      public List<string> Directors { get; set; }
      public List<string> Writers { get; set; }
      public string ProductionCode { get; set; }
      public string Overview { get; set; }
      public int UserRating { get; set; }
      public int UserRatingCount { get; set; }

      public Episode()
      {
         GuestStars = new List<string>();
         Directors = new List<string>();
         Writers = new List<string>();
      }
   }
}