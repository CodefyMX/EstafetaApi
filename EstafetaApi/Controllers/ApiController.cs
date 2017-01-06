using EstafetaApi.Experiments.Inputs;
using EstafetaApi.Experiments.Outputs;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EstafetaApi.Controllers
{
    public class ApiController : Controller
    {
        // GET: Api
        public async Task<JsonResult> Track(string codigo, int tipo = 2)
        {
            var result = await GetEstafetaResult(codigo, tipo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Track(string codigo, string brand, int tipo = 2)
        {
            switch (brand)
            {
                case "estafeta":
                    var result = await GetEstafetaResult(codigo, tipo);
                    return Json(result, JsonRequestBehavior.AllowGet);
                case "dhl":
                    //Todo: I think it will be easier, they are good fellas, they provided us this gift

                    //Track
                    //http://www.dhl.com.mx/shipmentTracking?AWB=[DaTrackingCode]&countryCode=mx&languageCode=es
                    //Quote
                    //http://dct.dhl.com/data/quotation/?dtbl=N&declVal=500&declValCur=MXN&wgtUom=kg&dimUom=cm&noPce=1&wgt0=0.1&w0=20&l0=10&h0=50&shpDate=2017-01-06&orgCtry=MX&orgCity=&orgSub=&orgZip=87140&dstCtry=MX&dstCity=&dstSub=&dstZip=87500
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task<EstafetaTrackOutput> GetEstafetaResult(string codigo, int tipo)
        {
            var estafetaApi = new Experiments.EstafetaApi();
            var result = await estafetaApi.Track(new EstafetaRequest()
            {
                wayBill = codigo,
                waybillType = tipo
            });
            return result;
        }
        public async Task<JsonResult> Quote(EstafetaQuoteInput input)
        {
            var estafetaApi = new Experiments.EstafetaApi();
            var result = await estafetaApi.Quote(input);
            result.From = input.CPOrigen;
            result.To = input.CPDestino;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}