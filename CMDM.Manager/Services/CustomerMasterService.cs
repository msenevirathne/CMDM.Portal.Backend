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

        public async Task<CustomerMaster> GetByIdAsync(int id)
        {
            return await _customerMasterRepository.GetByIdAsync(id);
        }

        public async Task<List<CustomerMaster>> GetAllAsync()
        {
            return await _customerMasterRepository.GetAllAsync();
        }

        public async Task<CustomerMaster> CreateAsync(Customer customer)
        {
            var entity = CustomerMapper.ToCustomerMaster(customer);
            var customerMaster = await _customerMasterRepository.AddAsync(entity);
            return customerMaster;
        }

        public Task<CustomerMaster> UpdateAsync(int id, CreateCustomerMasterDto customerMasterDto)
        {
            var entity = CustomerMapper.ToCustomerMaster(id, customerMasterDto);
            var customerMaster = _customerMasterRepository.UpdateAsync(entity);
            return customerMaster;
        }
    }
}
