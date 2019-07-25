using KidsToyHive.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KidsToyHive.Domain.Services
{
    public interface IEmailSender
    {
        void SendNewCustomerEmail(Customer customer, User user);
    }
}
