using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.Dashboards
{
    public class DashboardSettingsDtoValidator: AbstractValidator<DashboardSettingsDto>
    {
        public DashboardSettingsDtoValidator()
        {

        }
    }

    public class DashboardSettingsDto
    {        

    }
}
