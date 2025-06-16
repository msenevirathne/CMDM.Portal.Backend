using CMDM.Core.Models;
using CMDM.Manager;
using CMDM.Manager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CMDM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerMasterController : ControllerBase
    {
        private readonly ICustomerDataService _customerDataService;
        private readonly ICustomerMasterService _customerMasterService;
        private readonly ICustomerReferenceService _customerReferenceService;

        public CustomerMasterController(ICustomerDataService customerDataService, ICustomerMasterService customerMasterService,
            ICustomerReferenceService customerReferenceService)
        {
            _customerDataService = customerDataService;
            _customerMasterService = customerMasterService;
            _customerReferenceService = customerReferenceService;
        }

        [HttpGet("duplicates")]
        public IActionResult GetDuplicateCustomers()
        {
            try
            {
                var duplicateCustomerData = _customerDataService.GetDuplicateCustomers();
                return Ok(duplicateCustomerData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while merging customers.");
            }
        }

        [HttpPost("merge")]
        public async Task<IActionResult> MergeCustomers()
        {
            try
            {
                var duplicateCustomerData = _customerDataService.GetDuplicateCustomers();
                var groups = CustomerMatcher.GroupSimilarCustomers(duplicateCustomerData);

                foreach (var group in groups)
                {
                    var customer = group.FirstOrDefault();
                    if (customer == null)
                    {
                        continue; // Skip empty groups
                    }
                    // Map Customer to CreateCustomerMasterDto
                    var dtoModel = CustomerMapper.ToCreateDto(customer);
                    // Add data to the CustomerMasters table
                    var customerMaster = await _customerMasterService.CreateAsync(dtoModel);

                    if (customerMaster != null)
                    {
                        if (group.Count > 1)
                        {
                            var rest = group.Skip(1).ToList();
                            // Add data to the CustomerReferences table
                            foreach (var item in rest)
                            {
                                // Map Customer to CreateCustomerReferenceDto
                                var customerRefDtoModel = CustomerMapper.ToCreateDto(item, customerMaster.Id);
                                // Add data to the CustomerReference table
                                await _customerReferenceService.CreateAsync(customerRefDtoModel);
                            }
                        }
                        
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while merging customers.");
            }
        }

        [HttpGet("customerMasters")]
        public async Task<IActionResult> GetCustomerMasters()
        {
            try
            {
                var customerMasters = await _customerMasterService.GetAllAsync();
                return Ok(customerMasters);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the customer.");
            }
        }

        [HttpGet("customerReferences")]
        public async Task<IActionResult> GetCustomerReferences()
        {
            try
            {
                var customerReferences = await _customerReferenceService.GetAllAsync();
                return Ok(customerReferences);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the customer.");
            }
        }

        [HttpPut("{custNo}")]
        public async Task<IActionResult> UpdateCustomer(int custNo, [FromBody] Customer customer)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the customer.");
            }
        }
    }
}
