using CMDM.Core.Models;
using CMDM.Manager.DTOs;

namespace CMDM.Manager
{
    public static class CustomerMapper
    {
        // Map from Entity to DTO
        public static CreateCustomerMasterDto ToCreateDto(Customer customer)
        {
            return new CreateCustomerMasterDto
            {
                CustCode = customer.CustCode,
                Name = customer.Name,
                Add01 = customer.Add01,
                Add02 = customer.Add02,
                PostCode = customer.PostCode,
                Country = customer.Country
            };
        }

        public static CreateCustomerReferenceDto ToCreateDto(Customer customer, int id)
        {
            return new CreateCustomerReferenceDto
            {
                ParentCustomerId = id,
                CustCode = customer.CustCode,
                Name = customer.Name,
                Add01 = customer.Add01,
                Add02 = customer.Add02,
                PostCode = customer.PostCode,
                Country = customer.Country
            };
        }

        // Map from DTO to Entity
        public static CustomerMaster ToEntity(CreateCustomerMasterDto dto)
        {
            return new CustomerMaster
            {
                CustCode = dto.CustCode,
                Name = dto.Name,
                Add01 = dto.Add01,
                Add02 = dto.Add02,
                PostCode = dto.PostCode,
                Country = dto.Country
            };
        }

        public static CustomerReference ToEntity(CreateCustomerReferenceDto dto)
        {
            return new CustomerReference
            {
                ParentCustomerId = dto.ParentCustomerId,
                CustCode = dto.CustCode,
                Name = dto.Name,
                Add01 = dto.Add01,
                Add02 = dto.Add02,
                PostCode = dto.PostCode,
                Country = dto.Country
            };
        }
    }
}
