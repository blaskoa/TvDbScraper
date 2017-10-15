using System.Linq;
using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.HtmlRepresentations.SeriesFields
{
   public class SeriesOverviewRepresentation : BaseHtmlFieldRepresentation<Series>
   {
      public SeriesOverviewRepresentation(HtmlNode valueNode) : base(valueNode)
      {
      }

      public override void FillModel(Series modelToFill)
      {
         modelToFill.Overview = ValueNode.ChildNodes.FirstOrDefault(x => "Overview_7".Equals(x.GetAttributeValue("name", string.Empty)))?.InnerText.Trim();
      }
   }
}