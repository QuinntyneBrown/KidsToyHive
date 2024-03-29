// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KidsToyHive.Core.Enums;
using KidsToyHive.Core.Identity;
using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using KidsToyHive.Core.Models.DomainEvents;
using KidsToyHive.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KidsToyHive.Core.Sagas;

public class DriverCreatedSagaHandler : INotificationHandler<DriverCreated>
{
    private readonly IKidsToyHiveDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEmailService _emailSender;
    public DriverCreatedSagaHandler(IKidsToyHiveDbContext context, IPasswordHasher passwordHasher, IEmailService emailSender)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _emailSender = emailSender;
    }
    public async Task Handle(DriverCreated notification, CancellationToken cancellationToken)
    {
        var driver = notification.Driver;
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == driver.Email);
        if (user == null)
        {
            user = new User
            {
                Username = driver.Email
            };
            user.Password = _passwordHasher.HashPassword(user.Salt, "P@ssw0rd");
            await _context.Users.AddAsync(user);
        }
        user.Profiles.Add(new Profile
        {
            Name = $"{driver.FirstName} {driver.LastName}",
            Type = ProfileType.Driver
        });
        await _context.SaveChangesAsync(cancellationToken);
        _emailSender.SendNewDriver(driver, user);
    }
}

