using System.Linq;
using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.HtmlRepresentations.SeriesFields
{
   public class SeriesStatusRepresentation : BaseHtmlFieldRepresentation<Series>
   {
      private const string SelectElementName = "select";
      private const string SelectedAttributeName = "selected";
      private const string ValueAttributeName = "value";

      public SeriesStatusRepresentation(HtmlNode valueNode) : base(valueNode)
      {
      }

      public override void FillModel(Series modelToFill)
      {
         HtmlNode select = ValueNode.ChildNodes.FindFirst(SelectElementName);
         HtmlNode selectedOption = select.ChildNodes.FirstOrDefault(x => x.Attributes.Contains(SelectedAttributeName));
         string selectedValue = selectedOption?.GetAttributeValue(ValueAttributeName, string.Empty);

         modelToFill.SeriesStatus = selectedValue;
      }
   }
}