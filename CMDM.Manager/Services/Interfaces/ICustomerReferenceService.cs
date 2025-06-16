using CMDM.Core.Models;
using CMDM.Manager.DTOs;

namespace CMDM.Manager.Services.Interfaces
{
    public interface ICustomerReferenceService
    {
        Task<List<CustomerReference>> GetAllAsync();
        Task<CustomerReference> CreateAsync(CreateCustomerReferenceDto customerRefDto);
        Task<CustomerReference> UpdateAsync(CreateCustomerReferenceDto customerRefDto);
    }
}
