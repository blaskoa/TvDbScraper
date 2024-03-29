﻿using System;
using HtmlAgilityPack;
using TvDbScraper.Model;

namespace TvDbScraper.HtmlRepresentations.SeriesFields
{
   public class FirstAiredRepresentation : BaseHtmlFieldRepresentation<Series>
   {
      public FirstAiredRepresentation(HtmlNode valueNode) : base(valueNode)
      {
      }

      public override void FillModel(Series modelToFill)
      {
         DateTime? result = null;
         string dateTimeString = ValueNode.ChildNodes.FindFirst("input").GetAttributeValue("value", string.Empty)
            .Trim();
         if (!string.IsNullOrWhiteSpace(dateTimeString))
         {
            result = DateTime.Parse(dateTimeString);
         }

         modelToFill.FirstTimeAired = result;
      }
   }
}