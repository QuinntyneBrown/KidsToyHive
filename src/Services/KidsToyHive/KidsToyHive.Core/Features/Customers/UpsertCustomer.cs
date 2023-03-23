// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using FluentValidation;
using KidsToyHive.Core.Enums;
using KidsToyHive.Core.Exceptions;
using KidsToyHive.Core.Identity;
using KidsToyHive.Core.Common;
using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using KidsToyHive.Core.Models.DomainEvents;
using KidsToyHive.Core.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Customers;

public class UpsertCustomerValidator : AbstractValidator<UpsertCustomerRequest>
{
    public UpsertCustomerValidator()
    {
        RuleFor(request => request.Customer).NotNull();
        RuleFor(request => request.Customer).SetValidator(new CustomerDtoValidator());
    }
}
[AllowAnonymous]
public class UpsertCustomerRequest : Command<UpsertCustomerResponse>
{
    public CustomerDto Customer { get; set; }
    public bool AcceptedTermsAndConditions { get; set; }
    public override IEnumerable<string> SideEffects => new string[] { "Customer" };
}
public class UpsertCustomerResponse
{
    public Guid CustomerId { get; set; }
    public int Version { get; set; }
    public string AccessToken { get; set; }
}
public class UpsertCustomerHandler : IRequestHandler<UpsertCustomerRequest, UpsertCustomerResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ISecurityTokenFactory _securityTokenFactory;
    public UpsertCustomerHandler(
        IKidsToyHiveDbContext context,
        IPasswordHasher passwordHasher,
        ISecurityTokenFactory securityTokenFactory)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _securityTokenFactory = securityTokenFactory;
    }
    public async Task<UpsertCustomerResponse> Handle(UpsertCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FindAsync(request.Customer.CustomerId);
        if (customer == null)
        {
            if (await _context.Customers.AnyAsync(x => x.Email == request.Customer.Email))
                throw new CustomerExistsWithEmailException();
            if (!request.AcceptedTermsAndConditions)
                throw new CustomerFailedToAcceptTermsAndConditionsException();
            customer = new Customer();

            customer.CustomerTermsAndConditions.Add(new CustomerTermsAndConditions
            {
                Accepted = DateTime.UtcNow
            });
            customer.RaiseDomainEvent(new CustomerCreated(customer));
            _context.Customers.Add(customer);
        }

        customer.FirstName = request.Customer.FirstName;
        customer.LastName = request.Customer.LastName;
        customer.PhoneNumber = request.Customer.PhoneNumber;
        customer.Email = request.Customer.Email;
        if (request.Customer.Address != null)
            customer.Address = new Address(
                request.Customer.Address.Street,
                request.Customer.Address.City,
                request.Customer.Address.Province,
                request.Customer.Address.PostalCode);
        customer.CustomerLocations.Clear();
        foreach (var customerLocationDto in request.Customer.CustomerLocations)
        {
            var customerLocation = await _context.CustomerLocations.FindAsync(customerLocationDto.CustomerLocationId);
            if (customerLocation == null)
                customerLocation = new CustomerLocation();
            customerLocation.Location = await _context.Locations.FindAsync(customerLocationDto.LocationId);
            if (customerLocation.Location == null)
                customerLocation.Location = new Location();
            customerLocation.Name = customerLocationDto.Name;
            customerLocation.Location = new Location();
            customerLocation.LocationId = customerLocationDto.LocationId;
            customerLocation.Location.Type = customerLocationDto.Location.Type;
            customerLocation.Location.Adddress = new Address(
                customerLocationDto.Location.Address.Street,
                customerLocationDto.Location.Address.City,
                customerLocationDto.Location.Address.Province,
                customerLocationDto.Location.Address.PostalCode);
            customer.CustomerLocations.Add(customerLocation);
        }
        await _context.SaveChangesAsync(cancellationToken);
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == customer.Email);
        if (user == null)
        {
            user = new User
            {
                Username = customer.Email
            };
            //TODO: Make random passwords
            user.Password = _passwordHasher.HashPassword(user.Salt, "P@ssw0rd");
            await _context.Users.AddAsync(user);
        }
        user.Profiles.Add(new Profile
        {
            Name = $"{customer.FirstName} {customer.LastName}",
            Type = ProfileType.Customer
        });
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertCustomerResponse()
        {
            CustomerId = customer.CustomerId,
            Version = customer.Version,
            AccessToken = _securityTokenFactory.Create(user.Username, new List<Claim>() {
                  new Claim("UserId", $"{user.UserId}"),
                  new Claim("PartitionKey", $"{user.TenantId}"),
                  new Claim($"{nameof(customer.CustomerId)}",$"{customer.CustomerId}"),
                  new Claim(ClaimTypes.Role, nameof(ProfileType.Customer)),
                  new Claim("CurrentUserName",$"{user.Username}")
              })
        };
    }
}

