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

        public Task<CustomerMaster> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CustomerReference>> GetAllAsync()
        {
            try
            {
                return await _customerReferenceRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the customer references.", ex);
            }
        }

        public async Task<CustomerReference> CreateAsync(Customer customer, int customerMasterId)
        {
            try
            {
                var entity = CustomerMapper.ToCustomerReference(customer, customerMasterId);
                var customerRef = await _customerReferenceRepository.AddAsync(entity);
                return customerRef;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the customer reference.", ex);
            }
        }

        public Task<CustomerReference> UpdateAsync(int id, CreateCustomerReferenceDto customerReferenceDto)
        {
            try
            {
                var entity = CustomerMapper.ToCustomerReference(id, customerReferenceDto);
                var customerRef = _customerReferenceRepository.UpdateAsync(entity);
                return customerRef;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the customer reference.", ex);
            }
        }
    }
}
