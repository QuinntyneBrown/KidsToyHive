using FluentValidation;
using KidsToyHive.Domain.DataAccess;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bookings;

 public class Validator : AbstractValidator<Request>
 {
     public Validator()
     {
     }
 }
 public class CancelBookingRequest : IRequest<CancelBookingResponse>
 {
 }
 public class CancelBookingResponse
 {
 }
 public class CancelBookingHandler : IRequestHandler<CancelBookingRequest, CancelBookingResponse>
 {
     private readonly IAppDbContext _context;
     public CancelBookingHandler(IAppDbContext context) => _context = context;
     public async Task<CancelBookingResponse> Handle(CancelBookingRequest request, CancellationToken cancellationToken)
     {
         await _context.SaveChangesAsync(cancellationToken);
         return new CancelBookingResponse() { };
     }
 }
