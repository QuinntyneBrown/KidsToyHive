using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KidsToyHive.Domain.Features.HtmlContents
{
    public class GetHtmlContentByName
    {

        public class Request : IRequest<Response> {
            public string Name { get; set; }
        }

        public class Response
        {
            public HtmlContentDto HtmlContent { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    HtmlContent = (await _context.HtmlContents.SingleAsync(x => x.Name == request.Name)).ToDto()
                };
        }
    }
}
