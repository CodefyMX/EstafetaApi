using System.Collections.Generic;

namespace EstafetaApi.Experiments.Outputs
{
    public class EstafetaQuoteOutput
    {
        public string From { get; set; }
        public string To { get; set; }
        public List<QuoteInfo> QuoteInfos { get; set; }
    }

    public class QuoteInfo
    {
        public string Weight { get; set; }
        public Rate Rate { get; set; } = new Rate();
        public string ExtraCharges { get; set; }
        public string Total { get; set; }
        public OverWeight OverWeight { get; set; }

    }

    public class Rate
    {
        public string Guide { get; set; }
        public string Cc { get; set; }
    }

    public class OverWeight
    {
        public string Cost { get; set; }
        public string Cc { get; set; }
    }
}