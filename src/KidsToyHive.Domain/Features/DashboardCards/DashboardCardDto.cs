using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.DashboardCards
{
    public class DashboardCardDtoValidator: AbstractValidator<DashboardCardDto>
    {
        public DashboardCardDtoValidator()
        {
            RuleFor(x => x.DashboardCardId).NotNull();
            RuleFor(x => x.Name).NotNull();
        }
    }

    public class DashboardCardDto
    {        
        public Guid DashboardCardId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }

    public static class DashboardCardExtensions
    {        
        public static DashboardCardDto ToDto(this DashboardCard dashboardCard)
            => new DashboardCardDto
            {
                DashboardCardId = dashboardCard.DashboardCardId,
                Name = dashboardCard.Name,
                Version = dashboardCard.Version
            };
    }
}
