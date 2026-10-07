using System.Diagnostics.CodeAnalysis;

namespace Contacts.Domain.Models;

[ExcludeFromCodeCoverage]
public class Address
{
    public int AddressId { get; set; }
    public string StreetAddress { get; set; } = string.Empty;
    public string? SecondaryAddress { get; set; }
    public string? Unit { get; set; }
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? PostalCode { get; set; }
    public AddressType? AddressType { get; set; }
}