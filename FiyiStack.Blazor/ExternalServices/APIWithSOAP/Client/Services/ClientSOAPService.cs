using System.ServiceModel;
using ClientServiceReference;
using FiyiStack.Blazor.ExternalServices.APIWithSOAP.Client.Interfaces;

namespace FiyiStack.Blazor.ExternalServices.APIWithSOAP.Client.Services
{
    public class ClientSOAPService : IClientSOAPService
    {
        private readonly IConfiguration _configuration;

        public ClientSOAPService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ClientDTO> GetClientAsync(int clientId)
        {
            var url = _configuration["SOAP:ClientServiceEndpoint"]!;

            var binding = new BasicHttpBinding();

            var endpoint = new EndpointAddress(url);

            var client = new ClientServiceClient(binding, endpoint);

            try
            {
                var respuesta = await client.GetClientAsync(clientId);

                return respuesta;
            }
            finally
            {
                ((ICommunicationObject)client).Abort();
            }
        }
    }
}
