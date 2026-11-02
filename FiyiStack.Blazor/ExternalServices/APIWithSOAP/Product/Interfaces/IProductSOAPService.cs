using ProductServiceReference;

namespace FiyiStack.Blazor.ExternalServices.APIWithSOAP.Product.Interfaces
{
    public interface IProductSOAPService
    {
        public Task<ProductDTO> GetProductAsync(int productId);
    }
}
