using System.Collections.Generic;

namespace EstafetaApi.Experiments.Outputs
{
    public class EstafetaTrackOutput
    {
        public List<KeyValue> KeyValues { get; set; }
    }

    public class KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}