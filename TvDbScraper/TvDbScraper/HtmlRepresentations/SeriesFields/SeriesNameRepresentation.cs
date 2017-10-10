using System.Linq;
using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.HtmlRepresentations.SeriesFields
{
   public class SeriesNameRepresentation : BaseHtmlFieldRepresentation<Series>
   {
      private const string NameAttributeName = "name";
      private const string NameAttributeValue = "SeriesName_7";
      private const string ValueAttributeName = "value";
      public SeriesNameRepresentation(HtmlNode valueNode) : base(valueNode)
      {
      }

      public override void FillModel(Series modelToFill)
      {
         string result = ValueNode.ChildNodes
             .Where(x => NameAttributeValue.Equals(x.GetAttributeValue(NameAttributeName, string.Empty)))
             .Select(x => x.GetAttributeValue(ValueAttributeName, string.Empty))
             .FirstOrDefault();

         modelToFill.Name = result;
      }
   }
}