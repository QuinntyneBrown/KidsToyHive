﻿using KidsToyHive.Domain;
using KidsToyHive.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/commands")]
    public class CommandsController
    {
        private readonly ICommandRegistry _commandRegistry;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ConcurrentDictionary<string, object> _locks = new ConcurrentDictionary<string, object>();
        private readonly IMediator _mediator;

        public CommandsController(
            ICommandRegistry commandRegistry,
            IHttpContextAccessor httpContextAccessor,
            IMediator mediator)
        {
            _commandRegistry = commandRegistry;
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CancellationToken cancellationToken = default)
        {
            var item = await CommandRegistryItem.ParseAsync(Request);

            var syncLock = _locks.GetOrAdd($"{item.PartitionKey}", id => new object());

            lock (syncLock)
                _commandRegistry.TryToAdd(item);

            if (item.HasConflicts())
                await Observable.Zip(_commandRegistry
                    .GetByCorrelationIds(item.ConflictingIds.Split(','))
                    .Select(x => x.Completed));

            dynamic result = default;

            try
            {
                result = await _mediator.Send(JsonConvert.DeserializeObject(item.Request,Type.GetType(item.RequestDotNetType)) as dynamic);

                item.Complete();
            }
            catch (Exception e)
            {
                item.Error();
            }


            return new JsonResult(result);
        }

        private HttpRequest Request => _httpContextAccessor.HttpContext.Request;
    }
}
