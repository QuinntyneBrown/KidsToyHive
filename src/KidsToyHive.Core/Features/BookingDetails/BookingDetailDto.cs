// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;
using KidsToyHive.Core.Features.Products;

namespace KidsToyHive.Core.Features.BookingDetails;

public class BookingDetailDtoValidator : AbstractValidator<BookingDetailDto>
{
    public BookingDetailDtoValidator()
    {
        RuleFor(x => x.BookingDetailId).NotNull();
    }
}
public class BookingDetailDto
{
    public Guid BookingDetailId { get; set; }
    public Guid ProductId { get; set; }
    public ProductDto Product { get; set; }
    public int Quantity { get; set; }
    public int Version { get; set; }
}
public static class BookingDetailExtensions
{
    public static BookingDetailDto ToDto(this BookingDetail bookingDetail)
        => new BookingDetailDto
        {
            BookingDetailId = bookingDetail.BookingDetailId,
            Version = bookingDetail.Version,
            Product = bookingDetail.Product.ToDto()
        };
}

