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

        public Response getConsultaSaldo(Int64 saldo,Int64 noTarjeta, Int64 noCajero)
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

        public Response insertTransaccion(Int64 saldo, Int64 noTarjeta, int noCajero)
        {
            Response result = new Response();
            try
            {
                using (IDbConnection db = Connection)
                {
                    db.Open();
                    result = db.Query<Response>("sp_InsertTransaccionRetiro", new { Saldo = saldo, NoTarjeta = noTarjeta, NoCajero= noCajero }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    //if (result == null)
                    //{
                    //    return result = new Response()
                    //    {
                    //        Estatus = "Incorrecto",
                    //        Mensaje = "Registro no se pudo guardar correctamente"
                    //    };

                    //}
                    //if (result. == 0)
                    //{
                    //    return result = new Response()
                    //    {
                    //        Estatus = "Error al guardar",
                    //        Mensaje = "Registro " + prod.Name + " ya existe en el base de datos!"
                    //    };
                    //}
                    //if (result.Name != null && result.Creation != null)
                    //{
                    //    return result = new Response()
                    //    {
                    //        Name = "Correcto",
                    //        Mensaje = "Registro " + response.Name + " guardado correctamente"
                    //    };
                    //}


                    db.Close();

                    return result;
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
        public Response getConsultaSaldoCajero( Int64 noCajero)
        {
            Response result = new Response();
            try
            {
                using (IDbConnection db = Connection)
                {

                    result = db.Query<Response>("sp_getConsultaSaldoCajero", new { NoCajero= noCajero }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    if (result == null)
                    {
                        return result = new Response()
                        {
                            Estatus = "Incorrecto",
                            Mensaje = "¡Hubo algún Error en el Servidor!"
                        };

                    }
                    return result;

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
