using EstafetaApi.Experiments.Inputs;
using EstafetaApi.Experiments.Outputs;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EstafetaApi.Experiments
{
    /// <summary>
    /// Based on  
    /// </summary>
    public class EstafetaApi
    {
        public EstafetaApi(string trackUrl, string quoteUrl, string signatureUrl, string voucherUrl)
        {
            QuoteUrl = quoteUrl;
            Signature = signatureUrl;
            TrackUrl = trackUrl;
            Voucher = voucherUrl;
        }

        public EstafetaApi()
        {

        }
        //This is a post url
        public string TrackUrl { get; } = "http://www.estafeta.com/Tracking/searchWayBill/";
        public string QuoteUrl { get; } = "http://herramientascs.estafeta.com/Cotizador/Cotizar";
        public string Signature { get; } = "http://rastreo3.estafeta.com";

        public string Voucher { get; } =
            "http://rastreo3.estafeta.com/RastreoWebInternet/consultaEnvio.do?dispatch=doComprobanteEntrega&guiaEst=";
        public async Task<EstafetaTrackOutput> Track(EstafetaRequest input)
        {
            var obj = JsonConvert.SerializeObject(input);
            var buffer = System.Text.Encoding.UTF8.GetBytes(obj);
            var byteContent = new ByteArrayContent(buffer);
            var domAnalyzer = new DomAnalyzer();
            var objResult = domAnalyzer.Get22TrackInfoFromHtml(await GetContentFromUrl(byteContent, TrackUrl));
            return objResult;
        }

        public async Task<EstafetaQuoteOutput> Quote(EstafetaQuoteInput input)
        {
            var obj = JsonConvert.SerializeObject(input);
            var buffer = System.Text.Encoding.UTF8.GetBytes(obj);
            var byteContent = new ByteArrayContent(buffer);
            var domAnalyzer = new DomAnalyzer();
            var objResult = domAnalyzer.GetQuoteResutsFromHtml(await GetContentFromUrl(byteContent, TrackUrl));
            return new EstafetaQuoteOutput();
        }

        private async Task<string> GetContentFromUrl<T>(T postObj, string url)
        {
            var httpClient = new HttpClient { BaseAddress = new Uri(url) };
            var obj = JsonConvert.SerializeObject(postObj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(obj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(TrackUrl, byteContent);
            result.EnsureSuccessStatusCode();
            return await result.Content.ReadAsStringAsync();
        }
    }
}