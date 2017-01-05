using EstafetaApi.Experiments.Inputs;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EstafetaApi.Controllers
{
    public class ApiController : Controller
    {
        // GET: Api
        public async Task<JsonResult> Rastreo(string codigo, int tipo = 2)
        {
            var estafetaApi = new Experiments.EstafetaApi();
            var result = await estafetaApi.Track(new EstafetaRequest()
            {
                wayBill = codigo,
                waybillType = tipo
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Quote()
        {
            var estafetaApi = new Experiments.EstafetaApi();
            var result = await estafetaApi.Quote(new EstafetaQuoteInput()
            {
                cTipoEnvio = "sobre",
                Tipo = "sobre",
                CPOrigen = 87140,
                CPDestino = 87500
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}