// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Domain.Services;
using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Services;

public class EmailDeliveryServiceTests
{
    [Fact]
    public async Task ShouldDeliverEmail()
    {
        //var configuration = new ConfigurationBuilder()
        //    .AddInMemoryCollection(new List<KeyValuePair<string, string>>() {
        //    })
        //    .Build();
        //var emailDeliveryService = new EmailDeliveryService(configuration);
        //SendGridMessage message = new SendGridMessage
        //{
        //    Subject = "Booking Confirmation",
        //    From = new EmailAddress("notifications@thekidstoyhive.com", "The Kids Toy Hive Team"),
        //    HtmlContent = "<h1>Test</h1>"
        //};
        //message.AddTo("quinntynebrown@gmail.com", "Quinntyne Brown");
        //await emailDeliveryService.Send(message);
    }
}

