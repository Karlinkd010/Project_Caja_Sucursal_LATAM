using Back_Caja_Sucursal_LATAM.Models;

namespace Back_Caja_Sucursal_LATAM.Interfaces
{
    public interface ITransaccionService
    {
        public Response getTarjeta(Int64 NoTarjeta);
        public Response getNip(Int64 noTarjeta, int noNip);
        public Response getConsultaSaldoCajero(Int64 noCajero);
        public Response getConsultaSaldo(Int64 saldo, Int64 noTarjeta, Int64 noCajero);
        public Response insertTransaccion(Int64 saldo, Int64 noTarjeta, Int64 noCajero);

    }
}
