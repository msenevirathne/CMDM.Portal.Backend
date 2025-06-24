using CMDM.Core.Models;
using CMDM.Manager.DTOs;

namespace CMDM.Manager.Services.Interfaces
{
    public interface ICustomerReferenceService
    {
        Task<CustomerMaster> GetByIdAsync(int id);
        Task<List<CustomerReference>> GetAllAsync();
        Task<CustomerReference> CreateAsync(Customer customer, int customerMasterId);
        Task<CustomerReference> UpdateAsync(int id, CreateCustomerReferenceDto customerRefDto);
    }
}
