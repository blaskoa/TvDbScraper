using System;
using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.HtmlRepresentations.SeriesFields
{
   public class SeriesIdRepresentation : BaseHtmlFieldRepresentation<Series>
   {
      public SeriesIdRepresentation(HtmlNode valueNode) : base(valueNode)
      {
      }

      public override void FillModel(Series modelToFill)
      {
         int fieldValue = int.Parse(ValueNode.InnerText);
         modelToFill.Id = fieldValue;
      }
   }
}