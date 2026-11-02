using ClientServiceReference;

namespace FiyiStack.Blazor.ExternalServices.APIWithSOAP.Client.Interfaces
{
    public interface IClientSOAPService
    {
        public Task<ClientDTO> GetClientAsync(int clientId);
    }
}
