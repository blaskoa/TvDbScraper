using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.HtmlRepresentations.EpisodeFields
{
   public class EpisodeProductionCodeRepresentation : BaseHtmlFieldRepresentation<Episode>
   {
      public EpisodeProductionCodeRepresentation(HtmlNode valueNode) : base(valueNode)
      {
      }

      public override void FillModel(Episode modelToFill)
      {
         string result = ValueNode.ChildNodes.FindFirst("input").GetAttributeValue("value", string.Empty);
         modelToFill.ProductionCode = result;
      }
   }
}