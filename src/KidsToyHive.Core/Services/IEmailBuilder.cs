// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Enums;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Services;

public interface IEmailBuilder
{
    Task<SendGridMessage> Build(EmailTemplateName template, Dictionary<string, string> items);
}

