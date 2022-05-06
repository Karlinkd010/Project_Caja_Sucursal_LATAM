using Back_Caja_Sucursal_LATAM.Interfaces;
using Back_Caja_Sucursal_LATAM.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back_Caja_Sucursal_LATAM.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class TransaccionController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ITransaccionService _TransaccInterface;

        public TransaccionController(IConfiguration configuration, ITransaccionService transaccService)
        {
            _configuration = configuration;
            _TransaccInterface = transaccService;
        }
        ///listado de productos
        //[HttpGet]
        //public JsonResult GetProducts()
        //{
        //    var products = new List<Product>();
        //    products = _productInterface.getProducts().ToList();

        //    if (products == null)
        //    {
        //        return Json(new Response()
        //        {
        //            Name = "Incorrecto",
        //            Mensaje = "Datos vacios"
        //        });
        //    }

        //    return Json(products);
        //}


        ///lista producto detalle
        [Route("{noTarjeta}")]
        [HttpGet]
        public JsonResult GetTarjeta(Int64 noTarjeta)
        {
            var respuesta = new Response();

            if (noTarjeta.Equals(""))
            {
                return Json(new Response()
                {
                    Estatus = "Incorrecto",
                    Mensaje = "¡Datos vacios, llene los campos!"
                });

            }
            respuesta = _TransaccInterface.getTarjeta(noTarjeta);

            return Json(respuesta);
        }


        ///lista producto detalle
        [Route("ConsultaNip/{noTarjeta}/{noNip}")]
        [HttpGet]
        public JsonResult GetNip( Int64 noTarjeta, int noNip)
        {
            var respuesta = new Response();

            if (noTarjeta == 0 || noNip == 0)
            {
                return Json(new Response()
                {
                    Estatus = "Incorrecto",
                    Mensaje = "¡Datos vacios!"
                });

            }
            respuesta = _TransaccInterface.getNip(noTarjeta,noNip);

            return Json(respuesta);
        }


        ///lista producto detalle
        [Route("ConsultaSaldo/{saldo}/{noTarjeta}/{noCajero}")]
        [HttpGet]
        public JsonResult GetConsultaSaldo(Int64 saldo, Int64 noTarjeta, Int64 noCajero)
        {
            var respuesta = new Response();

            if (noTarjeta == 0 || saldo == 0 || noCajero == 0)
            {
                return Json(new Response()
                {
                    Estatus = "Incorrecto",
                    Mensaje = "¡Datos vacios!"
                });

            }
            respuesta = _TransaccInterface.getConsultaSaldo(saldo,noTarjeta, noCajero);

            return Json(respuesta);
        }

        [Route("{saldo}/{noTarjeta}/{noCajero}")]
        [HttpPost]
        public JsonResult insertTransaccion(Int64 saldo, Int64 noTarjeta, Int64 noCajero)
        {
            var respuesta = new Response();

            if (noTarjeta == 0 || saldo == 0 || noCajero == 0)
            {
                return Json(new Response()
                {
                    Estatus = "Incorrecto",
                    Mensaje = "¡Datos vacios!"
                });

            }
            respuesta = _TransaccInterface.getConsultaSaldo(saldo, noTarjeta, noCajero);

            return Json(respuesta);
        }
        ///inserta producto
        //[HttpPost]
        //public JsonResult insertProducts(Product product)
        //{
        //    var Response = new Response();

        //    if (product.Name.Equals(""))
        //    {
        //        return Json(new Response()
        //        {
        //            Name = "Incorrecto",
        //            Mensaje = "Datos vacios"
        //        });

        //    }

        //    Response = _productInterface.insertProduct( product);

        //    return Json(Response);


        //}
        /////actualiza producto
        //[Route("{pid}")]
        //[HttpPut]
        //public JsonResult updatetProducts(Product product)
        //{
        //    var Response = new Response();

        //    if (product.Id == null || product.Name.Equals("") )
        //    {
        //        return Json(new Response()
        //        {
        //            Name = "Incorrecto",
        //            Mensaje = "Datos vacios"
        //        });

        //    }

        //    Response = _productInterface.updateProduct(product);

        //    return Json(Response);

        //}
        /////elimina producto
        //[Route("{pid}")]
        //[HttpDelete]
        //public JsonResult deleteProducts(int pid)
        //{
        //    var Response = new Response();

        //    if (pid.Equals(""))
        //    {
        //        return Json(new Response()
        //        {
        //            Name = "Incorrecto",
        //            Mensaje = "Datos vacios"
        //        });

        //    }

        //    Response = _productInterface.deleteProduct(pid);

        //    return Json(Response);

        //}
    }
}
