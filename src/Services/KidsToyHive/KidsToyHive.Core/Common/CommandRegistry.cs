// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Enums;
using KidsToyHive.Core.Exceptions;
using KidsToyHive.Core.Features.Users;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace KidsToyHive.Core.Common;

public class CommandRegistry : ICommandRegistry
{
    private readonly ConcurrentDictionary<string, CommandRegistryItem> _inner = new ConcurrentDictionary<string, CommandRegistryItem>();
    private readonly ConcurrentDictionary<string, string> _types = new ConcurrentDictionary<string, string>();
    
    public CommandRegistry()
    {
        foreach (var type in typeof(AuthenticateRequest).GetTypeInfo().Assembly.GetTypes())
        {
            if (type.AssemblyQualifiedName.Contains("+Request"))
                _types.TryAdd(type.DeclaringType.Name, type.AssemblyQualifiedName);
        }
    }

    public List<CommandRegistryItem> GetByCorrelationIds(string[] correlationIds)
         => _inner
        .Where(x => correlationIds.Contains(x.Key))
        .Select(x => x.Value)
        .ToList();
    public void TryToAdd(CommandRegistryItem item, CancellationToken cancellationToken = default)
    {
        if (_inner.Where(x => !string.IsNullOrEmpty(item.Key) && x.Value.Key == item.Key && x.Value.State < CommandRegistryItemState.Cancelled).Any())
            throw new ConcurrencyException();
        item.CancellationToken = cancellationToken;
        item.SetConflictingIds(GetIdsOfConflictingIncompleteRequests(item));
        item.Index = _inner.Count() + 1;
        _inner.TryAdd(item.CorrelationId, item);
    }
    private string GetIdsOfConflictingIncompleteRequests(CommandRegistryItem item)
        => string.Join(",", GetAllIncomplete(item.PartitionKey)
            .Where(incompleteRequest => item.ConflictsWith(incompleteRequest))
            .Select(x => x.CorrelationId));
    public List<CommandRegistryItem> GetAllIncomplete(string partitionKey)
        => _inner
        .Where(x => x.Value.State < CommandRegistryItemState.Completed && x.Value.PartitionKey == partitionKey)
        .Select(x => x.Value)
        .ToList();
    public List<CommandRegistryItem> GetAll()
        => _inner.Select(x => x.Value)
        .OrderBy(x => x.Index)
        .ToList();
    public void Remove(CommandRegistryItem item)
        => _inner.TryRemove(item.CorrelationId, out _);
}

