using FiyiStack.APIWithSOAP.Areas.Client.DTOs;
using FiyiStack.APIWithSOAP.Areas.Client.Interfaces;

namespace FiyiStack.APIWithSOAP.Areas.Client.Services
{
    public class ClientService : IClientService
    {
        public ClientDTO GetClient(int clientId)
        {
            //Aca se haria una consulta SQL mediante EF Core
            return new ClientDTO { ClientId = 1, Name="Matias Novillo"};
        }
    }
}
