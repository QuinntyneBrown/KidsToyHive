using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.DashboardCards
{
    public class DashboardCardSettingsDtoValidator: AbstractValidator<DashboardCardSettingsDto>
    {
        public DashboardCardSettingsDtoValidator()
        {

        }
    }

    public class DashboardCardSettingsDto
    {        
    }
}
