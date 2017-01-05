using System.Collections.Generic;

namespace EstafetaApi.Experiments.Outputs
{
    public class EstafetaTrackOutput
    {
        public List<KeyValue> KeyValues { get; set; } = new List<KeyValue>();
        public List<History> Histories { get; set; } = new List<History>();
        public List<KeyValue> OrderProperties { get; set; } = new List<KeyValue>();
    }

    public class History
    {
        public string Date { get; set; }
        public string Place { get; set; }
        public string Comments { get; set; }
    }
    public class KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}