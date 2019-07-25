using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.BookingDetails
{
    public class BookingDetailDtoValidator: AbstractValidator<BookingDetailDto>
    {
        public BookingDetailDtoValidator()
        {
            RuleFor(x => x.BookingDetailId).NotNull();
        }
    }

    public class BookingDetailDto
    {        
        public Guid BookingDetailId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }

    public static class BookingDetailExtensions
    {        
        public static BookingDetailDto ToDto(this BookingDetail bookingDetail)
            => new BookingDetailDto
            {
                BookingDetailId = bookingDetail.BookingDetailId,
                Version = bookingDetail.Version
            };
    }
}
