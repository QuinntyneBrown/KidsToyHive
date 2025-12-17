// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Extensions;
using KidsToyHive.Core.Common;
using MediatR;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Geolocation;

public class GetLongLatCoordinatesRequest : AuthenticatedRequest<GetLongLatCoordinatesResponse>
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

