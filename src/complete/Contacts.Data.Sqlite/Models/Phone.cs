using System.Diagnostics.CodeAnalysis;

namespace Contacts.Data.Sqlite.Models
{
    [ExcludeFromCodeCoverage]
    public class Phone
    {
        public int PhoneId { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Extension { get; set; }
        public int? PhoneTypeId { get; set; }
        public PhoneType? PhoneType { get; set; }
        public Contact Contact { get; set; } = null!;
    }
}
