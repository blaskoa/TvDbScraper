using System.Linq;
using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.HtmlRepresentations.EpisodeFields
{
   public class EpisodeNameRepresentation : BaseHtmlFieldRepresentation<Episode>
   {
      public EpisodeNameRepresentation(HtmlNode valueNode) : base(valueNode)
      {
      }

      public override void FillModel(Episode modelToFill)
      {
         string result = ValueNode.ChildNodes
            .Where(x => "EpisodeName_7".Equals(x.GetAttributeValue("name", string.Empty)))
            .Select(x => x.GetAttributeValue("value", string.Empty))
            .FirstOrDefault();

         modelToFill.EpisodeName = result;
      }
   }
}