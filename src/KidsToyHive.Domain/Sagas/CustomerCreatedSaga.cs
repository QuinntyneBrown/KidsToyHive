using KidsToyHive.Core.Enums;
using KidsToyHive.Core.Identity;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using KidsToyHive.Domain.Models.DomainEvents;
using KidsToyHive.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Sagas
{
    public class CustomerCreatedSaga
    {
        public class Handler : INotificationHandler<CustomerCreated>
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

            public async Task Handle(CustomerCreated notification, CancellationToken cancellationToken)
            {
                var customer = notification.Customer;

                var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == customer.Email);

                if (user == null)
                {
                    user = new User
                    {
                        Username = customer.Email
                    };

                    user.Password = _passwordHasher.HashPassword(user.Salt, "P@ssw0rd");

                    await _context.Users.AddAsync(user);
                }

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
