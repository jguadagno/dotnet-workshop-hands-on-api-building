using System.Diagnostics.CodeAnalysis;

namespace Contacts.Domain.Models;

[ExcludeFromCodeCoverage]
public class Phone
{
    public int PhoneId { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Extension { get; set; }
    public PhoneType? PhoneType { get; set; }
}