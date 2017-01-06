using CsQuery;
using EstafetaApi.Experiments.Helpers;
using EstafetaApi.Experiments.Outputs;

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

            //This should not exists at all!!! -> output.KeyValues = TrackDomHelpers.BuildKeyValues(sections);

            output.EstafetaTrackObj = TrackDomHelpers.BuildKeyValuesObjectStrategy(sections);

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

            output.QuoteInfos = QuoteDomHelpers.GetQuoteInfos(rows);
            return output;
        }


    }
}