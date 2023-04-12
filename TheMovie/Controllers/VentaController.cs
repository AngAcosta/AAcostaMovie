using Microsoft.AspNetCore.Mvc;

namespace TheMovie.Controllers
{
    public class VentaController : Controller
    {
        public ActionResult Venta()
        {
            ML.Cine cine = new ML.Cine();
            ML.Result result = BL.Cine.Porcentaje();

            if (result.Correct)
            {
                cine = (ML.Cine)result.Object; //unboxing
                return View(cine);
            }
            else
            {
                return View(cine);
            }
        }
    }
}