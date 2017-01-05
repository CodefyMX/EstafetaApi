using CsQuery;
using EstafetaApi.Experiments.Outputs;
using System.Collections.Generic;
using System.Linq;

namespace EstafetaApi.Experiments
{
    public class DomAnalyzer
    {
        private string mainSection = "#rastreoContent";
        //16 cols 
        public EstafetaTrackOutput Get22TrackInfoFromHtml(string fullHtml)
        {
            CQ dom = fullHtml;
            var output = new EstafetaTrackOutput() { KeyValues = new List<KeyValue>() };
            //Table or div with the main estafeta info
            var mainContentDiv = GetMainContentElement(dom);

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
            return output;

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

        private CQ GetMainContentElement(CQ dom)
        {
            var mainElement = dom[mainSection];
            return mainElement;
        }

        private List<IDomObject> GetSections(CQ dom)
        {
            var doms = dom["table"].ToList();
            return doms;
        }
    }
}