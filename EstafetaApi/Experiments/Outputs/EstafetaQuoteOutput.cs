using System.Collections.Generic;

namespace EstafetaApi.Experiments.Outputs
{
    public class EstafetaQuoteOutput
    {
        public int From { get; set; }
        public int To { get; set; }
        public List<QuoteInfo> QuoteInfos { get; set; } = new List<QuoteInfo>();
    }

    public class QuoteInfo
    {
        public string Weight { get; set; }
        public Rate Rate { get; set; } = new Rate();
        public string ExtraCharges { get; set; }
        public string Total { get; set; }
        public OverWeight OverWeight { get; set; } = new OverWeight();

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