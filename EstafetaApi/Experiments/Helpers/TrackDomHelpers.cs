using CsQuery;
using EstafetaApi.Experiments.Outputs;
using System.Collections.Generic;
using System.Linq;

namespace EstafetaApi.Experiments.Helpers
{
    public static class TrackDomHelpers
    {
        public static List<CQ> GetHistoryRows(CQ historyContent)
        {
            var cqList = new List<CQ>();

            var table = historyContent.Find("table").First();

            var domObjs = CQ.Create(table.Html())["tr"].Skip(1).ToList();
            foreach (var domObject in domObjs)
            {
                cqList.Add(CQ.Create(domObject));
            }
            return cqList;
        }

        public static List<CQ> GetColumns(CQ table)
        {
            var cqList = new List<CQ>();
            var domObjs = table["tbody tr td.encabezado"].ToList();
            foreach (var domObject in domObjs)
            {
                cqList.Add(CQ.Create(domObject));
            }
            return cqList;
        }

        public static CQ GetMainContentElement(CQ dom, string section)
        {
            var mainElement = dom[section];
            return mainElement;
        }

        public static List<IDomObject> GetSections(CQ dom)
        {
            var doms = dom["table"].ToList();
            return doms;
        }

        public static List<History> BuildStories(List<CQ> allRows)
        {
            var histories = new List<History>();
            foreach (var allRow in allRows)
            {
                var tds = allRow["td"];
                var history = new History();
                for (int i = 0; i < tds.Length; i++)
                {
                    //Date
                    if (i == 0)
                    {
                        history.Date = tds[i].InnerText;
                    }
                    //Place
                    if (i == 1)
                    {
                        history.Place = tds[i].InnerText;
                    }
                    //Comments
                    if (i == 2)
                    {
                        history.Comments = tds[i].InnerText;
                    }
                }
                histories.Add(history);
            }
            return histories;
        }

        public static List<KeyValue> BuildKeyValues(List<IDomObject> sections)
        {
            var keyValues = new List<KeyValue>();
            for (int i = 0; i < sections.ToList().Count; i++)
            {
                //Get the table
                var table = sections[i];
                //Get the table columns
                var columns = TrackDomHelpers.GetColumns(CQ.Create(table));

                foreach (var domObject in columns)
                {
                    var value = domObject[".dato"].Text().Trim();
                    var dato = domObject[".dato"];
                    dato.Remove();
                    var key = domObject.Text();
                    keyValues.Add(new KeyValue()
                    {
                        Key = key,
                        Value = value
                    });
                }
            }
            return keyValues;
        }



    }
}