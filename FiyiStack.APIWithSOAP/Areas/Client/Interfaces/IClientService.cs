using CoreWCF;
using FiyiStack.APIWithSOAP.Areas.Client.DTOs;

namespace FiyiStack.APIWithSOAP.Areas.Client.Interfaces
{
    [ServiceContract]
    public interface IClientService
    {
        [OperationContract]
        public ClientDTO GetClient(int clientId);
    }
}
