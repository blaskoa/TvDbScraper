using System.Net.Http;
using System.Threading.Tasks;

namespace TvDbScraper
{
   public class HttpManager
   {
      public const string TestUrl = "/index.php?tab=series&id=289590";
      private const string BaseUrl = "https://www.thetvdb.com";
      private HttpClient _httpClient;
      

      public HttpManager()
      {
         _httpClient = new HttpClient();
      }

      public string GetWebPage(string url)
      {
         string resultUrl = BaseUrl + url;
         Task<HttpResponseMessage> httpResult = _httpClient.GetAsync(resultUrl);
         var result = httpResult.Result.Content.ReadAsStringAsync();

         return result.Result;
      }

      public static string GetCompleteUri(string link)
      {
         return BaseUrl + link;
      }
   }
}