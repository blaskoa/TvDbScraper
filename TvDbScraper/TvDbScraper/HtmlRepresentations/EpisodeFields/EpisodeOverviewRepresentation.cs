using System.Linq;
using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.HtmlRepresentations.EpisodeFields
{
   public class EpisodeOverviewRepresentation : BaseHtmlFieldRepresentation<Episode>
   {
      public EpisodeOverviewRepresentation(HtmlNode valueNode) : base(valueNode)
      {
      }

      public override void FillModel(Episode modelToFill)
      {
         modelToFill.Overview = ValueNode.ChildNodes.FirstOrDefault(x => "Overview_7".Equals(x.GetAttributeValue("name", string.Empty)))?.InnerText.Trim();
      }
   }
}