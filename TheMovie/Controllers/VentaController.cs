using Microsoft.AspNetCore.Mvc;

namespace TheMovie.Controllers
{
    public class VentaController : Controller
    {
        public ActionResult Venta()
        {
            ML.Cine cine = new ML.Cine();
            ML.Result resultCine = BL.Cine.GetAll();
            ML.Result result = BL.Cine.Porcentaje();

            if (result.Correct)
            {
                cine = (ML.Cine)result.Object; //unboxing
                cine.Cines = resultCine.Objects;
                return View(cine);
            }
            else
            {
                return View(cine);
            }
        }
    }
}