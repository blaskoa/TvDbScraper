using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using TvDbScraper.HtmlRepresentations;
using TvDbScraper.Model;

namespace TvDbScraper.Parsers
{
   public class SeriesParser
   {
      private const string FormRootId = "seriesform";
      private const string DatatableId = "datatable";
      private const string TableRowHtmlName = "tr";
      private const string TableCellHtmlName = "td";
      private const string FormHtmlName = "form";
      private readonly HtmlDocument _seriesPage;

      public SeriesParser(HtmlDocument seriesPage)
      {
         _seriesPage = seriesPage;
      }

      public Series ParseFromDocument()
      {
         Series result = new Series();

         ParseAndFillUserRatings(result);
         ParseAndFillNumberOfRatings(result);

         HtmlNode form = _seriesPage.GetElementbyId(FormRootId).ChildNodes.FindFirst(FormHtmlName);
         HtmlNode table = form.ChildNodes.FirstOrDefault(x => DatatableId.Equals(x.Id));

         if (table != null)
         {
            foreach (HtmlNode tableRow in table.ChildNodes.Where(x => TableRowHtmlName.Equals(x.Name)))
            {
               List<HtmlNode> tableCells = tableRow.ChildNodes.Where(x => TableCellHtmlName.Equals(x.Name)).ToList();
               Tuple<HtmlNode, HtmlNode> htmlFieldTuple = new Tuple<HtmlNode, HtmlNode>(tableCells[0], tableCells[1]);
               BaseHtmlFieldRepresentation<Series> fieldRepresentation =
                  HtmlFieldRepresentationFactory.FromHtmlTableCellTupleForSeries(htmlFieldTuple);

               fieldRepresentation?.FillModel(result);
            }
         }

         return result;
      }

      public List<string> GetSeasonsLinks()
      {
         List<string> result = new List<string>();
         HtmlNodeCollection nodes = _seriesPage.DocumentNode.SelectNodes("//div[contains(@id,\'content\')]");
         HtmlNode seriesNode = nodes.FirstOrDefault(x => "Seasons".Equals(x.ChildNodes.FindFirst("h1").InnerText));

         if (seriesNode != null)
         {
            result = seriesNode.ChildNodes.Where(x => "a".Equals(x.Name) && !"All".Equals(x.InnerText))
               .Select(x => x.GetAttributeValue("href", string.Empty)).ToList();
         }

         return result;
      }

      private void ParseAndFillNumberOfRatings(Series result)
      {
         HtmlNode numberOfRatings = _seriesPage.GetElementbyId("smalltext");
         string ratingCount = numberOfRatings.InnerText.Trim().Split(' ')[0];

         result.NumberOfRatings = int.Parse(ratingCount);
      }

      private void ParseAndFillUserRatings(Series result)
      {
         HtmlNode fanArtDiv = _seriesPage.GetElementbyId("fanart");
         int countOfStars = fanArtDiv.Descendants("img")
            .Count(x => "/images/largestar_on.gif".Equals(x.GetAttributeValue("src", string.Empty)));
         result.Rating = countOfStars;
      }
   }
}