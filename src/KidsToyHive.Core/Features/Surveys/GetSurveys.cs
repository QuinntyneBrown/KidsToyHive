// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Surveys;

public class GetSurveysRequest : IRequest<GetSurveysResponse> { }
public class GetSurveysResponse
{
    public IEnumerable<SurveyDto> Surveys { get; set; }
}
public class GetSurveysHandler : IRequestHandler<GetSurveysRequest, GetSurveysResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetSurveysHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetSurveysResponse> Handle(GetSurveysRequest request, CancellationToken cancellationToken)
        => new GetSurveysResponse()
        {
            Surveys = await _context.Surveys.Select(x => x.ToDto()).ToArrayAsync()
        };
}

