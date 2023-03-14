using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ProductCategories;

 public class GetProductCategoryByIdRequest : IRequest<GetProductCategoryByIdResponse>
 {
     public Guid ProductCategoryId { get; set; }
 }
 public class GetProductCategoryByIdResponse
 {
     public ProductCategoryDto ProductCategory { get; set; }
 }
 public class GetProductCategoryByIdHandler : IRequestHandler<GetProductCategoryByIdRequest, GetProductCategoryByIdResponse>
 {
     private readonly IAppDbContext _context;
     public GetProductCategoryByIdHandler(IAppDbContext context) => _context = context;
     public async Task<GetProductCategoryByIdResponse> Handle(GetProductCategoryByIdRequest request, CancellationToken cancellationToken)
         => new GetProductCategoryByIdResponse()
         {
             ProductCategory = (await _context.ProductCategories.FindAsync(request.ProductCategoryId)).ToDto()
         };
 }
