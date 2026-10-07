using System.Diagnostics.CodeAnalysis;
using Contacts.Domain.Interfaces;
using Contacts.Domain.Models;

namespace Contacts.Data;

[ExcludeFromCodeCoverage]
public class ContactRepository(IContactDataStore contactDataStore) : IContactRepository
{
    public Contact? GetContact(int contactId)
    {
        return contactDataStore.GetContact(contactId);
    }

    public async Task<Contact?> GetContactAsync(int contactId)
    {
        return await contactDataStore.GetContactAsync(contactId);
    }

    public List<Contact> GetContacts()
    {
        return contactDataStore.GetContacts();
    }

    public async Task<List<Contact>> GetContactsAsync()
    {
        return await contactDataStore.GetContactsAsync();
    }

    public List<Contact> GetContacts(string firstName, string lastName)
    {
        return contactDataStore.GetContacts(firstName, lastName);
    }

    public async Task<List<Contact>> GetContactsAsync(string firstName, string lastName)
    {
        return await contactDataStore.GetContactsAsync(firstName, lastName);
    }

    public List<Contact> GetContactsFull()
    {
        return contactDataStore.GetContactsFull();
    }

    public async Task<List<Contact>> GetContactsFullAsync()
    {
        return await contactDataStore.GetContactsFullAsync();
    }

    public Contact? SaveContact(Contact contact)
    {
        return contactDataStore.SaveContact(contact);
    }

    public async Task<Contact?> SaveContactAsync(Contact contact)
    {
        return await contactDataStore.SaveContactAsync(contact);
    }

    public bool DeleteContact(int contactId)
    {
        return contactDataStore.DeleteContact(contactId);
    }

    public async Task<bool> DeleteContactAsync(int contactId)
    {
        return await contactDataStore.DeleteContactAsync(contactId);
    }

    public bool DeleteContact(Contact contact)
    {
        return contactDataStore.DeleteContact(contact);
    }

    public async Task<bool> DeleteContactAsync(Contact contact)
    {
        return await contactDataStore.DeleteContactAsync(contact);
    }

    public List<Phone> GetContactPhones(int contactId)
    {
        return contactDataStore.GetContactPhones(contactId);
    }

    public async Task<List<Phone>> GetContactPhonesAsync(int contactId)
    {
        return await contactDataStore.GetContactPhonesAsync(contactId);
    }

    public Phone? GetContactPhone(int contactId, int phoneId)
    {
        return contactDataStore.GetContactPhone(contactId, phoneId);
    }

    public async Task<Phone?> GetContactPhoneAsync(int contactId, int phoneId)
    {
        return await contactDataStore.GetContactPhoneAsync(contactId, phoneId);
    }

    public List<Address> GetContactAddresses(int contactId)
    {
        return contactDataStore.GetContactAddresses(contactId);
    }

    public async Task<List<Address>> GetContactAddressesAsync(int contactId)
    {
        return await contactDataStore.GetContactAddressesAsync(contactId);
    }

    public Address? GetContactAddress(int contactId, int addressId)
    {
        return contactDataStore.GetContactAddress(contactId, addressId);
    }

    public async Task<Address?> GetContactAddressAsync(int contactId, int addressId)
    {
        return await contactDataStore.GetContactAddressAsync(contactId, addressId);
    }
}