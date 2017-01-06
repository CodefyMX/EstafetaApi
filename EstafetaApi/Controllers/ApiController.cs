using EstafetaApi.Experiments.Inputs;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EstafetaApi.Controllers
{
    public class ApiController : Controller
    {
        // GET: Api
        public async Task<JsonResult> Track(string codigo, int tipo = 2)
        {
            var estafetaApi = new Experiments.EstafetaApi();
            var result = await estafetaApi.Track(new EstafetaRequest()
            {
                wayBill = codigo,
                waybillType = tipo
            });
            return Json(result, JsonRequestBehavior.AllowGet);
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