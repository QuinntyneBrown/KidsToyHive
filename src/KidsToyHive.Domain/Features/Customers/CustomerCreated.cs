using KidsToyHive.Core.Enums;
using KidsToyHive.Core.Identity;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using KidsToyHive.Domain.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Customers
{
    public class CustomerCreated
    {
        public class Notification: INotification
        {
            public Guid CustomerId { get; set; }
        }

        public class Handler : INotificationHandler<Notification>
        {
            private readonly IAppDbContext _context;
            private readonly IPasswordHasher _passwordHasher;
            private readonly IEmailSender _emailSender;

            public Handler(IAppDbContext context, IPasswordHasher passwordHasher, IEmailSender emailSender)
            {
                _context = context;
                _passwordHasher = passwordHasher;
                _emailSender = emailSender;
            }

            public async Task Handle(Notification notification, CancellationToken cancellationToken)
            {
                var customer = await _context.Customers.FindAsync(notification.CustomerId);

                var user = new User
                {
                    Username = customer.Email
                };

                user.Password = _passwordHasher.HashPassword(user.Salt,"P@ssw0rd");

                user.Profiles.Add(new Profile
                {
                    Name = $"{customer.FirstName} {customer.LastName}",
                    Type = ProfileType.Customer
                });

                await _context.Users.AddAsync(user);

                await _context.SaveChangesAsync(cancellationToken);

                _emailSender.SendNewCustomerEmail(customer, user);
            }
        }
    }
}
