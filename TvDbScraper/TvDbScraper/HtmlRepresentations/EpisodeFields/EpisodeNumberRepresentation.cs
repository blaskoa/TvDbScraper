using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.HtmlRepresentations.EpisodeFields
{
   public class EpisodeNumberRepresentation : BaseHtmlFieldRepresentation<Episode>
   {
      public EpisodeNumberRepresentation(HtmlNode valueNode) : base(valueNode)
      {
      }

      public override void FillModel(Episode modelToFill)
      {
         int fieldValue = int.Parse(ValueNode.ChildNodes.FindFirst("input").GetAttributeValue("value", string.Empty));
         modelToFill.EpisodeNumber = fieldValue;
      }
   }
}