// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KidsToyHive.Core.Enums;
using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

namespace KidsToyHive.Core.Services;

public class EmailBuilder : IEmailBuilder
{
    private readonly IKidsToyHiveDbContext _context;
    public EmailBuilder(IKidsToyHiveDbContext context)
    {
        _context = context;
    }
    public async Task<SendGridMessage> Build(EmailTemplateName template, Dictionary<string, string> items)
    {
        var sendGridMessage = new SendGridMessage();
        EmailTemplate emailTemplate;
        switch (template)
        {
            case EmailTemplateName.BookingConfirmation:
                emailTemplate = await _context.EmailTemplates.SingleAsync(x => x.Name == nameof(EmailTemplateName.BookingConfirmation));
                break;
            case EmailTemplateName.NewCustomer:
                emailTemplate = await _context.EmailTemplates.SingleAsync(x => x.Name == nameof(EmailTemplateName.NewCustomer));
                break;
            default:
                throw new NotSupportedException("");
        }
        string result = emailTemplate.Value;
        foreach (var item in items)
        {
            result = result.Replace(item.Key, item.Value);
        }
        sendGridMessage.HtmlContent = result;
        return sendGridMessage;
    }
}

