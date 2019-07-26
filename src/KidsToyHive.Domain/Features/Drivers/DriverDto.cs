using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.Drivers
{
    public class DriverDtoValidator: AbstractValidator<DriverDto>
    {
        public DriverDtoValidator()
        {
            RuleFor(x => x.DriverId).NotNull();
            RuleFor(x => x.Name).NotNull();
        }
    }

    public class DriverDto
    {        
        public Guid DriverId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }

    public static class DriverExtensions
    {        
        public static DriverDto ToDto(this Driver driver)
            => new DriverDto
            {
                DriverId = driver.DriverId,
                Name = driver.Name,
                Version = driver.Version
            };
    }
}
