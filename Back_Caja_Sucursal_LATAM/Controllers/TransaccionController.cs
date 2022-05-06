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

        ///Consulta saldo a cejero
        [Route("ConsultaSaldoCajero/{noCajero}")]
        [HttpGet]
        public JsonResult GetConsultaSaldoCajero(Int64 noCajero)
        {
            var respuesta = new Response();

            if (noCajero.Equals(""))
            {
                return Json(new Response()
                {
                    Estatus = "Incorrecto",
                    Mensaje = "¡Datos vacios, llene los campos!"
                });

            }
            respuesta = _TransaccInterface.getConsultaSaldoCajero(noCajero);

            return Json(respuesta);
        }


        ///Consulta tarjeta
        [Route("ValidaTarjeta/{noTarjeta}")]
        [HttpGet]
        public JsonResult getValidaTarjeta(Int64 noTarjeta)
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
        [Route("ValidaNip/{noTarjeta}/{noNip}")]
        [HttpGet]
        public JsonResult GetValidaNip( Int64 noTarjeta, int noNip)
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
        [Route("ConsultaSaldoClienteCajero/{saldo}/{noTarjeta}/{noCajero}")]
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

        [Route("TransacciónRetiro/{saldo}/{noTarjeta}/{noCajero}")]
        [HttpPost]
        public JsonResult postTransaccionRetiro(Int64 saldo, Int64 noTarjeta, Int64 noCajero)
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
            respuesta = _TransaccInterface.insertTransaccion(saldo, noTarjeta, noCajero);

            return Json(respuesta);
        }
    }
}
