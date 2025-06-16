using CMDM.Core.Models;
using CMDM.DAL.Repositories.Interfaces;
using CMDM.Manager.DTOs;
using CMDM.Manager.Services.Interfaces;

namespace CMDM.Manager.Services
{
    public class CustomerMasterService : ICustomerMasterService
    {
        private readonly ICustomerMasterRepository _customerMasterRepository;

        public CustomerMasterService(ICustomerMasterRepository customerMasterRepository)
        {
            _customerMasterRepository = customerMasterRepository;
        }

        public async Task<List<CustomerMaster>> GetAllAsync()
        {
            return await _customerMasterRepository.GetAllAsync();
        }

        public async Task<CustomerMaster> CreateAsync(CreateCustomerMasterDto customerMasterDto)
        {
            var entity = CustomerMapper.ToEntity(customerMasterDto);
            var customerMaster = await _customerMasterRepository.AddAsync(entity);
            return customerMaster;
        }

        public Task<CustomerMaster> UpdateAsync(CreateCustomerMasterDto customerMasterDto)
        {
            throw new NotImplementedException();
        }
    }
}
