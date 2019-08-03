using KidsToyHive.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Services
{
    public interface IEmailService
    {
        Task SendNewCustomerEmail(Customer customer, User user);
        Task SendNewDriverEmail(Driver driver, User user);
    }
}
