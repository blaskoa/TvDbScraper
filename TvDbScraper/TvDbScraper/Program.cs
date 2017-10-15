using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Nest;
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
         List<string> seriesIds = new List<string> {"75897", "277165", "79168"};
         seriesIds = seriesIds.Distinct().ToList();

         List<Series> resultSeries = new List<Series>(seriesIds.Count);
         List<Task> crawlingTasks = new List<Task>(seriesIds.Count);

         foreach (string id in seriesIds)
         {
            crawlingTasks.Add(Task.Run(() =>
               {
                  resultSeries.Add(ParseAllSeriesInformation(id));
               }
            ));
         }

         Task.WaitAll(crawlingTasks.ToArray());

         ElasticClient client = new ElasticClient(new ConnectionSettings().DefaultIndex("series"));
         foreach (Series series in resultSeries)
         {
            IIndexResponse response = client.Index(series);
         }
         
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
