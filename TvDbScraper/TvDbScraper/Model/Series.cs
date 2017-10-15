﻿using System;
using System.Collections.Generic;

namespace TvDbScraper.Model
{
   public class Series
   {
      private const string ContinuingStringValue = "Continuing";
      private const string EndedStringValue = "Ended";

      public int Id { get; set; }
      public string Name { get; set; }
      public int Rating { get; set; }
      public int NumberOfRatings { get; set; }
      public SeriesStatus SeriesStatus { get; set; }
      public string Overview { get; set; }
      public List<SeriesGenre> SeriesGenres { get; set; }
      public DateTime FirstTimeAired { get; set; }
      public DateTime AirTime { get; set; }
      public SeriesPeriodicity SeriesPeriodicity { get; set; }
      public string Network { get; set; }
      public int RuntimeInMinutes { get; set; }
      public SeriesRating SeriesRating { get; set; }
      public List<Season> Seasons { get; set; }

      public Series()
      {
         Seasons = new List<Season>();
         SeriesGenres = new List<SeriesGenre>();
      }

      #region Enum converters
      public static SeriesStatus GetStatusFromString(string stringStatus)
      {
         SeriesStatus result = SeriesStatus.Unknown;
         if (!string.IsNullOrWhiteSpace(stringStatus))
         {
            if (ContinuingStringValue.Equals(stringStatus))
            {
               result = SeriesStatus.Continuing;
            }
            else if (EndedStringValue.Equals(stringStatus))
            {
               result = SeriesStatus.Ended;
            }
         }
         return result;
      }

      public static SeriesGenre GetGenreFromString(string stringGenre)
      {
         SeriesGenre result;
         if (!string.IsNullOrWhiteSpace(stringGenre))
         {
            switch (stringGenre)
            {
               case "Action":
                  result = SeriesGenre.Action;
                  break;
               case "Animation":
                  result = SeriesGenre.Animation;
                  break;
               case "Comedy":
                  result = SeriesGenre.Comedy;
                  break;
               case "Documentary":
                  result = SeriesGenre.Documentary;
                  break;
               case "Family":
                  result = SeriesGenre.Family;
                  break;
               case "Food":
                  result = SeriesGenre.Food;
                  break;
               case "Home and Garden":
                  result = SeriesGenre.HomeAndGarden;
                  break;
               case "Mini-Series":
                  result = SeriesGenre.MiniSeries;
                  break;
               case "News":
                  result = SeriesGenre.News;
                  break;
               case "Romance":
                  result = SeriesGenre.Romance;
                  break;
               case "Soap":
                  result = SeriesGenre.Soap;
                  break;
               case "Sport":
                  result = SeriesGenre.Sport;
                  break;
               case "Talk Show":
                  result = SeriesGenre.TalkShow;
                  break;
               case "Travel":
                  result = SeriesGenre.Travel;
                  break;
               case "Horror":
                  result = SeriesGenre.Horror;
                  break;
               case "Mystery":
                  result = SeriesGenre.Mystery;
                  break;
               case "Reality":
                  result = SeriesGenre.Reality;
                  break;
               case "Science-Fiction":
                  result = SeriesGenre.ScienceFiction;
                  break;
               case "Special Interest":
                  result = SeriesGenre.SpecialInterest;
                  break;
               case "Suspense":
                  result = SeriesGenre.Suspense;
                  break;
               case "Thriller":
                  result = SeriesGenre.Thriller;
                  break;
               case "Adventure":
                  result = SeriesGenre.Adventure;
                  break;
               case "Children":
                  result = SeriesGenre.Children;
                  break;
               case "Crime":
                  result = SeriesGenre.Crime;
                  break;
               case "Drama":
                  result = SeriesGenre.Drama;
                  break;
               case "Fantasy":
                  result = SeriesGenre.Fantasy;
                  break;
               case "Game Show":
                  result = SeriesGenre.GameShow;
                  break;
               default:
                  throw new Exception("Unknown genre: " + stringGenre);
            }
         }
         else
         {
            throw new Exception("Unknown genre: " + stringGenre);
         }
         return result;
      }

      public static SeriesPeriodicity GetPeriodicityFromString(string periodicityString)
      {
         SeriesPeriodicity result = SeriesPeriodicity.Undefined;

         switch (periodicityString)
         {
            case "Sunday":
               result = SeriesPeriodicity.Sunday;
               break;
            case "Monday":
               result = SeriesPeriodicity.Monday;
               break;
            case "Tuesday":
               result = SeriesPeriodicity.Tuesday;
               break;
            case "Wednesday":
               result = SeriesPeriodicity.Wednesday;
               break;
            case "Thursday":
               result = SeriesPeriodicity.Thursday;
               break;
            case "Friday":
               result = SeriesPeriodicity.Friday;
               break;
            case "Saturday":
               result = SeriesPeriodicity.Saturday;
               break;
            case "Daily":
               result = SeriesPeriodicity.Daily;
               break;
         }
         return result;
      }

      public static SeriesRating GetRatingFromString(string ratingString)
      {
         SeriesRating result = SeriesRating.Undefined;
         switch (ratingString)
         {
            case "TV-Y":
               result = SeriesRating.Y;
               break;
            case "TV-Y7":
               result = SeriesRating.YSeven;
               break;
            case "TV-G":
               result = SeriesRating.G;
               break;
            case "TV-PG":
               result = SeriesRating.Pg;
               break;
            case "TV-14":
               result = SeriesRating.Fourteen;
               break;
            case "TV-MA":
               result = SeriesRating.Ma;
               break;
         }

         return result;
      }
      #endregion
   }

   #region Enum definitions
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
   #endregion
}