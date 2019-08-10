using MediatR;
using System.Threading.Tasks;
using System.Net.Http;
using KidsToyHive.Core.Extensions;
using System.Threading;
using KidsToyHive.Domain.Common;

namespace KidsToyHive.Domain.Features.Geolocation
{
    public class GetLongLatCoordinates
    {
        public class Request : AuthenticatedRequest<Response>
        {
            public string Address { get; set; }
        }

        public class Response
        {
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            protected readonly HttpClient _client;

            public Handler(HttpClient client)
            {
                _client = client;
            }

            public async Task<Response> Handle(Request request, CancellationToken token)
            {                
                var googleResponse = await _client.GetAsync<GoogleEncodeResponse>($"http://maps.googleapis.com/maps/api/geocode/json?address={request.Address}&sensor=false");

                return new Response()
                {
                    Latitude = googleResponse.Latitude,
                    Longitude = googleResponse.Longitude
                };
            }
            
        }
    }
}