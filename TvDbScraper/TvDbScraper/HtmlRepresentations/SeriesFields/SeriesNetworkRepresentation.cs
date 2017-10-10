using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.HtmlRepresentations.SeriesFields
{
   public class SeriesNetworkRepresentation : BaseHtmlFieldRepresentation<Series>
   {
      public SeriesNetworkRepresentation(HtmlNode valueNode) : base(valueNode)
      {
      }

      public override void FillModel(Series modelToFill)
      {
         modelToFill.Network = ValueNode.ChildNodes.FindFirst("input").GetAttributeValue("value", string.Empty);
      }
   }
}