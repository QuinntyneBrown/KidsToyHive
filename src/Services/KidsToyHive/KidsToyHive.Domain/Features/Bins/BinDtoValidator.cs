using FluentValidation;

namespace KidsToyHive.Domain.Features.Bins;

public class BinDtoValidator : AbstractValidator<BinDto>
{
    public BinDtoValidator()
    {
        RuleFor(x => x.BinId).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}
