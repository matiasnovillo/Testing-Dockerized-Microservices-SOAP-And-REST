using FiyiStack.APIWithSOAP.Areas.Product.DTOs;
using FiyiStack.APIWithSOAP.Areas.Product.Interfaces;

namespace FiyiStack.APIWithSOAP.Areas.Product.Services
{
    public class ProductService : IProductService
    {
        public ProductDTO GetProduct(int productId)
        {
            //Aca se haria una consulta SQL mediante EF Core
            return new ProductDTO { ProductID = 1, Name="Parlante JBL Extreme" };
        }
    }
}
