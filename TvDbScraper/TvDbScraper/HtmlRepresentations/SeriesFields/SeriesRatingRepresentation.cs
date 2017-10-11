using System.Linq;
using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.HtmlRepresentations.SeriesFields
{
   public class SeriesRatingRepresentation : BaseHtmlFieldRepresentation<Series>
   {
      public SeriesRatingRepresentation(HtmlNode valueNode) : base(valueNode)
      {
      }

      public override void FillModel(Series modelToFill)
      {
         string stringValue = ValueNode.ChildNodes.FindFirst("select").ChildNodes
            .FirstOrDefault(x => x.Attributes.Contains("selected"))?.NextSibling.InnerText.Trim();

         modelToFill.SeriesRating = Series.GetRatingFromString(stringValue);
      }
   }
}