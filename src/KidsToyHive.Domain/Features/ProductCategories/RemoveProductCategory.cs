using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ProductCategories;

 public class Validator : AbstractValidator<Request>
 {
     public Validator()
     {
         RuleFor(request => request.ProductCategoryId).NotNull();
     }
 }
 public class RemoveProductCategoryRequest : IRequest
 {
     public Guid ProductCategoryId { get; set; }
 }
 public class RemoveProductCategoryHandler : IRequestHandler<Request>
 {
     private readonly IAppDbContext _context;
     public RemoveProductCategoryHandler(IAppDbContext context) => _context = context;
     public async Task<Unit> Handle(RemoveProductCategoryRequest request, CancellationToken cancellationToken)
     {
         var productCategory = await _context.ProductCategories.FindAsync(request.ProductCategoryId);
         _context.ProductCategories.Remove(productCategory);
         await _context.SaveChangesAsync(cancellationToken);
         return new Unit();
     }
 }
