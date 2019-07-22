using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace KidsToyHive.Domain.Common
{
    public interface ICommandRegistry
    {
        List<CommandRegistryItem> GetByCorrelationIds(string[] correlationIds);
        void TryToAdd(CommandRegistryItem item, CancellationToken cancellationToken = default);
        List<CommandRegistryItem> GetAll();
        void Remove(CommandRegistryItem item);
    }
}
