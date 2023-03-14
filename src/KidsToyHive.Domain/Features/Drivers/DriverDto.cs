using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.Drivers;

public class DriverDtoValidator : AbstractValidator<DriverDto>
{
    public DriverDtoValidator()
    {
        RuleFor(x => x.DriverId).NotNull();
        RuleFor(x => x.FirstName).NotNull();
    }
}
public class DriverDto
{
    public Guid DriverId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int Version { get; set; }
}
public static class DriverExtensions
{
    public static DriverDto ToDto(this Driver driver)
        => new DriverDto
        {
            DriverId = driver.DriverId,
            FirstName = driver.FirstName,
            Version = driver.Version
        };
}
