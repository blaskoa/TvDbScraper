using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using TvDbScraper.File;
using TvDbScraper.Model;
using TvDbScraper.Parsers;

namespace TvDbScraper
{
   class Program
   {
      private static HtmlFileLoader _fileLoader;
      static void Main(string[] args)
      {
         _fileLoader = new HtmlFileLoader();

         Series series = ParseAllSeriesInformation("289590");
      }

      private static Series ParseAllSeriesInformation(string seriesId)
      {
         HtmlDocument document = _fileLoader.GetDocument(FileRepresentation.FromSeriesId(seriesId));
         SeriesParser seriesParser = new SeriesParser(document);

         Series series = seriesParser.ParseFromDocument();

         List<string> seasonsLinks = seriesParser.GetSeasonsLinks();
         List<FileRepresentation> seasonFiles = seasonsLinks.Select(FileRepresentation.FromSeasonLink).ToList();

         foreach (FileRepresentation seasonFile in seasonFiles)
         {
            HtmlDocument seasonDocument = _fileLoader.GetDocument(seasonFile);
            SeasonParser seasonParser = new SeasonParser(seasonDocument);

            Season season = seasonParser.ParseFromDocument(seasonFile.SeasonId);
            series.Seasons.Add(season);

            List<string> episodeLinks = seasonParser.GetEpisodeLinks();
            List<FileRepresentation> episodeFiles = episodeLinks.Select(FileRepresentation.FromEpisodeLink).ToList();

            foreach (FileRepresentation episodeFile in episodeFiles)
            {
               HtmlDocument episodeDocument = _fileLoader.GetDocument(episodeFile);
               EpisodeParser episodeParser = new EpisodeParser(episodeDocument);

               Episode episode = episodeParser.ParseFromDocument(episodeFile.EpisodeId);
               season.Episodes.Add(episode);
            }
         }
         return series;
      }
   }
}
