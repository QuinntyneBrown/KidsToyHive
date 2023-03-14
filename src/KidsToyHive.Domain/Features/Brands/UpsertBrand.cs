using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Brands;

 public class Validator : AbstractValidator<Request>
 {
     public Validator()
     {
         RuleFor(request => request.Brand).NotNull();
         RuleFor(request => request.Brand).SetValidator(new BrandDtoValidator());
     }
 }
 public class UpsertBrandRequest : IRequest<UpsertBrandResponse>
 {
     public BrandDto Brand { get; set; }
 }
 public class UpsertBrandResponse
 {
     public Guid BrandId { get; set; }
 }
 public class UpsertBrandHandler : IRequestHandler<UpsertBrandRequest, UpsertBrandResponse>
 {
     private readonly IAppDbContext _context;
     public UpsertBrandHandler(IAppDbContext context) => _context = context;
     public async Task<UpsertBrandResponse> Handle(UpsertBrandRequest request, CancellationToken cancellationToken)
     {
         var brand = await _context.Brands.FindAsync(request.Brand.BrandId);
         if (brand == null)
         {
             brand = new Brand();
             _context.Brands.Add(brand);
         }
         brand.Name = request.Brand.Name;
         await _context.SaveChangesAsync(cancellationToken);
         return new UpsertBrandResponse() { BrandId = brand.BrandId };
     }
 }
