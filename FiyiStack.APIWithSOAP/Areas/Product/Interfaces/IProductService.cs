using CoreWCF;
using FiyiStack.APIWithSOAP.Areas.Product.DTOs;

namespace FiyiStack.APIWithSOAP.Areas.Product.Interfaces
{
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        public ProductDTO GetProduct(int productId);
    }
}
