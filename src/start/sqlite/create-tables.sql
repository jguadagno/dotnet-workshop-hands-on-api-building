PRAGMA foreign_keys = ON;

CREATE TABLE AddressTypes
(
    AddressTypeId INTEGER PRIMARY KEY AUTOINCREMENT,
    Type          TEXT,
    Description   TEXT
);

CREATE TABLE Contacts
(
    ContactId    INTEGER PRIMARY KEY AUTOINCREMENT,
    FirstName    TEXT,
    MiddleName   TEXT,
    LastName     TEXT,
    EmailAddress TEXT,
    ImageUrl     TEXT,
    Birthday     TEXT NOT NULL,
    Anniversary  TEXT
);

CREATE TABLE PhoneTypes
(
    PhoneTypeId INTEGER PRIMARY KEY AUTOINCREMENT,
    Type        TEXT,
    Description TEXT
);

CREATE TABLE Addresses
(
    AddressId        INTEGER PRIMARY KEY AUTOINCREMENT,
    StreetAddress    TEXT,
    SecondaryAddress TEXT,
    Unit             TEXT,
    City             TEXT,
    State            TEXT,
    Country          TEXT,
    PostalCode       TEXT,
    AddressTypeId    INTEGER,
    ContactId        INTEGER,
    FOREIGN KEY (AddressTypeId) REFERENCES AddressTypes (AddressTypeId),
    FOREIGN KEY (ContactId) REFERENCES Contacts (ContactId)
);

CREATE TABLE Phones
(
    PhoneId     INTEGER PRIMARY KEY AUTOINCREMENT,
    PhoneNumber TEXT,
    Extension   TEXT,
    PhoneTypeId INTEGER,
    ContactId   INTEGER,
    FOREIGN KEY (PhoneTypeId) REFERENCES PhoneTypes (PhoneTypeId),
    FOREIGN KEY (ContactId) REFERENCES Contacts (ContactId)
);

CREATE INDEX IX_Addresses_AddressTypeId ON Addresses (AddressTypeId);
CREATE INDEX IX_Addresses_ContactId ON Addresses (ContactId);
CREATE INDEX IX_Phones_PhoneTypeId ON Phones (PhoneTypeId);
CREATE INDEX IX_Phones_ContactId ON Phones (ContactId);
