using System.Net.Http;
using System.Threading.Tasks;

namespace TvDbScraper
{
   public class HttpManager
   {
      private const string BaseUrl = "https://www.thetvdb.com/index.php?tab=series&id=275360";
      private HttpClient _httpClient;
      

      public HttpManager()
      {
         _httpClient = new HttpClient();
      }

      public string GetWebPage()
      {
         Task<HttpResponseMessage> httpResult = _httpClient.GetAsync(BaseUrl);
         var result = httpResult.Result.Content.ReadAsStringAsync();

         return result.Result;
      }
   }
}