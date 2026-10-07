using System.Diagnostics.CodeAnalysis;
using Contacts.Data.Sqlite.Models;
using Contacts.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Contact = Contacts.Domain.Models.Contact;

namespace Contacts.Data.Sqlite
{
    [ExcludeFromCodeCoverage]
    public class SqliteDataStore(ContactContext contactContext) : IContactDataStore
    {
        public Contact? GetContact(int contactId)
        {
            var dbContact = contactContext.Contacts
                .FirstOrDefault(c => c.ContactId == contactId);
            return dbContact.ToDomain();
        }

        public async Task<Contact?> GetContactAsync(int contactId)
        {
            var dbContact = await contactContext.Contacts
                .FirstOrDefaultAsync(c => c.ContactId == contactId);
            return dbContact.ToDomain();
        }

        public List<Contact> GetContacts()
        {
            var contacts = contactContext.Contacts.ToList();
            return contacts.ToDomain();
        }

        public async Task<List<Contact>> GetContactsAsync()
        {
            var contacts = await contactContext.Contacts.ToListAsync();
            return contacts.ToDomain();
        }

        public List<Contact> GetContacts(string firstName, string lastName)
        {
            ValidationForGetContacts(firstName, lastName);

            var dbContact = contactContext.Contacts
                .Where(contact => contact.LastName == lastName && contact.FirstName == firstName).ToList();
            return dbContact.ToDomain();
        }

        public async Task<List<Contact>> GetContactsAsync(string firstName, string lastName)
        {
            ValidationForGetContacts(firstName, lastName);

            var dbContact = await contactContext.Contacts
                .Where(contact => contact.LastName == lastName && contact.FirstName == firstName).ToListAsync();
            return dbContact.ToDomain();
        }

        public List<Contact> GetContactsFull()
        {
            var contacts = contactContext.Contacts
                .Include(c => c.Addresses)
                .Include(c => c.Phones)
                .Include(c => c.Addresses).ThenInclude(a => a.AddressType)
                .Include(c => c.Phones).ThenInclude(p => p.PhoneType)
                .ToList();
            return contacts.ToDomain();
        }

        public async Task<List<Contact>> GetContactsFullAsync()
        {
            var contacts = await contactContext.Contacts
                .Include(c => c.Addresses)
                .Include(c => c.Phones)
                .Include(c => c.Addresses).ThenInclude(a => a.AddressType)
                .Include(c => c.Phones).ThenInclude(p => p.PhoneType)
                .ToListAsync();
            return contacts.ToDomain();
        }

        private static void ValidationForGetContacts(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentNullException(nameof(lastName), "LastName is a required field");
            }

            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentNullException(nameof(firstName), "FirstName is a required field");
            }
        }

        public Contact? SaveContact(Contact contact)
        {
            var dbContact = contact.ToEntity();
            if (dbContact.ContactId == 0)
            {
                contactContext.Contacts.Add(dbContact);
            }
            else
            {
                contactContext.Contacts.Update(dbContact);
            }

            var wasSaved = contactContext.SaveChanges() != 0;
            if (wasSaved)
            {
                return ApplyGeneratedIds(contact, dbContact);
            }
            return null;
        }

        public async Task<Contact?> SaveContactAsync(Contact contact)
        {
            var dbContact = contact.ToEntity();

            if (dbContact.ContactId == 0)
            {
                await contactContext.Contacts.AddAsync(dbContact);
            }
            else
            {
                contactContext.Contacts.Update(dbContact);
            }

            var wasSaved = await contactContext.SaveChangesAsync() != 0;
            if (wasSaved)
            {
                return ApplyGeneratedIds(contact, dbContact);
            }
            return null;
        }

        private static Contact ApplyGeneratedIds(Contact contact, Models.Contact dbContact)
        {
            contact.ContactId = dbContact.ContactId;

            for (var index = 0; index < contact.Addresses.Count; index++)
            {
                contact.Addresses[index].AddressId = dbContact.Addresses[index].AddressId;
            }

            for (var index = 0; index < contact.Phones.Count; index++)
            {
                contact.Phones[index].PhoneId = dbContact.Phones[index].PhoneId;
            }

            return contact;
        }

        public bool DeleteContact(int contactId)
        {
            var contact = contactContext.Contacts
                .Include(c => c.Addresses)
                .Include(c => c.Phones)
                .FirstOrDefault(c => c.ContactId == contactId);

            if (contact == null)
            {
                return false;
            }

            contactContext.Contacts.Remove(contact);
            foreach (var contactAddress in contact.Addresses)
            {
                contactContext.Addresses.Remove(contactAddress);
            }

            foreach (var contactPhone in contact.Phones)
            {
                contactContext.Phones.Remove(contactPhone);
            }

            return contactContext.SaveChanges() != 0;
        }

        public async Task<bool> DeleteContactAsync(int contactId)
        {
            var contact = await contactContext.Contacts
                    .Include(c => c.Addresses)
                    .Include(c => c.Phones)
                    .FirstOrDefaultAsync(c => c.ContactId == contactId);

            if (contact == null)
            {
                return false;
            }

            contactContext.Contacts.Remove(contact);
            foreach (var contactAddress in contact.Addresses)
            {
                contactContext.Addresses.Remove(contactAddress);
            }

            foreach (var contactPhone in contact.Phones)
            {
                contactContext.Phones.Remove(contactPhone);
            }

            return await contactContext.SaveChangesAsync() != 0;
        }

        public bool DeleteContact(Contact contact)
        {
            return ValidateDeleteContact(contact) && DeleteContact(contact.ContactId);
        }

        public async Task<bool> DeleteContactAsync(Contact contact)
        {
            return ValidateDeleteContact(contact) && await DeleteContactAsync(contact.ContactId);
        }

        private static bool ValidateDeleteContact(Contact contact)
        {
            return contact != null;
        }

        public List<Domain.Models.Phone> GetContactPhones(int contactId)
        {
            var dbPhones = contactContext.Phones
                .Where(p => p.Contact.ContactId == contactId).ToList();

            return dbPhones.ToDomain();
        }

        public async Task<List<Domain.Models.Phone>> GetContactPhonesAsync(int contactId)
        {
            var dbPhones = await contactContext.Phones
                .Where(p => p.Contact.ContactId == contactId).ToListAsync();

            return dbPhones.ToDomain();
        }

        public Domain.Models.Phone? GetContactPhone(int contactId, int phoneId)
        {
            var dbPhone = contactContext.Phones
                .FirstOrDefault(p => p.Contact.ContactId == contactId && p.PhoneId == phoneId);
            return dbPhone.ToDomain();
        }

        public async Task<Domain.Models.Phone?> GetContactPhoneAsync(int contactId, int phoneId)
        {
            var dbPhone = await contactContext.Phones
                .FirstOrDefaultAsync(p => p.Contact.ContactId == contactId && p.PhoneId == phoneId);
            return dbPhone.ToDomain();
        }

        public List<Domain.Models.Address> GetContactAddresses(int contactId)
        {
            var dbAddresses = contactContext.Addresses
                .Where(a => a.Contact.ContactId == contactId).ToList();

            return dbAddresses.ToDomain();
        }

        public async Task<List<Domain.Models.Address>> GetContactAddressesAsync(int contactId)
        {
            var dbAddresses = await contactContext.Addresses
                .Where(a => a.Contact.ContactId == contactId).ToListAsync();

            return dbAddresses.ToDomain();
        }

        public Domain.Models.Address? GetContactAddress(int contactId, int addressId)
        {
            var dbAddress = contactContext.Addresses
                .FirstOrDefault(a => a.Contact.ContactId == contactId && a.AddressId == addressId);
            return dbAddress.ToDomain();
        }

        public async Task<Domain.Models.Address?> GetContactAddressAsync(int contactId, int addressId)
        {
            var dbAddress = await contactContext.Addresses
                .FirstOrDefaultAsync(a => a.Contact.ContactId == contactId && a.AddressId == addressId);
            return dbAddress.ToDomain();
        }
    }
}