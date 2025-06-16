using CMDM.Core.Models;

namespace CMDM.Manager.Services.Interfaces
{
    public interface ICustomerDataService
    {
        List<Customer> GetDuplicateCustomers();
    }
}
