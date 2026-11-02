using System.Runtime.Serialization;

namespace FiyiStack.APIWithSOAP.Areas.Client.DTOs
{
    [DataContract]
    public class ClientDTO
    {
        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        public string Name { get; set; } = string.Empty;
    }
}
