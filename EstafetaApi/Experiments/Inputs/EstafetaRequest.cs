namespace EstafetaApi.Experiments.Inputs
{
    public class EstafetaRequest : ITrackRequest
    {
        public int waybillType { get; set; }
        public string wayBill { get; set; }
    }

    public interface ITrackRequest
    {
        /// <summary>
        /// Track code
        /// </summary>
        string wayBill { get; set; }
    }
}