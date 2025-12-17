// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Services;

public class EmailDeliveryService : IEmailDeliveryService
{
    private readonly SendGridClient _client;
    public EmailDeliveryService(IConfiguration configuration)
    {
        _client = new SendGridClient(configuration["SendGrid:ApiKey"]);
    }
    public async Task Send(SendGridMessage message)
    {
        try
        {
            await _client.SendEmailAsync(message);
        }
        catch (Exception e)
        {
            throw;
        }
    }
}

