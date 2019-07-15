using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Api
{
    public class Worker : BackgroundService
    {
        private readonly ICommandRegistry _registry;
        
        public Worker(ICommandRegistry registry)
        {
            _registry = registry;
        }

        private bool GivenConflictingIdsHaveBeenProcessed(CommandRegistryItem item)
            => !(_registry.GetByCorrelationIds(item.ConflictingIds.Split(',')))
            .Any(x => x.State < CommandRegistryItemState.Completed);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var items = _registry.GetAll();

                foreach (var commandRegistryItem in items)
                {
                    if (commandRegistryItem.State == CommandRegistryItemState.Sleeping && (commandRegistryItem.HasNoConflicts() || GivenConflictingIdsHaveBeenProcessed(commandRegistryItem)))
                    {
                        commandRegistryItem.Run();

                        _ = _client.PostAsync(commandRegistryItem).ContinueWith(x =>
                        {
                            if (x.Exception is Exception)
                            {
                                commandRegistryItem.Error();
                            }
                            else
                            {
                                commandRegistryItem.Result = x.Result;
                                commandRegistryItem.Complete();
                                _notificationService.Feed(items);
                            }
                        });
                    }
                }

                await Task.Delay(1, stoppingToken);
            }
        }
    }
}
