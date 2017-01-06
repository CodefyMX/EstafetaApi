using CsQuery;
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
    }
}