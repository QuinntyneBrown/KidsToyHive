// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Services;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Fakes;

public class FakeEmailDeliveryService : IEmailDeliveryService
{
    public Task Send(SendGridMessage message)
    {
        return Task.CompletedTask;
    }
}

