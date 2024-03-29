// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Api;
using KidsToyHive.Core.Enums;
using KidsToyHive.Infrastructure.Data;
using KidsToyHive.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Services;

public class EmailBuilderTests
{
    [Fact]
    public async Task ShouldBuildTemplate()
    {
        var options = new DbContextOptionsBuilder<KidsToyHiveDbContext>()
            .UseInMemoryDatabase($"{nameof(EmailBuilderTests)}:{nameof(ShouldBuildTemplate)}")
            .Options;
        var mediator = new Mock<IMediator>().Object;
        using (var context = new AppDbContext(options, mediator))
        {
            SeedData.Seed(context, ConfigurationHelper.Seed);
            IEmailBuilder emailBuilder = new EmailBuilder(context);
            var email = await emailBuilder.Build(EmailTemplateName.BookingConfirmation, new Dictionary<string, string>() {
                 { "{{ bookingDate }}","Today" }
             });
            Assert.NotNull(email);
        }
    }
    [Fact]
    public async Task ShouldBuildTemplateWithHTMLBody()
    {
        //IEmailBuilder emailBuilder = new EmailBuilder(null);
        //var email = await emailBuilder.Build(EmailTemplateName.NewCustomer, new Dictionary<string, string>());
    }
}

