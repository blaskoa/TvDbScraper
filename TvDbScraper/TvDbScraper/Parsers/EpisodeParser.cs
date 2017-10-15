using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using TvDbScraper.HtmlRepresentations;
using TvDbScraper.Model;

namespace TvDbScraper.Parsers
{
   public class EpisodeParser
   {
      private readonly HtmlDocument _document;

      public EpisodeParser(HtmlDocument document)
      {
         _document = document;
      }

      public Episode ParseFromDocument(string id)
      {
         Episode result = new Episode();

         result.Id = int.Parse(id);
         ParseAndFillNumberOfRatings(result);
         ParseAndFillUserRatings(result);
         HtmlNode dataTable = _document.GetElementbyId("datatable");

         if (dataTable != null)
         {
            foreach (HtmlNode tableRow in dataTable.ChildNodes.Where(x => "tr".Equals(x.Name)))
            {
               List<HtmlNode> tableCells = tableRow.ChildNodes.Where(x => "td".Equals(x.Name)).ToList();
               if (tableCells.Count < 2)
               {
                  //skipping footnotes
                  continue;
               }
               Tuple<HtmlNode, HtmlNode> htmlFieldTuple = new Tuple<HtmlNode, HtmlNode>(tableCells[0], tableCells[1]);
               BaseHtmlFieldRepresentation<Episode> fieldRepresentation =
                  HtmlFieldRepresentationFactory.FromHtmlTableCellTupleForEpisode(htmlFieldTuple);

               fieldRepresentation?.FillModel(result);
            }
         }
         return result;
      }



      private void ParseAndFillNumberOfRatings(Episode result)
      {
         HtmlNode numberOfRatings = _document.GetElementbyId("smalltext");
         string ratingCount = numberOfRatings.InnerText.Trim().Split(' ')[0];

         result.UserRatingCount = int.Parse(ratingCount);
      }

      private void ParseAndFillUserRatings(Episode result)
      {
         HtmlNodeCollection sectionDivs = _document.DocumentNode.SelectNodes("//div[contains(@class,\'section\')]");
         HtmlNode ratingSection =
            sectionDivs.FirstOrDefault(x => "Rating".Equals(x.ChildNodes.FindFirst("h1")?.InnerText.Trim()));
         if (ratingSection != null)
         {
            int countOfStars = ratingSection.Descendants("img")
               .Count(x => "/images/star_on.gif".Equals(x.GetAttributeValue("src", string.Empty)));
            result.UserRating = countOfStars;
         }
      }
   }
}