using KidsToyHive.Core.Extensions;
using KidsToyHive.Domain.Common;
using MediatR;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Geolocation;

 public class GetLongLatCoordinatesRequest : AuthenticatedRequest<Response>
 {
     public string Address { get; set; }
 }
 public class GetLongLatCoordinatesResponse
 {
     public double Longitude { get; set; }
     public double Latitude { get; set; }
 }
 public class GetLongLatCoordinatesHandler : IRequestHandler<GetLongLatCoordinatesRequest, GetLongLatCoordinatesResponse>
 {
     protected readonly HttpClient _client;
     public GetLongLatCoordinatesHandler(HttpClient client)
     {
         _client = client;
     }
     public async Task<GetLongLatCoordinatesResponse> Handle(GetLongLatCoordinatesRequest request, CancellationToken token)
     {
         var googleResponse = await _client.GetAsync<GoogleEncodeResponse>($"http://maps.googleapis.com/maps/api/geocode/json?address={request.Address}&sensor=false");
         return new GetLongLatCoordinatesResponse()
         {
             Latitude = googleResponse.Latitude,
             Longitude = googleResponse.Longitude
         };
     }

 }
