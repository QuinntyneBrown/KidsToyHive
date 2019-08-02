using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;

namespace KidsToyHive.Domain.Services
{
    public interface IEmailDeliveryService
    {
        void Send(SendGridMessage message);
    }
}
