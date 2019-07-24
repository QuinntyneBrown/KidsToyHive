using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.SalesOrderDetails
{
    public class SalesOrderDetailDtoValidator: AbstractValidator<SalesOrderDetailDto>
    {
        public SalesOrderDetailDtoValidator()
        {
            RuleFor(x => x.SalesOrderDetailId).NotNull();
            RuleFor(x => x.Name).NotNull();
        }
    }

    public class SalesOrderDetailDto
    {        
        public Guid SalesOrderDetailId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }

    public static class SalesOrderDetailExtensions
    {        
        public static SalesOrderDetailDto ToDto(this SalesOrderDetail salesOrderDetail)
            => new SalesOrderDetailDto
            {
                SalesOrderDetailId = salesOrderDetail.SalesOrderDetailId,
                Name = salesOrderDetail.Name,
                Version = salesOrderDetail.Version
            };
    }
}
