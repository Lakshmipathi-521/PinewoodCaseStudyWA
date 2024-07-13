using Business.CRMSystem;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models.Customers;

namespace Controllers.CRMSystem;

[ApiController]
[Produces("application/json")]
[Route("api/customers")]
public class CustomersController : ControllerBase, ICustomersController
{
    private const int DEFAULT_NO_OF_RECORDS = 25;
    private ICustomersBusinessComponent customersBusinessComponent;

    public CustomersController(ICustomersBusinessComponent customersBusinessComponent)
    {
        if (customersBusinessComponent == default(ICustomersBusinessComponent))
            throw new ArgumentException("Invalid Customers Business Component Specified!");

        this.customersBusinessComponent = customersBusinessComponent;
    }

    [HttpPost]
    public async Task<ActionResult<Customer>> AddNewCustomerRecord(Customer newCustomerRecord)
    {
        var validation = newCustomerRecord != null;

        if (!validation)
            return BadRequest();

        var status = await Task.Run<bool>(() =>
        {
#pragma warning disable CS8604 // Possible null reference argument.
            return this.customersBusinessComponent.AddNewCustomer(newCustomerRecord);
#pragma warning restore CS8604 // Possible null reference argument.
        });

        if (status)
            return Ok(newCustomerRecord);

        return BadRequest();
    }

    [HttpDelete]
    public async Task<ActionResult<Customer>> DeleteCustomerRecord(Customer existingCustomerRecord)
    {
        var validation = existingCustomerRecord != null;

        if (!validation)
            return BadRequest();

        var status = await Task.Run<bool>(() =>
        {
#pragma warning disable CS8604 // Possible null reference argument.
            return customersBusinessComponent.DeleteCustomer(existingCustomerRecord);
#pragma warning restore CS8604 // Possible null reference argument.
        });

        if (status)
            return Ok(existingCustomerRecord);

        return BadRequest();
    }

    public void Dispose()
    {
        customersBusinessComponent?.Dispose();
    }

    [HttpGet]
    [Route("details/{customerId}")]
    public async Task<ActionResult<Customer>> GetCustomerDetails(int customerId)
    {
        var validation = customerId != default(int);

        if (!validation)
            return BadRequest();

        var filteredRecord = await Task.Run<Customer>(() =>
        {
            return customersBusinessComponent.GetCustomerDetails(customerId);
        });

        if (filteredRecord != null)
        {
            return Ok(filteredRecord);
        }

        return NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers(
        [FromQuery] int noOfRecords = 0
    )
    {
        var filteredCustomers = await Task.Run<IEnumerable<Customer>>(() =>
        {
            return customersBusinessComponent.GetCustomers().Take(noOfRecords);
        });

        if (filteredCustomers != null)
            return Ok(filteredCustomers);

        return NotFound();
    }

    [HttpGet]
    [Route("search/{searchString}")]
    public async Task<ActionResult<IEnumerable<Customer>>> SearchCustomerRecords(
        string? searchString
    )
    {
        var validation = !string.IsNullOrEmpty(searchString);

        if (!validation)
            return BadRequest();

        var filteredCustomers = await Task.Run<IEnumerable<Customer>>(() =>
        {
            return customersBusinessComponent.GetCustomers(searchString);
        });

        if (filteredCustomers != null)
            return Ok(filteredCustomers);

        return NotFound();
    }

    [HttpPut]
    public async Task<ActionResult<Customer>> UpdateExistingCustomerRecord(
        Customer existingCustomerRecord
    )
    {
        var validation = existingCustomerRecord != null;

        if (!validation)
            return BadRequest();

        var updatedCustomerRecord = await Task.Run<Customer>(() =>
        {
#pragma warning disable CS8604 // Possible null reference argument.
            return customersBusinessComponent.UpdateCustomer(existingCustomerRecord);
#pragma warning restore CS8604 // Possible null reference argument.
        });

        if (updatedCustomerRecord != null)
            return Ok(updatedCustomerRecord);

        return BadRequest();
    }
}
