// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Extensions;
using KidsToyHive.Core.Common;
using MediatR;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Geolocation;

public class GetAddressFromLatitudeAndLongitudeRequest : AuthenticatedRequest<GetAddressFromLatitudeAndLongitudeResponse>
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
public class GetAddressFromLatitudeAndLongitudeResponse
{
    public string Address { get; set; }
}
public class GetAddressFromLatitudeAndLongitudeHandler : IRequestHandler<GetAddressFromLatitudeAndLongitudeRequest, GetAddressFromLatitudeAndLongitudeResponse>
{
    protected readonly HttpClient _client;
    public GetAddressFromLatitudeAndLongitudeHandler(HttpClient client)
    {
        _client = client;
    }
    public async Task<GetAddressFromLatitudeAndLongitudeResponse> Handle(GetAddressFromLatitudeAndLongitudeRequest request, CancellationToken token)
    {
        var googleEncodeResponse = await _client.GetAsync<GoogleEncodeResponse>($"http://maps.googleapis.com/maps/api/geocode/json?latlng={request.Latitude},{request.Longitude}&sensor=false");
        var addressComponents = googleEncodeResponse.results.ElementAt(0).address_components;
        var streetAddress = addressComponents.First(x => x.types.Any(t => t == "street_number")).long_name;
        var street = addressComponents.First(x => x.types.Any(t => t == "route")).long_name;
        var city = addressComponents.First(x => x.types.Any(t => t == "locality")).long_name;
        return new GetAddressFromLatitudeAndLongitudeResponse()
        {
            Address = $"{streetAddress} {street}, {city}"
        };
    }

}

