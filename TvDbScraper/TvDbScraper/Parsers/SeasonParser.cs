using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.Parsers
{
   public class SeasonParser
   {
      private readonly HtmlDocument _document;
      private List<string> _episodeLinks;

      public SeasonParser(HtmlDocument document)
      {
         _document = document;
      }

      public Season ParseFromDocument()
      {
         Season result = new Season();

         ParseSeasonName(result);
         FillEpisodeLinks();
         result.EpisodeCount = _episodeLinks.Count;

         return result;
      }

      public List<string> GetEpisodeLinks()
      {
         if (_episodeLinks == null)
         {
            FillEpisodeLinks();
         }

         return _episodeLinks;
      }

      private void FillEpisodeLinks()
      {
         HtmlNode episodesTable = _document.GetElementbyId("listtable");

         IEnumerable<HtmlNode> tableRows = episodesTable.ChildNodes.Where(x => "tr".Equals(x.Name)).ToList();
         HtmlNode firstRow = tableRows.First();
         IEnumerable<HtmlNode> episodeRows = tableRows.Except(new[] {firstRow}).ToList();

         _episodeLinks = episodeRows.Select(x => x.ChildNodes.FindFirst("td").ChildNodes.FindFirst("a")
            .GetAttributeValue("href", string.Empty)).ToList();
      }

      private void ParseSeasonName(Season result)
      {
         HtmlNode nameDiv = _document.DocumentNode.SelectSingleNode("//div[contains(@class, \'titlesection\')]");
         result.Name = nameDiv.ChildNodes.FindFirst("h2").InnerText.Split('&').First().Trim();
      }
   }
}