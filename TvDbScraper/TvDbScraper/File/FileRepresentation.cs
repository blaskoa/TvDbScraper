using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace TvDbScraper.File
{
   public class FileRepresentation
   {
      private const string IdentifierDelimiter = "_";
      private const string FileExtension = ".html";

      public string SeriesId { get; set; }
      public string SeasonId { get; set; }
      public string EpisodeId { get; set; }
      public string Url { get; set; }

      public string GetFileName()
      {
         StringBuilder stringBuilder = new StringBuilder();

         stringBuilder.Append(SeriesId);
         if (!string.IsNullOrWhiteSpace(SeasonId))
         {
            stringBuilder.Append(IdentifierDelimiter);
            stringBuilder.Append(SeasonId);

            if (!string.IsNullOrWhiteSpace(EpisodeId))
            {
               stringBuilder.Append(IdentifierDelimiter);
               stringBuilder.Append(EpisodeId);
            }
         }

         stringBuilder.Append(FileExtension);

         return stringBuilder.ToString();
      }
      public static FileRepresentation FromSeriesId(string seriesId)
      {
         string url = $"/index.php?tab=series&id={seriesId}";
         FileRepresentation result = new FileRepresentation
         {
            Url = url,
            SeriesId = seriesId,
            SeasonId = null,
            EpisodeId = null
         };

         return result;
      }

      public static FileRepresentation FromSeriesLink(string link)
      {
         // /index.php?tab=series&id=289590

         Uri uri = new Uri(HttpManager.GetCompleteUri(link));
         NameValueCollection parameters = HttpUtility.ParseQueryString(uri.Query);

         FileRepresentation result = new FileRepresentation
         {
            Url = link,
            SeriesId = parameters.Get("id"),
            SeasonId = null,
            EpisodeId = null
         };
         
         return result;
      }

      public static FileRepresentation FromSeasonLink(string link)
      {
         // /?tab=season&seriesid=275360&seasonid=636410&amp;lid=7
         
         Uri uri = new Uri(HttpManager.GetCompleteUri(link));
         NameValueCollection parameters = HttpUtility.ParseQueryString(uri.Query);
         FileRepresentation result = new FileRepresentation
         {
            Url = link,
            SeriesId = parameters.Get("seriesid"),
            SeasonId = parameters.Get("seasonid"),
            EpisodeId = null
         };

         return result;
      }

      public static FileRepresentation FromEpisodeLink(string link)
      {
         // /?tab=episode&seriesid=289590&seasonid=606757&id=5077068&amp;lid=7
         Uri uri = new Uri(HttpManager.GetCompleteUri(link));
         NameValueCollection parameters = HttpUtility.ParseQueryString(uri.Query);
         FileRepresentation result = new FileRepresentation
         {
            Url = link,
            SeriesId = parameters.Get("seriesid"),
            SeasonId = parameters.Get("seasonid"),
            EpisodeId = parameters.Get("id")
         };

         return result;
      }
   }
}
