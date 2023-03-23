// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Services;

public interface IEmailDeliveryService
{
    Task Send(SendGridMessage message);
}

