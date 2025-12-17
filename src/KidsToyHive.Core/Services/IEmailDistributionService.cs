// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;

namespace KidsToyHive.Core.Services;

public interface IEmailDistributionService
{
    void SetDistributionList(ref SendGridMessage message);
}

