using System.Web.Mvc;

namespace EstafetaApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var estafetaApi = new Experiments.EstafetaApi();
            //var result = await estafetaApi.Track(new EstafetaRequest()
            //{
            //    wayBill = "5085373039697720012697",
            //    waybillType = 1
            //});
            return View();
        }


        public ActionResult Cotizador()
        {
            return View();
        }
    }
}