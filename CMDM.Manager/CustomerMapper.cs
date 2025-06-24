using CMDM.Core.Models;
using CMDM.Manager.DTOs;

namespace CMDM.Manager
{
    public static class CustomerMapper
    {
        public static CustomerMaster ToCustomerMaster(Customer customer)
        {
            return new CustomerMaster
            {
                CustCode = customer.CustCode,
                Name = customer.Name,
                Add01 = customer.Add01,
                Add02 = customer.Add02,
                PostCode = customer.PostCode,
                Country = customer.Country
            };
        }

        public static CustomerReference ToCustomerReference(Customer customer, int customerMasterId)
        {
            return new CustomerReference
            {
                ParentCustomerId = customerMasterId,
                CustCode = customer.CustCode,
                Name = customer.Name,
                Add01 = customer.Add01,
                Add02 = customer.Add02,
                PostCode = customer.PostCode,
                Country = customer.Country   
            };
        }

        // Map dto classes to entity classes
        public static CustomerMaster ToCustomerMaster(int id, CreateCustomerMasterDto customerDto)
        {
            return new CustomerMaster
            {
                Id = id,
                CustCode = customerDto.CustCode,
                Name = customerDto.Name,
                Add01 = customerDto.Add01,
                Add02 = customerDto.Add02,
                PostCode = customerDto.PostCode,
                Country = customerDto.Country
            };
        }

        public static CustomerReference ToCustomerReference(int id, CreateCustomerReferenceDto customerDto)
        {
            return new CustomerReference
            {
                Id = id,
                ParentCustomerId = customerDto.ParentCustomerId,
                CustCode = customerDto.CustCode,
                Name = customerDto.Name,
                Add01 = customerDto.Add01,
                Add02 = customerDto.Add02,
                PostCode = customerDto.PostCode,
                Country = customerDto.Country
            };
        }

        // Map entity classes to dto classes
        public static CustomerReferenceDto ToCustomerReferenceDto(CustomerReference customerRef)
        {
            return new CustomerReferenceDto
            {
                Id = customerRef.Id,
                ParentCustomerId = customerRef.ParentCustomerId,
                CustCode = customerRef.CustCode,
                Name = customerRef.Name,
                Add01 = customerRef.Add01,
                Add02 = customerRef.Add02,
                PostCode = customerRef.PostCode,
                Country = customerRef.Country
            };
        }
    }
}
