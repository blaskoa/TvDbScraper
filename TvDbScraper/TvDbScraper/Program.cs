using System;
using System.IO;
using HtmlAgilityPack;

namespace TvDbScraper
{
   class Program
   {
      private const string TmpFileName = "index.html";
      static void Main(string[] args)
      {

         HttpManager manager = new HttpManager();
         FileInfo fileInfo = new FileInfo(TmpFileName);
         string result;
         if (!fileInfo.Exists)
         {
            result = manager.GetWebPage();
            using (var stream = fileInfo.CreateText())
            {
               stream.WriteLine(result);
               stream.Flush();
            }
         }
         else
         {
            using (var stream = fileInfo.OpenText())
            {
               result = stream.ReadToEnd();
            }
         }
         
         
         HtmlDocument document = new HtmlDocument();
         document.LoadHtml(result);

         Console.Out.WriteLine(result);
         //Console.ReadLine();

      }
   }
}
