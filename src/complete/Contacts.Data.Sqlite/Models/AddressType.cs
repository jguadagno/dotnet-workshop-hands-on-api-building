using System.Diagnostics.CodeAnalysis;

namespace Contacts.Data.Sqlite.Models
{
    [ExcludeFromCodeCoverage]
    public class AddressType
    {
        public int AddressTypeId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
