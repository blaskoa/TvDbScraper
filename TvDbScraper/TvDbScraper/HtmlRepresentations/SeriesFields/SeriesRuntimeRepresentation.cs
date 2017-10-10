using System.Linq;
using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.HtmlRepresentations.SeriesFields
{
   public class SeriesRuntimeRepresentation : BaseHtmlFieldRepresentation<Series>
   {
      public SeriesRuntimeRepresentation(HtmlNode valueNode) : base(valueNode)
      {
      }

      public override void FillModel(Series modelToFill)
      {
         string stringValue =
            ValueNode.ChildNodes.FindFirst("select").ChildNodes.FirstOrDefault(x => x.Attributes.Contains("selected"))
               ?.GetAttributeValue("value", null);

         int intValue = stringValue == null ? 0 : int.Parse(stringValue);
         modelToFill.RuntimeInMinutes = intValue;
      }
   }
}