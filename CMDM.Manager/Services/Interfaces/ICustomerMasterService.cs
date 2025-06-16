using CMDM.Core.Models;
using CMDM.Manager.DTOs;

namespace CMDM.Manager.Services.Interfaces
{
    public interface ICustomerMasterService
    {
        Task<List<CustomerMaster>> GetAllAsync();
        Task<CustomerMaster> CreateAsync(CreateCustomerMasterDto customerMasterDto);
        Task<CustomerMaster> UpdateAsync(CreateCustomerMasterDto customerMasterDto);
    }
}
