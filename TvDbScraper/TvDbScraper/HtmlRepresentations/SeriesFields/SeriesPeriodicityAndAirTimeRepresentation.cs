using System;
using System.Linq;
using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.HtmlRepresentations.SeriesFields
{
   public class SeriesPeriodicityAndAirTimeRepresentation : BaseHtmlFieldRepresentation<Series>
   {
      public SeriesPeriodicityAndAirTimeRepresentation(HtmlNode valueNode) : base(valueNode)
      {
      }

      public override void FillModel(Series modelToFill)
      {
         FillPeriodicity(modelToFill);
         FillAirTime(modelToFill);
      }

      private void FillPeriodicity(Series modelToFill)
      {
         HtmlNode periodicitySelect = ValueNode.ChildNodes.FindFirst("select");
         HtmlNode selectedOption = periodicitySelect.ChildNodes.FirstOrDefault(x => x.Attributes.Contains("selected"));
         modelToFill.SeriesPeriodicity = Series.GetPeriodicityFromString(selectedOption?.NextSibling.InnerText.Trim());
      }
      private void FillAirTime(Series modelToFill)
      {
         HtmlNode airTimeInput = ValueNode.ChildNodes.FindFirst("input");
         modelToFill.AirTime = DateTime.Parse(airTimeInput.GetAttributeValue("value", string.Empty));
      }
   }
}