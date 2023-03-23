// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Surveys;

public class RemoveSurveyValidator : AbstractValidator<RemoveSurveyRequest>
{
    public RemoveSurveyValidator()
    {
        RuleFor(request => request.SurveyId).NotNull();
    }
}
public class RemoveSurveyRequest : IRequest
{
    public Guid SurveyId { get; set; }
}
public class RemoveSurveyHandler : IRequestHandler<RemoveSurveyRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveSurveyHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveSurveyRequest request, CancellationToken cancellationToken)
    {
        var survey = await _context.Surveys.FindAsync(request.SurveyId);
        _context.Surveys.Remove(survey);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

