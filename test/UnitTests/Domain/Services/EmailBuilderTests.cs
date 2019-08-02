using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using KidsToyHive.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests.Domain.Services
{
    public class EmailBuilderTests
    {        
        [Fact]
        public void ShouldBuildTemplate()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"{nameof(EmailBuilderTests)}:{nameof(ShouldBuildTemplate)}")
                .Options;

            var mediator = new Mock<IMediator>().Object;

            using (var context = new AppDbContext(options, mediator)) {

                IEmailBuilder emailBuilder = new EmailBuilder(context);

                var user = new User();

                var email = emailBuilder.Build(EmailTemplateName.NewCustomer, user);

                Assert.NotNull(email);
            }


        }

        [Fact]
        public void ShouldBuildTemplateWithHTMLBody()
        {
            IEmailBuilder emailBuilder = new EmailBuilder(null);
            var user = new User();

            var email = emailBuilder.Build(EmailTemplateName.NewCustomer, user);

            Assert.NotNull(email.HtmlContent);
        }
    }
}
