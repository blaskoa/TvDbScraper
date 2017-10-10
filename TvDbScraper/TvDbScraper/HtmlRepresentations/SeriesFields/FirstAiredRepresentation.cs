using System;
using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.HtmlRepresentations.SeriesFields
{
   public class FirstAiredRepresentation : BaseHtmlFieldRepresentation<Series>
   {
      public FirstAiredRepresentation(HtmlNode valueNode) : base(valueNode)
      {
      }

      public override void FillModel(Series modelToFill)
      {
         DateTime result = DateTime.Parse(ValueNode.ChildNodes.FindFirst("input").GetAttributeValue("value", string.Empty));
         modelToFill.FirstTimeAired = result;
      }
   }
}