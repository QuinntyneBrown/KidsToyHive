// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Services;

public interface IEmailService
{
    Task SendNewCustomer(Customer customer, User user);
    Task SendNewDriver(Driver driver, User user);
    Task SendBookingConfirmation(Booking booking);
}

