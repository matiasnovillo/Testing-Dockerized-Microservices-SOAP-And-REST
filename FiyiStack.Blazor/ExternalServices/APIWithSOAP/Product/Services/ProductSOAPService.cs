using ProductServiceReference;
using System.ServiceModel;
using FiyiStack.Blazor.ExternalServices.APIWithSOAP.Product.Interfaces;

namespace FiyiStack.Blazor.ExternalServices.APIWithSOAP.Product.Services
{
    public class ProductSOAPService : IProductSOAPService
    {
        private readonly IConfiguration _configuration;

        public ProductSOAPService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ProductDTO> GetProductAsync(int productId)
        {
            var url = _configuration["SOAP:ProductServiceEndpoint"]!;

            var binding = new BasicHttpBinding();

            var endpoint = new EndpointAddress(url);

            var client = new ProductServiceClient(binding, endpoint);

            try
            {
                var respuesta = await client.GetProductAsync(productId);

                return respuesta;
            }
            finally
            {
                ((ICommunicationObject)client).Abort();
            }
        }
    }
}
