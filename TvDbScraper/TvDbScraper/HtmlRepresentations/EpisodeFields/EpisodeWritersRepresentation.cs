using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.HtmlRepresentations.EpisodeFields
{
   public class EpisodeWritersRepresentation : BaseHtmlFieldRepresentation<Episode>
   {
      public EpisodeWritersRepresentation(HtmlNode valueNode) : base(valueNode)
      {
      }

      public override void FillModel(Episode modelToFill)
      {
         List<string> result = ValueNode.ChildNodes.FindFirst("input").GetAttributeValue("value", string.Empty)
            .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList()
            .Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
         modelToFill.Writers = result;
      }
   }
}