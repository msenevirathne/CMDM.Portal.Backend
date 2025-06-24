using CMDM.Core.Models;
using CMDM.Manager.DTOs;

namespace CMDM.Manager.Services.Interfaces
{
    public interface ICustomerMasterService
    {
        Task<CustomerMaster> GetByIdAsync(int id);
        Task<List<CustomerMaster>> GetAllAsync();
        Task<CustomerMaster> CreateAsync(Customer customer);
        Task<CustomerMaster> UpdateAsync(int id, CreateCustomerMasterDto customerMasterDto);
    }
}
