using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.HtmlRepresentations
{
   public abstract class BaseHtmlFieldRepresentation <T> where T: BaseModel
   {
      protected readonly HtmlNode ValueNode;

      protected BaseHtmlFieldRepresentation(HtmlNode valueNode)
      {
         ValueNode = valueNode;
      }

      public abstract void FillModel(T modelToFill);

   }
}
