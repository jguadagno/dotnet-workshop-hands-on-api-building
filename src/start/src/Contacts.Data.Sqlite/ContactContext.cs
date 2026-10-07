using System.Diagnostics.CodeAnalysis;
using Contacts.Data.Sqlite.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Data.Sqlite;

[ExcludeFromCodeCoverage]
public sealed class ContactContext(DbContextOptions<ContactContext> options) : DbContext(options)
{
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Phone> Phones => Set<Phone>();
    public DbSet<AddressType> AddressTypes => Set<AddressType>();
    public DbSet<PhoneType> PhoneTypes => Set<PhoneType>();
}