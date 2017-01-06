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

            output.KeyValues = TrackDomHelpers.BuildKeyValues(sections);

            //This is a div

            var historyContent = TrackDomHelpers.GetMainContentElement(dom, historySection);

            var allRows = TrackDomHelpers.GetHistoryRows(historyContent);

            output.Histories = TrackDomHelpers.BuildStories(allRows);

            return output;

        }



        public object GetQuoteResutsFromHtml(string fullHtml)
        {
            throw new System.NotImplementedException();
        }

    }
}