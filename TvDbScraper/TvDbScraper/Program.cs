using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HtmlAgilityPack;
using MySql.Data.MySqlClient;
using Nest;
using TvDbScraper.Database.Model;
using TvDbScraper.File;
using TvDbScraper.Model;
using TvDbScraper.Parsers;

namespace TvDbScraper
{
   class Program
   {
      private static HtmlFileLoader _fileLoader;
      private const string ConnectionString = "Server=localhost; database=tvdb; UID=root; password=root";
      private const bool ReadFromDatabase = true;
      private const int pageSize = 500;

      static void Main(string[] args)
      {
         _fileLoader = new HtmlFileLoader();
         List<string> seriesIds = new List<string> {"75897", "277165", "79168"};
         seriesIds = seriesIds.Distinct().ToList();



         List<Series> resultSeries = new List<Series>(seriesIds.Count);
         IEnumerable<DenormalizedTvEpisode> episodes;
         
         if (!ReadFromDatabase)
         {
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
         }
         else
         {
            using (IDbConnection connection = new MySqlConnection(ConnectionString))
            {
               ElasticClient client = new ElasticClient(new ConnectionSettings().DefaultIndex("series"));

               int numberOfResult = connection.QueryFirst<int>("select count(*) from tvdb.tvseries");
               for (int page = 0; page < numberOfResult; page += pageSize)
               {
                  IEnumerable<int> seriesIdsFromDb = connection.Query<int>($"select id from tvdb.tvseries order by id limit {page},{pageSize}").ToList();
                  int firstId = seriesIdsFromDb.First();
                  int lastId = seriesIdsFromDb.Last();

                  episodes = connection.Query<DenormalizedTvEpisode>(DenormalizedTvEpisode.DbSelect, new { firstId, lastId });
                  var response = client.IndexMany(episodes.Select(x => x.ToDomainModel()));

               }
            }
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
