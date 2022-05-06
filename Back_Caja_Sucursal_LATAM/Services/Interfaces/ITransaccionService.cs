using Back_Caja_Sucursal_LATAM.Models;

namespace Back_Caja_Sucursal_LATAM.Interfaces
{
    public interface ITransaccionService
    {
        //public List<Product> getProducts();
        //public Product getProduct(int Id);
        //public Response insertProduct(Product prod);
        //public Response updateProduct(Product prod);
        //public Response deleteProduct(int id);

        public Response getTarjeta(Int64 NoTarjeta);
        public Response getNip(Int64 noTarjeta, int noNip);
        public Response getConsultaSaldo(Int64 saldo, Int64 noTarjeta, int noCajero);


    }
}
