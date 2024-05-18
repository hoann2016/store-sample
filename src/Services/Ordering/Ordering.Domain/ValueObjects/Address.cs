﻿namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;
    public string? EmailAddress { get; } = default!;
    public string Country { get; } = default!;
    public string State { get; } = default!;
    public string AddressLine { get; } = default!;
    public string ZipCode { get; } = default!;

    protected Address()
    {
    }

    private Address(string firstName, string lastName, string emailAddress, string addressLine, string country,
        string zipCode, string state)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        Country = country;
        ZipCode = zipCode;
        State = state;
    }

    public static Address Of(string firstName, string lastName, string emailAddress, string addressLine,
        string country,
        string zipCode, string state)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);
        ArgumentException.ThrowIfNullOrWhiteSpace(country);
        ArgumentException.ThrowIfNullOrWhiteSpace(zipCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(state);

        return new Address(firstName, lastName, emailAddress, addressLine, country, zipCode, state);
    }
}