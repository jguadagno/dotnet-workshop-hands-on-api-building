using System.ComponentModel.DataAnnotations;
using Contacts.Domain.Interfaces;
using Contacts.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController(IContactManager contactManager) : ControllerBase
{
    /// <summary>
    /// Gets all contacts.
    /// </summary>
    /// <returns>All available contacts.</returns>
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<Contact>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<Contact>>> GetContacts()
    {
        var contacts = await contactManager.GetContactsAsync();
        return Ok(contacts);
    }

    /// <summary>
    /// Gets a contact by identifier.
    /// </summary>
    /// <param name="id">The contact identifier.</param>
    /// <returns>The matching contact.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType<Contact>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Contact>> GetContact(int id)
    {
        var contact = await contactManager.GetContactAsync(id);
        return contact is null ? NotFound() : Ok(contact);
    }

    /// <summary>
    /// Creates a contact.
    /// </summary>
    /// <param name="contact">The contact to create.</param>
    /// <returns>The created contact and its location.</returns>
    [HttpPost]
    [ProducesResponseType<Contact>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Contact>> SaveContact(Contact contact)
    {
        var savedContact = await contactManager.SaveContactAsync(contact);

        return savedContact is null
            ? Problem(
                title: "Unable to create contact",
                detail: "The contact data store did not save the contact.")
            : CreatedAtAction(
                nameof(GetContact),
                new { id = savedContact.ContactId },
                savedContact);
    }

    /// <summary>
    /// Deletes a contact.
    /// </summary>
    /// <param name="id">The contact identifier.</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteContact(int id)
    {
        var wasDeleted = await contactManager.DeleteContactAsync(id);
        return wasDeleted ? NoContent() : NotFound();
    }

    /// <summary>
    /// Searches for contacts by first and last name.
    /// </summary>
    /// <param name="firstName">The first name to match.</param>
    /// <param name="lastName">The last name to match.</param>
    /// <returns>Contacts that match both names.</returns>
    [HttpGet("search")]
    [ProducesResponseType<IReadOnlyList<Contact>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<Contact>>> SearchContacts(
        [FromQuery, Required] string firstName,
        [FromQuery, Required] string lastName)
    {
        var contacts = await contactManager.GetContactsAsync(firstName, lastName);
        return Ok(contacts);
    }

    /// <summary>
    /// Gets all phone numbers for a contact.
    /// </summary>
    /// <param name="id">The contact identifier.</param>
    /// <returns>The contact's phone numbers.</returns>
    [HttpGet("{id:int}/phones")]
    [ProducesResponseType<IReadOnlyList<Phone>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<Phone>>> GetContactPhones(int id)
    {
        var phones = await contactManager.GetContactPhonesAsync(id);
        return Ok(phones);
    }

    /// <summary>
    /// Gets a specific phone number for a contact.
    /// </summary>
    /// <param name="id">The contact identifier.</param>
    /// <param name="phoneId">The phone identifier.</param>
    /// <returns>The matching phone number.</returns>
    [HttpGet("{id:int}/phones/{phoneId:int}")]
    [ProducesResponseType<Phone>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Phone>> GetContactPhone(int id, int phoneId)
    {
        var phone = await contactManager.GetContactPhoneAsync(id, phoneId);
        return phone is null ? NotFound() : Ok(phone);
    }

    /// <summary>
    /// Gets all addresses for a contact.
    /// </summary>
    /// <param name="id">The contact identifier.</param>
    /// <returns>The contact's addresses.</returns>
    [HttpGet("{id:int}/addresses")]
    [ProducesResponseType<IReadOnlyList<Address>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<Address>>> GetContactAddresses(int id)
    {
        var addresses = await contactManager.GetContactAddressesAsync(id);
        return Ok(addresses);
    }

    /// <summary>
    /// Gets a specific address for a contact.
    /// </summary>
    /// <param name="id">The contact identifier.</param>
    /// <param name="addressId">The address identifier.</param>
    /// <returns>The matching address.</returns>
    [HttpGet("{id:int}/addresses/{addressId:int}")]
    [ProducesResponseType<Address>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Address>> GetContactAddress(int id, int addressId)
    {
        var address = await contactManager.GetContactAddressAsync(id, addressId);
        return address is null ? NotFound() : Ok(address);
    }
}
