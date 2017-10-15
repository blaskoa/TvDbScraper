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
      static void Main(string[] args)
      {
         HtmlFileLoader fileLoader = new HtmlFileLoader();

         HtmlDocument document = fileLoader.GetDocument(FileRepresentation.FromSeriesId("289590"));
         SeriesParser seriesParser = new SeriesParser(document);

         Series series = seriesParser.ParseFromDocument();

         List<string> seasonsLinks = seriesParser.GetSeasonsLinks();
         List<FileRepresentation> seasonFiles = seasonsLinks.Select(FileRepresentation.FromSeasonLink).ToList();

         foreach (FileRepresentation seasonFile in seasonFiles)
         {
            HtmlDocument seasonDocument = fileLoader.GetDocument(seasonFile);
            SeasonParser seasonParser = new SeasonParser(seasonDocument);

            Season season = seasonParser.ParseFromDocument();
            series.Seasons.Add(season);

            List<string> episodeLinks = seasonParser.GetEpisodeLinks();
            List<FileRepresentation> episodeFiles = episodeLinks.Select(FileRepresentation.FromEpisodeLink).ToList();

            foreach (FileRepresentation episodeFile in episodeFiles)
            {
               HtmlDocument episodeDocument = fileLoader.GetDocument(episodeFile);
               EpisodeParser episodeParser = new EpisodeParser(episodeDocument);

               Episode episode = episodeParser.ParseFromDocument();
               season.Episodes.Add(episode);
            }
         }


      }
   }
}
