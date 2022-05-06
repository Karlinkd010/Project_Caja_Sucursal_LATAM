using Back_Caja_Sucursal_LATAM.Models;
using Back_Caja_Sucursal_LATAM.Interfaces;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Back_Caja_Sucursal_LATAM.Services
{
    public class TransaccionServices : ITransaccionService
    {
        private readonly IConfiguration _configuration;
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }

        public TransaccionServices(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("AppConnection");
            ProviderName = "System.Data.SqlClient";
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }

        }

        //Valida tarjeta
        public Response getTarjeta(Int64 NoTarjeta)
        {
            Response respuesta = new Response();
            try
            {
                using (IDbConnection db = Connection)
                {
                    respuesta = db.Query<Response>("sp_getTarjeta", new { NoTarjeta = NoTarjeta }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (respuesta == null)
                    {
                        return respuesta = new Response()
                        {
                            Estatus = "Incorrecto",
                            Mensaje = "¡Hubo algún Error en el Servidor!"
                        };

                    }

                    return respuesta;
                }
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Estatus = "Error",
                    Mensaje = ex.Message
                };
            }  
        }

        //Valida Nip
        public Response getNip(Int64 noTarjeta , int noNip)
        {
            Response respuesta = new Response();
            try
            {
                using (IDbConnection db = Connection)
                {

                    respuesta = db.Query<Response>("sp_getNip", new { NoNip = noNip, NoTarjeta = noTarjeta }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    if (respuesta == null)
                    {
                        return respuesta = new Response()
                        {
                            Estatus = "Incorrecto",
                            Mensaje = "¡Hubo algún Error en el Servidor!"
                        };

                    }
                    return respuesta;

                }
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Estatus = "Error",
                    Mensaje = ex.Message
                };
            }
        }

        public Response getConsultaSaldo(Int64 saldo,Int64 noTarjeta, int noCajero)
        {
            Response respuesta = new Response();
            try
            {
                using (IDbConnection db = Connection)
                {

                    respuesta = db.Query<Response>("sp_getValidaSaldo", new { Saldo = saldo, NoTarjeta = noTarjeta, NoCajero= noCajero }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    if (respuesta == null)
                    {
                        return respuesta = new Response()
                        {
                            Estatus = "Incorrecto",
                            Mensaje = "¡Hubo algún Error en el Servidor!"
                        };

                    }
                    return respuesta;

                }
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Estatus = "Error",
                    Mensaje = ex.Message
                };
            }
        }



    }
    
}
