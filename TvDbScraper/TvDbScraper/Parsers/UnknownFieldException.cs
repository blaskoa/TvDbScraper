using System;

namespace TvDbScraper.Parsers
{
   public class UnknownFieldException : Exception
   {
      public UnknownFieldException(string message) : base(message)
      {
         
      }
   }
}