using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.DashboardCards
{
    public class DashboardCardOptionsDtoValidator: AbstractValidator<DashboardCardOptionsDto>
    {
        public DashboardCardOptionsDtoValidator()
        {

        }
    }

    public class DashboardCardOptionsDto
    {        
    }
}
