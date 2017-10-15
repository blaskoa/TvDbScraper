using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.HtmlRepresentations.EpisodeFields
{
   public class EpisodeGuestStarsRepresentation : BaseHtmlFieldRepresentation<Episode>
   {
      public EpisodeGuestStarsRepresentation(HtmlNode valueNode) : base(valueNode)
      {
      }

      public override void FillModel(Episode modelToFill)
      {
         List<string> result = ValueNode.ChildNodes.FindFirst("input").GetAttributeValue("value", string.Empty)
            .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList()
            .Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
         modelToFill.GuestStars = result;
      }
   }
}