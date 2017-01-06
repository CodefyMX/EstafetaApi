using CsQuery;
using EstafetaApi.Experiments.Helpers;
using EstafetaApi.Experiments.Outputs;
using System.Collections.Generic;
using System.Linq;

namespace EstafetaApi.Experiments
{
    public class DomAnalyzer
    {
        private string mainSection = "#rastreoContent";
        private string historySection = ".tablaIconografica2"; // lol
        //16 cols 
        public EstafetaTrackOutput Get22TrackInfoFromHtml(string fullHtml)
        {
            CQ dom = fullHtml;
            var output = new EstafetaTrackOutput();

            //Table or div with the main estafeta info
            var mainContentDiv = TrackDomHelpers.GetMainContentElement(dom, mainSection);

            //Working on 05-01-2017 //Sections are divided by tables

            var sections = TrackDomHelpers.GetSections(mainContentDiv);

            output.KeyValues = TrackDomHelpers.BuildKeyValues(sections);

            //This is a div

            var historyContent = TrackDomHelpers.GetMainContentElement(dom, historySection);

            var allRows = TrackDomHelpers.GetHistoryRows(historyContent);

            output.Histories = TrackDomHelpers.BuildStories(allRows);

            return output;

        }



        public EstafetaQuoteOutput GetQuoteResutsFromHtml(string fullHtml)
        {
            var output = new EstafetaQuoteOutput();
            var mainContentElement = QuoteDomHelpers.GetMainContentElement(fullHtml);

            var rows = QuoteDomHelpers.GetDataRows(mainContentElement);

            foreach (var row in rows)
            {
                var tds = GetTdsFromRow(row);
                var quoteInfo = new QuoteInfo();
                for (int i = 0; i < tds.Count; i++)
                {
                    //Working on 06-01-2017
                    //0 Weight
                    //--Rates--
                    //1 Guide
                    //2 CC 
                    //--!Rates--
                    //3 Extra Charges
                    //--OverWeight
                    //4 Cost
                    //5 CC
                    //--!OverWeight
                    //6 Total 
                    var tdElement = tds[i];
                    if (i == 0)
                    {
                        quoteInfo.Weight = tdElement.Text().Trim();
                    }
                    if (i == 1)
                    {
                        quoteInfo.Rate.Guide = tdElement.Text().Trim();
                    }
                    if (i == 2)
                    {
                        quoteInfo.Rate.Cc = tdElement.Text().Trim();
                    }
                    if (i == 3)
                    {
                        quoteInfo.ExtraCharges = tdElement.Text().Trim();
                    }
                    if (i == 4)
                    {
                        quoteInfo.OverWeight.Cost = tdElement.Text().Trim();
                    }
                    if (i == 5)
                    {
                        quoteInfo.OverWeight.Cc = tdElement.Text().Trim();
                    }
                    if (i == 6)
                    {
                        quoteInfo.Total = tdElement.Text().Trim();
                    }
                }
                output.QuoteInfos.Add(quoteInfo);
            }
            return output;
        }

        private List<CQ> GetTdsFromRow(CQ row)
        {
            var list = new List<CQ>();
            //First td in the data table is empty
            var tdsInRow = row.Find("td").Skip(1).ToList();
            foreach (var td in tdsInRow)
            {
                list.Add(CQ.Create(td));
            }
            return list;
        }
    }
}