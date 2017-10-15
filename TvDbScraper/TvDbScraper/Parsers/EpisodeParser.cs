using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.Parsers
{
   public class EpisodeParser
   {
      private readonly HtmlDocument _document;

      public EpisodeParser(HtmlDocument document)
      {
         _document = document;
      }

      public Episode ParseFromDocument()
      {
         Episode result = new Episode();

         HtmlNode dataTable = _document.GetElementbyId("datatable");

         return result;
      }
   }
}