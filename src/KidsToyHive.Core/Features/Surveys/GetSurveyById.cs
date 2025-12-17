// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Surveys;

public class GetSurveyByIdRequest : IRequest<GetSurveyByIdResponse>
{
    public Guid SurveyId { get; set; }
}
public class GetSurveyByIdResponse
{
    public SurveyDto Survey { get; set; }
}
public class GetSurveyByIdHandler : IRequestHandler<GetSurveyByIdRequest, GetSurveyByIdResponse>
{
    public IKidsToyHiveDbContext _context { get; set; }
    public GetSurveyByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetSurveyByIdResponse> Handle(GetSurveyByIdRequest request, CancellationToken cancellationToken)
        => new GetSurveyByIdResponse()
        {
            Survey = (await _context.Surveys.FindAsync(request.SurveyId)).ToDto()
        };
}

