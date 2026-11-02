using System.Runtime.Serialization;

namespace FiyiStack.APIWithSOAP.Areas.Product.DTOs
{
    [DataContract]
    public class ProductDTO
    {
        [DataMember]
        public int ProductID { get; set; }

        [DataMember]
        public string Name { get; set; } = string.Empty;
    }
}
