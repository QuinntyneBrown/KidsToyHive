using KidsToyHive.Domain;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Customers;

public class RemoveCustomerValidator : AbstractValidator<RemoveCustomerRequest>
{
    public RemoveCustomerValidator()
    {
        RuleFor(request => request.CustomerId).NotNull();
    }
}
public class RemoveCustomerRequest : IRequest
{
    public Guid CustomerId { get; set; }
}
public class RemoveCustomerHandler : IRequestHandler<RemoveCustomerRequest>
{
    private readonly IAppDbContext _context;
    public RemoveCustomerHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FindAsync(request.CustomerId);
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
