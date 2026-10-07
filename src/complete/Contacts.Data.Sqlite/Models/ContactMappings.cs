using DomainAddress = Contacts.Domain.Models.Address;
using DomainAddressType = Contacts.Domain.Models.AddressType;
using DomainContact = Contacts.Domain.Models.Contact;
using DomainPhone = Contacts.Domain.Models.Phone;
using DomainPhoneType = Contacts.Domain.Models.PhoneType;

namespace Contacts.Data.Sqlite.Models;

internal static class ContactMappings
{
    public static DomainContact? ToDomain(this Contact? contact) =>
        contact is null
            ? null
            : new DomainContact
            {
                ContactId = contact.ContactId,
                FirstName = contact.FirstName,
                MiddleName = contact.MiddleName,
                LastName = contact.LastName,
                EmailAddress = contact.EmailAddress,
                Birthday = contact.Birthday,
                Anniversary = contact.Anniversary,
                ImageUrl = contact.ImageUrl,
                Addresses = contact.Addresses.ToDomain(),
                Phones = contact.Phones.ToDomain()
            };

    public static List<DomainContact> ToDomain(this IEnumerable<Contact> contacts) =>
        contacts.Select(contact => contact.ToDomain()!).ToList();

    public static Contact ToEntity(this DomainContact contact)
    {
        var entity = new Contact
        {
            ContactId = contact.ContactId,
            FirstName = contact.FirstName ?? string.Empty,
            MiddleName = contact.MiddleName,
            LastName = contact.LastName ?? string.Empty,
            EmailAddress = contact.EmailAddress ?? string.Empty,
            Birthday = contact.Birthday,
            Anniversary = contact.Anniversary,
            ImageUrl = contact.ImageUrl
        };

        entity.Addresses = contact.Addresses
            .Select(address => address.ToEntity(entity))
            .ToList();
        entity.Phones = contact.Phones
            .Select(phone => phone.ToEntity(entity))
            .ToList();

        return entity;
    }

    public static DomainAddress? ToDomain(this Address? address) =>
        address is null
            ? null
            : new DomainAddress
            {
                AddressId = address.AddressId,
                StreetAddress = address.StreetAddress,
                SecondaryAddress = address.SecondaryAddress,
                Unit = address.Unit,
                City = address.City,
                State = address.State,
                Country = address.Country,
                PostalCode = address.PostalCode,
                AddressType = address.AddressType?.ToDomain()
            };

    public static List<DomainAddress> ToDomain(this IEnumerable<Address> addresses) =>
        addresses.Select(address => address.ToDomain()!).ToList();

    private static Address ToEntity(this DomainAddress address, Contact contact) =>
        new()
        {
            AddressId = address.AddressId,
            StreetAddress = address.StreetAddress,
            SecondaryAddress = address.SecondaryAddress,
            Unit = address.Unit,
            City = address.City,
            State = address.State,
            Country = address.Country,
            PostalCode = address.PostalCode,
            AddressTypeId = address.AddressType?.AddressTypeId,
            Contact = contact
        };

    public static DomainPhone? ToDomain(this Phone? phone) =>
        phone is null
            ? null
            : new DomainPhone
            {
                PhoneId = phone.PhoneId,
                PhoneNumber = phone.PhoneNumber,
                Extension = phone.Extension,
                PhoneType = phone.PhoneType?.ToDomain()
            };

    public static List<DomainPhone> ToDomain(this IEnumerable<Phone> phones) =>
        phones.Select(phone => phone.ToDomain()!).ToList();

    private static Phone ToEntity(this DomainPhone phone, Contact contact) =>
        new()
        {
            PhoneId = phone.PhoneId,
            PhoneNumber = phone.PhoneNumber,
            Extension = phone.Extension,
            PhoneTypeId = phone.PhoneType?.PhoneTypeId,
            Contact = contact
        };

    private static DomainAddressType ToDomain(this AddressType addressType) =>
        new()
        {
            AddressTypeId = addressType.AddressTypeId,
            Type = addressType.Type,
            Description = addressType.Description
        };

    private static DomainPhoneType ToDomain(this PhoneType phoneType) =>
        new()
        {
            PhoneTypeId = phoneType.PhoneTypeId,
            Type = phoneType.Type,
            Description = phoneType.Description
        };
}
