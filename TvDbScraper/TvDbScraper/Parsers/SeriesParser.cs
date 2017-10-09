using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using TvDbScraper.HtmlRepresentations;
using TvDbScraper.Model;

namespace TvDbScraper.Parsers
{
   public class SeriesParser
   {
      private const string FormRootId = "seriesform";
      private const string DatatableId = "datatable";
      private const string TableRowHtmlName = "tr";
      private const string TableCellHtmlName = "td";
      private const string FormHtmlName = "form";
      private readonly HtmlDocument _seriesPage;

      public SeriesParser(HtmlDocument seriesPage)
      {
         _seriesPage = seriesPage;
      }

      public Series ParseFromDocument()
      {
         Series result = new Series();
         //todo also parse rating
         HtmlNode form = _seriesPage.GetElementbyId(FormRootId).ChildNodes.FindFirst(FormHtmlName);
         HtmlNode table = form.ChildNodes.FirstOrDefault(x => DatatableId.Equals(x.Id));

         if (table != null)
         {
            foreach (HtmlNode tableRow in table.ChildNodes.Where(x => TableRowHtmlName.Equals(x.Name)))
            {
               List<HtmlNode> tableCells = tableRow.ChildNodes.Where(x => TableCellHtmlName.Equals(x.Name)).ToList();
               Tuple<HtmlNode, HtmlNode> htmlFieldTuple = new Tuple<HtmlNode, HtmlNode>(tableCells[0], tableCells[1]);
               BaseHtmlFieldRepresentation<Series> fieldRepresentation = 
                  HtmlFieldRepresentationFactory.FromHtmlTableCellTupleForSeries(htmlFieldTuple);

               fieldRepresentation.FillModel(result);
            }
         }
         
         return result;
      }
   }
}