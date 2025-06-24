using CMDM.Core.Models;
using CMDM.Manager;
using CMDM.Manager.DTOs;
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

        [HttpGet("checktables")]
        public async Task<IActionResult> CheckTablesExist()
        {
            try
            {
                var customerMasters = await _customerMasterService.GetAllAsync();
                var customerReferences = await _customerReferenceService.GetAllAsync();
                bool anyExist = customerMasters.Any() || customerReferences.Any();
                return Ok(new { anyExist = anyExist });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while checking customer tables.");
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
                    // Add data to the CustomerMasters table
                    var customerMaster = await _customerMasterService.CreateAsync(customer);

                    if (customerMaster != null)
                    {
                        if (group.Count > 1)
                        {
                            var rest = group.Skip(1).ToList();
                            // Add data to the CustomerReferences table
                            foreach (var customerRef in rest)
                            {
                                // Add data to the CustomerReference table
                                await _customerReferenceService.CreateAsync(customerRef, customerMaster.Id);
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

        [HttpPut("updatemaster/{custNo}")]
        public async Task<IActionResult> UpdateCustomerMaster(int custNo, [FromBody] CreateCustomerMasterDto customerDto)
        {
            try
            {
                var result = await _customerMasterService.UpdateAsync(custNo, customerDto);
                return Ok(new
                {
                    success = true,
                    message = "Customer master updated successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the customer master.");
            }
        }

        [HttpPut("updatereference/{custNo}")]
        public async Task<IActionResult> UpdateCustomerReference(int custNo, [FromBody] CreateCustomerReferenceDto customerDto)
        {
            try
            {
                var customerMaster = await _customerMasterService.GetByIdAsync(customerDto.ParentCustomerId);
                if (customerMaster == null)
                {
                    return BadRequest("Invalid ParentCustomerId: No such CustomerMaster exists.");
                }
                var result = await _customerReferenceService.UpdateAsync(custNo, customerDto);
                return Ok(new
                {
                    success = true,
                    message = "Customer reference updated successfully.",
                    data = CustomerMapper.ToCustomerReferenceDto(result)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the customer reference.");
            }
        }
    }
}
