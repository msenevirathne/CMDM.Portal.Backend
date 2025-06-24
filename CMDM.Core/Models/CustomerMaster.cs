using CMDM.Core.Common;

namespace CMDM.Core.Models
{
    public class CustomerMaster : BaseEntity
    {
        public string CustCode { get; set; }
        public string Name { get; set; }
        public string? Add01 { get; set; }
        public string? Add02 { get; set; }
        public string? PostCode { get; set; }
        public string? Country { get; set; }
        public List<CustomerReference> CustomerReferences { get; set; } = new List<CustomerReference>();
    }
}
