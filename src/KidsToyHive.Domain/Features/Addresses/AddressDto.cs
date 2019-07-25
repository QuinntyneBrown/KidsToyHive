using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.Addresses
{
    public class AddressDtoValidator: AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {

        }
    }

    public class AddressDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }

    }

    public static class AddressExtensions
    {
        public static AddressDto ToDto(this Address address)
        {
            return new AddressDto
            {
                Street = address.Street,
                City = address.City,
                Province = address.Province,
                PostalCode = address.PostalCode
            };
        }
    }
}
