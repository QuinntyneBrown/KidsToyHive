using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.BookingDetails;

public class UpsertBookingDetail
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.BookingDetail).NotNull();
            RuleFor(request => request.BookingDetail).SetValidator(new BookingDetailDtoValidator());
        }
    }
    public class Request : IRequest<Response>
    {
        public BookingDetailDto BookingDetail { get; set; }
    }
    public class Response
    {
        public Guid BookingDetailId { get; set; }
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var bookingDetail = await _context.BookingDetails.FindAsync(request.BookingDetail.BookingDetailId);
            if (bookingDetail == null)
            {
                bookingDetail = new BookingDetail();
                _context.BookingDetails.Add(bookingDetail);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return new Response() { BookingDetailId = bookingDetail.BookingDetailId };
        }
    }
}
