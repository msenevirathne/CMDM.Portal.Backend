using CMDM.Core.Models;
using CMDM.Manager.Services.Interfaces;

namespace CMDM.Manager.Services
{
    public class CustomerDataService : ICustomerDataService
    {
        public List<Customer> GetDuplicateCustomers()
        {
            var data = new List<Customer>
            {
                new Customer { CustCode = "BRW001", Name = "British water PVT LTD.", Add01 = "Talbot Road", Add02 = "London", PostCode = "LN1 1QH", Country = "UK" },
                new Customer { CustCode = "BRWT", Name = "British water", Add01 = "Talbot Road, London", Add02 = "", PostCode = "LN1 1QH", Country = "" },
                new Customer { CustCode = "BW123", Name = "BRITISH WATER", Add01 = "London", Add02 = "", PostCode = "LN11QH", Country = "United Kingdom" },
                new Customer { CustCode = "RE099", Name = "Row Electronics ", Add01 = "11", Add02 = "Redwell road", PostCode = "NN8 1KL", Country = "GB" },
                new Customer { CustCode = "ROWE", Name = "Row Electronic ltd", Add01 = "Redwell road", Add02 = "", PostCode = "NN8 1KL", Country = "United Kingdom" },
                new Customer { CustCode = "RE002", Name = "Row Electronics", Add01 = "11, Redwell road", Add02 = "Wellingborough", PostCode = "", Country = "UK" }
            };
            return data;
        }
    }
}
