using System;
using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.HtmlRepresentations.EpisodeFields
{
   public class EpisodeAiredRepresentation : BaseHtmlFieldRepresentation<Episode>
   {
      public EpisodeAiredRepresentation(HtmlNode valueNode) : base(valueNode)
      {
      }

      public override void FillModel(Episode modelToFill)
      {
         DateTime? result = null;
         string dateTimeString = ValueNode.ChildNodes.FindFirst("input").GetAttributeValue("value", string.Empty)
            .Trim();
         if (!string.IsNullOrWhiteSpace(dateTimeString))
         {
            result = DateTime.Parse(dateTimeString);
         }
         
         modelToFill.DateAired = result;
      }
   }
}