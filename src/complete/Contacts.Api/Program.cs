using Contacts.Data;
using Contacts.Data.Sqlite;
using Contacts.Domain.Interfaces;
using Contacts.Logic;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ContactsDatabaseSqlite")
    ?? throw new InvalidOperationException(
        "Connection string 'ContactsDatabaseSqlite' was not found.");

builder.Services.AddDbContext<ContactContext>(options =>
    options.UseSqlite(
        connectionString,
        sqliteOptions => sqliteOptions.UseQuerySplittingBehavior(
            QuerySplittingBehavior.SplitQuery)));
builder.Services.AddScoped<IContactDataStore, SqliteDataStore>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactManager, ContactManager>();

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, _, _) =>
    {
        document.Info = new()
        {
            Title = "Contacts API",
            Version = "v1",
            Description = "Create, retrieve, search, and delete contacts."
        };

        return Task.CompletedTask;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
