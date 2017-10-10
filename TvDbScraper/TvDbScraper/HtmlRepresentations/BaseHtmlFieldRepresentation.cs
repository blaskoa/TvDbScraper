using HtmlAgilityPack;

namespace TvDbScraper.HtmlRepresentations
{
   public abstract class BaseHtmlFieldRepresentation<T>
   {
      protected readonly HtmlNode ValueNode;

      protected BaseHtmlFieldRepresentation(HtmlNode valueNode)
      {
         ValueNode = valueNode;
      }

      public abstract void FillModel(T modelToFill);

   }
}
