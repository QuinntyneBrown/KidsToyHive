using GeoCoordinatePortable;

namespace KidsToyHive.Core.Interfaces
{
    public interface IGeoCoordinate
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public GeoCoordinate GeoCoordinate => new GeoCoordinate(Latitude, Longitude);

        public double GetDistanceTo(IGeoCoordinate destination)
            => GeoCoordinate.GetDistanceTo(destination.GeoCoordinate);
    }
}
