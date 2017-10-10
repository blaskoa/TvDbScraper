using System;
using HtmlAgilityPack;
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
   }
}