using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.Models;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace KidsToyHive.Domain.Services
{
    public class EmailService: IEmailService
    {
        private readonly IEmailBuilder _emailBuilder;
        private readonly IEmailDistributionService _emailDistributionService;
        private readonly IEmailDeliveryService _emailDeliveryService;

        public EmailService(
            IEmailBuilder emailBuilder, 
            IEmailDistributionService emailDistributionService,
            IEmailDeliveryService emailDeliveryService)
        {
            _emailBuilder = emailBuilder;
            _emailDistributionService = emailDistributionService;
            _emailDeliveryService = emailDeliveryService;
        }
        public void SendNewCustomerEmail(Customer customer, User user)
        {
            var mailMessage = _emailBuilder.Build(EmailTemplateName.NewCustomer, user);
            _emailDistributionService.SetDistributionList(ref mailMessage);
            _emailDeliveryService.Send(mailMessage);

        }

        public void SendNewDriverEmail(Driver driver, User user)
        {

        }
    }
}
