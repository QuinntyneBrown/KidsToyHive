using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.BookingDetails;

public class UpsertBookingDetailValidator : AbstractValidator<UpsertBookingDetailRequest>
{
    public UpsertBookingDetailValidator()
    {
        RuleFor(request => request.BookingDetail).NotNull();
        RuleFor(request => request.BookingDetail).SetValidator(new BookingDetailDtoValidator());
    }
}
public class UpsertBookingDetailRequest : IRequest<UpsertBookingDetailResponse>
{
    public BookingDetailDto BookingDetail { get; set; }
}
public class UpsertBookingDetailResponse
{
    public Guid BookingDetailId { get; set; }
}
public class UpsertBookingDetailHandler : IRequestHandler<UpsertBookingDetailRequest, UpsertBookingDetailResponse>
{
    private readonly IAppDbContext _context;
    public UpsertBookingDetailHandler(IAppDbContext context) => _context = context;
    public async Task<UpsertBookingDetailResponse> Handle(UpsertBookingDetailRequest request, CancellationToken cancellationToken)
    {
        var bookingDetail = await _context.BookingDetails.FindAsync(request.BookingDetail.BookingDetailId);
        if (bookingDetail == null)
        {
            bookingDetail = new BookingDetail();
            _context.BookingDetails.Add(bookingDetail);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertBookingDetailResponse() { BookingDetailId = bookingDetail.BookingDetailId };
    }
}
