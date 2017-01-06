using System.Collections.Generic;

namespace EstafetaApi.Experiments.Outputs
{
    public class EstafetaTrackOutput
    {
        public EstafetaTrackObj EstafetaTrackObj { get; set; } = new EstafetaTrackObj();
        //public List<KeyValue> KeyValues { get; set; } = new List<KeyValue>();
        public List<History> Histories { get; set; } = new List<History>();
        public List<KeyValue> OrderProperties { get; set; } = new List<KeyValue>();
    }

    public class EstafetaTrackObj
    {
        public string GuideNumber { get; set; }
        public string TrackNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string CpDestiny { get; set; }
        public string ServiceStatus { get; set; }
        public string ReceivedBy { get; set; }
        public string Service { get; set; }
        public string DeliveryDate { get; set; }
        public string TypeOfDelivery { get; set; }
        public string EstimatedDeliveryDate { get; set; }
        public string ROrderCode { get; set; }
        public string RDate { get; set; }
    }
    public class History
    {
        public string Date { get; set; }
        public string Place { get; set; }
        public string Comments { get; set; }
    }
    /// <summary>
    /// This should be called PeriUglyfierBonanza
    /// </summary>
    public class KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}