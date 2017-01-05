using CsQuery;
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
            var output = new EstafetaTrackOutput() { KeyValues = new List<KeyValue>() };
            //Table or div with the main estafeta info
            var mainContentDiv = GetMainContentElement(dom, mainSection);

            //Working on 05-01-2017 //Sections are divided by tables

            var sections = GetSections(mainContentDiv);
            for (int i = 0; i < sections.ToList().Count; i++)
            {
                //Get the table
                var table = sections[i];
                //Get the table columns
                var columns = GetColumns(CQ.Create(table));

                foreach (var domObject in columns)
                {
                    var value = domObject[".dato"].Text().Trim();
                    var dato = domObject[".dato"];
                    dato.Remove();
                    var key = domObject.Text();
                    output.KeyValues.Add(new KeyValue()
                    {
                        Key = key,
                        Value = value
                    });
                }
            }


            //This is a div 
            var historyContent = GetMainContentElement(dom, historySection);

            var allRows = GetHistoryRows(historyContent);

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
                output.Histories.Add(history);
            }


            return output;

        }

        private List<CQ> GetHistoryRows(CQ historyContent)
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

        private List<CQ> GetColumns(CQ table)
        {
            var cqList = new List<CQ>();
            var domObjs = table["tbody tr td.encabezado"].ToList();
            foreach (var domObject in domObjs)
            {
                cqList.Add(CQ.Create(domObject));
            }
            return cqList;
        }

        private CQ GetMainContentElement(CQ dom, string section)
        {
            var mainElement = dom[section];
            return mainElement;
        }

        private List<IDomObject> GetSections(CQ dom)
        {
            var doms = dom["table"].ToList();
            return doms;
        }
    }
}