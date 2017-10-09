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
         BaseHtmlFieldRepresentation<Series> result;
         string fieldName = tableCells.Item1.FirstChild.InnerText;
         switch (fieldName)
         {
            case "Series ID:":
               result = new SeriesIdRepresentation(tableCells.Item2);
               break;
            default:
               throw new UnknownFieldException("Unknown field for Series \"" + fieldName + "\"");
         }
         return result;
      }
   }
}