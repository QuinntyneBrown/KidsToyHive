// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Enums;
using KidsToyHive.Core.Models;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Services;

public class EmailService : IEmailService
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
    public async Task SendNewCustomer(Customer customer, User user)
    {
        var mailMessage = await _emailBuilder.Build(EmailTemplateName.NewCustomer, new Dictionary<string, string>());
        _emailDistributionService.SetDistributionList(ref mailMessage);
        mailMessage.Subject = "New Customer";
        mailMessage.From = new EmailAddress("notications@thekidstoyhive.com", "Kids Toy Hive");
        mailMessage.AddTo($"{customer.Email}", $"{customer.FirstName} {customer.LastName}");
        await _emailDeliveryService.Send(mailMessage);
    }
    public async Task SendNewDriver(Driver driver, User user)
    {
        var mailMessage = await _emailBuilder.Build(EmailTemplateName.NewDriver, new Dictionary<string, string>());
        mailMessage.Subject = "New Driver";
        _emailDistributionService.SetDistributionList(ref mailMessage);
        await _emailDeliveryService.Send(mailMessage);
    }
    public async Task SendBookingConfirmation(Booking booking)
    {
        var mailMessage = await _emailBuilder.Build(EmailTemplateName.BookingConfirmation, new Dictionary<string, string>() {
             { "{{ bookingDate }}",booking.Date.ToLongDateString() }
         });
        mailMessage.Subject = "Booking Confirmation";
        _emailDistributionService.SetDistributionList(ref mailMessage);
        mailMessage.From = new EmailAddress("notications@thekidstoyhive.com", "Kids Toy Hive");
        mailMessage.AddTo($"{booking.Customer.Email}", $"{booking.Customer.FirstName} {booking.Customer.LastName}");
        await _emailDeliveryService.Send(mailMessage);
    }
}

