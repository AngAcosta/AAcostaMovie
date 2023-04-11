using Microsoft.AspNetCore.Mvc;

namespace TheMovie.Controllers
{
    public class CineController : Controller
    {
        //GET: Cine
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Cine cine = new ML.Cine();
            ML.Result result = BL.Cine.GetAll();


            if (result.Correct)
            {
                cine.Cines = result.Objects;
                return View(cine);
            }
            else
            {
                return View(cine);
            }
        }

        [HttpGet]
        public ActionResult Form(int? IdCine)
        {
            ML.Result resultZona = BL.Zona.GetAll();

            ML.Cine cine = new ML.Cine();
            cine.Zona = new ML.Zona();

            if (resultZona.Correct)
            {
                cine.Zona.Zonas = resultZona.Objects;
            }
            if (IdCine == null)
            {
                //add //formulario vacio
                return View(cine);
            }
            else
            {
                //getById
                ML.Result result = BL.Cine.GetById(IdCine.Value);

                if (result.Correct)
                {
                    cine = (ML.Cine)result.Object; //unboxing
                    cine.Zona.Zonas = resultZona.Objects;

                    //update
                    return View(cine);
                }
                else
                {
                    ViewBag.Message = "Ocurrio al consultar la informacion del Cine";
                    return View("Modal");
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Cine cine)
        {
            ML.Result result = new ML.Result();

            if (cine.IdCine == 0)
            {
                //add
                result = BL.Cine.Add(cine);

                if (result.Correct)
                {
                    ViewBag.Message = "Se Completo del Registro";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al Insertar el Registro";
                }
                return View("Modal");
            }
            else
            {
                //update
                result = BL.Cine.Update(cine);

                if (result.Correct)
                {
                    ViewBag.Message = "Se Actualizo el Registro";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al Actualizar el Registro";
                    return View("Modal");
                }
                return View("Modal");
            }
        }

        [HttpGet]
        public ActionResult Delete(int IdCine)
        {
            ML.Result result = BL.Cine.Delete(IdCine);

            if (result.Correct)
            {
                ViewBag.message = "Se elimino el registro";
                return View("Modal");
            }
            else
            {
                ViewBag.message = "No se elimino el registro";
            }
            return View("Modal");
        }

    }
}