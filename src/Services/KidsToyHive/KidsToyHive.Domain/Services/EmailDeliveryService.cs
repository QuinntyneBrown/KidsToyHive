using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Services;

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
