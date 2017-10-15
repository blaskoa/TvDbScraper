using System.Linq;
using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.HtmlRepresentations.SeriesFields
{
   public class SeriesGenreRepresentation : BaseHtmlFieldRepresentation<Series>
   {
      private const string InputFieldName = "input";
      private const string ValueAttributeName = "value";
      private const string ValueDelimiter = "|";
      public SeriesGenreRepresentation(HtmlNode valueNode) : base(valueNode)
      {
      }

      public override void FillModel(Series modelToFill)
      {
         HtmlNode input = ValueNode.ChildNodes.FindFirst(InputFieldName);

         string value = input.GetAttributeValue(ValueAttributeName, string.Empty);

         modelToFill.SeriesGenres 
            = value.Split(ValueDelimiter.ToCharArray())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();
      }
   }
}