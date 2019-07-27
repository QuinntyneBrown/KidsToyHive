using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.Bins
{
    public class BinDtoValidator: AbstractValidator<BinDto>
    {
        public BinDtoValidator()
        {
            RuleFor(x => x.BinId).NotNull();
            RuleFor(x => x.Name).NotNull();
        }
    }

    public class BinDto
    {        
        public Guid BinId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }

    public static class BinExtensions
    {        
        public static BinDto ToDto(this Bin bin)
            => new BinDto
            {
                BinId = bin.BinId,
                Name = bin.Name,
                Version = bin.Version
            };
    }
}
