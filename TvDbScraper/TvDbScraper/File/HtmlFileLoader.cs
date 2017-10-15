using System.IO;
using HtmlAgilityPack;

namespace TvDbScraper.File
{
   public class HtmlFileLoader
   {
      private HttpManager _httpManager;
      private const string FolderName = "raw_html";

      public HtmlFileLoader()
      {
         _httpManager = new HttpManager();

         DirectoryInfo directoryInfo = new DirectoryInfo(FolderName);
         if (!directoryInfo.Exists)
         {
            directoryInfo.Create();
         }
      }

      public HtmlDocument GetDocument(FileRepresentation file)
      {
         FileInfo fileInfo = new FileInfo(Path.Combine(FolderName, file.GetFileName()));
         string htmlString;
         if (!fileInfo.Exists)
         {
            htmlString = _httpManager.GetWebPage(file.Url);
            using (StreamWriter stream = fileInfo.CreateText())
            {
               stream.WriteLine(htmlString);
               stream.Flush();
            }
         }
         else
         {
            using (StreamReader stream = fileInfo.OpenText())
            {
               htmlString = stream.ReadToEnd();
            }
         }

         HtmlDocument document = new HtmlDocument();
         document.LoadHtml(htmlString);

         return document;
      }
      
   }
}