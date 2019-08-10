using KidsToyHive.Core;
using KidsToyHive.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Models
{
    [Owned]
    public class Address : ValueObject, IGeoCoordinate
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string Province { get; private set; }
        public string PostalCode { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        
        public Address(string street, string city, string province, string postalCode)
        {
            Street = street;
            City = city;
            Province = province;
            PostalCode = postalCode;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return City;
            yield return Province;
            yield return PostalCode;
            yield return Longitude;
            yield return Latitude;
        }
    }
}
