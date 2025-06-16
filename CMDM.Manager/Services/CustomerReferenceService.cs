using CMDM.Core.Models;
using CMDM.DAL.Repositories.Interfaces;
using CMDM.Manager.DTOs;
using CMDM.Manager.Services.Interfaces;

namespace CMDM.Manager.Services
{
    public class CustomerReferenceService : ICustomerReferenceService
    {
        private readonly ICustomerReferenceRepository _customerReferenceRepository;

        public CustomerReferenceService(ICustomerReferenceRepository customerReferenceRepository)
        {
            _customerReferenceRepository = customerReferenceRepository;
        }

        public async Task<List<CustomerReference>> GetAllAsync()
        {
            return await _customerReferenceRepository.GetAllAsync();
        }

        public async Task<CustomerReference> CreateAsync(CreateCustomerReferenceDto customerReferenceDto)
        {
            var entity = CustomerMapper.ToEntity(customerReferenceDto);
            var customerRef = await _customerReferenceRepository.AddAsync(entity);
            return customerRef;
        }

        public Task<CustomerReference> UpdateAsync(CreateCustomerReferenceDto customerMasterDto)
        {
            throw new NotImplementedException();
        }
    }
}
