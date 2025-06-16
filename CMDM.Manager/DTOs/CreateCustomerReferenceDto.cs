using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDM.Manager.DTOs
{
    public class CreateCustomerReferenceDto
    {
        public int ParentCustomerId { get; set; }
        public string CustCode { get; set; }
        public string Name { get; set; }
        public string? Add01 { get; set; }
        public string? Add02 { get; set; }
        public string? PostCode { get; set; }
        public string? Country { get; set; }
    }
}
