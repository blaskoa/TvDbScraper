using System;
using HtmlAgilityPack;
using TvDbScraper.HtmlRepresentations.EpisodeFields;
using TvDbScraper.HtmlRepresentations.SeriesFields;
using TvDbScraper.Model;
using TvDbScraper.Parsers;

namespace TvDbScraper.HtmlRepresentations
{
   public class HtmlFieldRepresentationFactory
   {
      public static BaseHtmlFieldRepresentation<Series> FromHtmlTableCellTupleForSeries(Tuple<HtmlNode, HtmlNode> tableCells)
      {
         BaseHtmlFieldRepresentation<Series> result = null;
         string fieldName = tableCells.Item1.FirstChild.InnerText;
         HtmlNode valueNode = tableCells.Item2;

         switch (fieldName)
         {
            case "Series ID:":
               result = new SeriesIdRepresentation(valueNode);
               break;
            case "Series Name:":
               result = new SeriesNameRepresentation(valueNode);
               break;
            case "Status:":
               result = new SeriesStatusRepresentation(valueNode);
               break;
            case "Genre: ":
               result = new SeriesGenreRepresentation(valueNode);
               break;
            case "First Aired:":
               result = new FirstAiredRepresentation(valueNode);
               break;
            case "Airs:":
               result = new SeriesPeriodicityAndAirTimeRepresentation(valueNode);
               break;
            case "Original Network:":
               result = new SeriesNetworkRepresentation(valueNode);
               break;
            case "Runtime:":
               result = new SeriesRuntimeRepresentation(valueNode);
               break;
            case "Rating:":
               result = new SeriesRatingRepresentation(valueNode);
               break;
            case "Overview: ":
               result = new SeriesOverviewRepresentation(valueNode);
               break;
            //ignored fields
            case "TV.com ID:":
            case "IMDB.com ID:":
            case "Zap2it / SchedulesDirect ID:":
               break;
            default:
               throw new UnknownFieldException("Unknown field for Series \"" + fieldName + "\"");
         }
         return result;
      }

      public static BaseHtmlFieldRepresentation<Episode> FromHtmlTableCellTupleForEpisode(Tuple<HtmlNode, HtmlNode> tableCells)
      {
         BaseHtmlFieldRepresentation<Episode> result = null;
         string fieldName = tableCells.Item1.FirstChild.InnerText;
         HtmlNode valueNode = tableCells.Item2;

         switch (fieldName)
         {
            case "Aired Episode Number:":
               result = new EpisodeNumberRepresentation(valueNode);
               break;
            case "Episode Name: ":
               result = new EpisodeNameRepresentation(valueNode);
               break;
            case "First Aired:":
               result = new EpisodeAiredRepresentation(valueNode);
               break;
            case "Guest Stars:":
               result = new EpisodeGuestStarsRepresentation(valueNode);
               break;
            case "Director:":
               result = new EpisodeDirectorRepresentation(valueNode);
               break;
            case "Writer:":
               result = new EpisodeWritersRepresentation(valueNode);
               break;
            case "Production Code:":
               result = new EpisodeProductionCodeRepresentation(valueNode);
               break;
            case "Overview: ":
               result = new EpisodeOverviewRepresentation(valueNode);
               break;
            //ignored fields
            case "DVD Disc ID:":
            case "DVD Season:":
            case "DVD Episode Number:":
            case "DVD Chapter:":
            case "Absolute Number:":
            case "IMDB.com ID:":
            case "Airs After Season:":
            case "Airs Before:":
            case "Is Movie:":
            case "Last Edited By:":
               //todo somehow also ignore the last field
               break;
            default:
               throw new UnknownFieldException("Unknown field for Episode \"" + fieldName + "\"");
         }
         return result;
      }
   }
}