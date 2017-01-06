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
                    var value = tds[i].InnerText;
                    value = value.Replace("\n", "");
                    value = value.Replace("  ", "");
                    //Date
                    if (i == 0)
                    {
                        history.Date = value;
                    }
                    //Place
                    if (i == 1)
                    {
                        history.Place = value;
                    }
                    //Comments
                    if (i == 2)
                    {
                        history.Comments = value;
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
                var columns = GetColumns(CQ.Create(table));

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

        public static EstafetaTrackObj BuildKeyValuesObjectStrategy(List<IDomObject> sections)
        {
            var obj = new EstafetaTrackObj();
            var realIndex = 0;
            var colAcum = 0;
            for (int tIndex = 0; tIndex < sections.Count; tIndex++)
            {
                //Get the table columns
                var columns = GetColumns(CQ.Create(sections[tIndex]));

                //Working on 06-01-2017
                //0 GuideNumber
                //1 TrackCode
                //2 Origin
                //3 Destiny
                //4 CpDestiny
                //5 ServiceStatus
                //6 ReceivedBy
                //7 Service
                //8 DeliveryTime
                //9 TypeOfDelivery
                //10 DeliveryDate 
                for (int i = 0; i < columns.Count; i++)
                {
                    var value = columns[i][".dato"].Text().Trim();
                    value = value.Replace("\n", "");
                    value = value.Replace("  ", "");
                    realIndex = colAcum;

                    switch (realIndex)
                    {
                        case 0:
                            obj.GuideNumber = value;
                            break;
                        case 1:
                            obj.TrackNumber = value;
                            break;
                        case 2:
                            obj.Origin = value;
                            break;
                        case 3:
                            obj.Destination = value;
                            break;
                        case 4:
                            obj.CpDestiny = value;
                            break;
                        case 5:
                            obj.ServiceStatus = value;
                            break;
                        case 6:

                            //Clean string = 
                            var withOutCFirma = value.Replace("Consulta firma", "");
                            obj.ReceivedBy = withOutCFirma.Trim();
                            break;
                        case 7:
                            obj.Service = value;
                            break;
                        case 8:
                            obj.DeliveryDate = value;
                            break;
                        case 9:
                            obj.TypeOfDelivery = value;
                            break;
                        case 10:
                            obj.EstimatedDeliveryDate = value;
                            break;
                    }
                    colAcum = colAcum + 1;
                }
            }
            foreach (var table in sections)
            {

            }
            return obj;
        }


    }
}