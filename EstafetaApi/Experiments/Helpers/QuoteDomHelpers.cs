using CsQuery;
using EstafetaApi.Experiments.Outputs;
using System.Collections.Generic;
using System.Linq;

namespace EstafetaApi.Experiments.Helpers
{
    public static class QuoteDomHelpers
    {
        private const string tableElement = "table";
        public static CQ GetMainContentElement(string fullHtml)
        {
            var dom = CQ.Create(fullHtml);
            var tables = dom.Find(tableElement);
            var table = tables.Skip(4).FirstOrDefault();
            return table != null ? CQ.Create(table) : dom;
        }

        public static List<CQ> GetDataRows(CQ mainContentElement)
        {
            var list = new List<CQ>();
            var rows = mainContentElement.Find("tr.style1");
            foreach (var row in rows)
            {
                list.Add(CQ.Create(row));
            }
            return list;
        }

        public static List<QuoteInfo> GetQuoteInfos(List<CQ> rows)
        {
            var quoteInfos = new List<QuoteInfo>();
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
                quoteInfos.Add(quoteInfo);
            }
            return quoteInfos;
        }
        private static List<CQ> GetTdsFromRow(CQ row)
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