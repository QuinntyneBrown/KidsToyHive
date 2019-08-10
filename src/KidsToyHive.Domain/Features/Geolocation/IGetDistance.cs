using GeoCoordinatePortable;

namespace KidsToyHive.Domain.Features.Geolocation
{
    public interface IGetDistance
    {
        public double GetDistance(double destinationLongitude, double destinationLatitude, double originLongitude, double originLatitude)
        {
            var origin = new GeoCoordinate(originLatitude, originLongitude);

            var destination = new GeoCoordinate(destinationLatitude, destinationLongitude);

            return origin.GetDistanceTo(destination);
        }
    }
}
