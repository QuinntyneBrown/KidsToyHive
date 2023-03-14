using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.BookingDetails;

 public class GetBookingDetailsRequest : IRequest<GetBookingDetailsResponse> { }
 public class GetBookingDetailsResponse
 {
     public IEnumerable<BookingDetailDto> BookingDetails { get; set; }
 }
 public class GetBookingDetailsHandler : IRequestHandler<GetBookingDetailsRequest, GetBookingDetailsResponse>
 {
     private readonly IAppDbContext _context;
     public GetBookingDetailsHandler(IAppDbContext context) => _context = context;
     public async Task<GetBookingDetailsResponse> Handle(GetBookingDetailsRequest request, CancellationToken cancellationToken)
         => new GetBookingDetailsResponse()
         {
             BookingDetails = await _context.BookingDetails.Select(x => x.ToDto()).ToArrayAsync()
         };
 }
