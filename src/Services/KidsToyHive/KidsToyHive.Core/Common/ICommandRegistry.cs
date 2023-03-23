// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace KidsToyHive.Core.Common;

public interface ICommandRegistry
{
    List<CommandRegistryItem> GetByCorrelationIds(string[] correlationIds);
    void TryToAdd(CommandRegistryItem item, CancellationToken cancellationToken = default);
    List<CommandRegistryItem> GetAll();
    void Remove(CommandRegistryItem item);
}

