// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace KidsToyHive.Core.Common;

public static class DotNetTypeMapper
{
    private static readonly ConcurrentDictionary<string, string> _types = new ConcurrentDictionary<string, string>();
    static DotNetTypeMapper()
    {
        foreach (var type in typeof(DotNetTypeMapper).GetTypeInfo().Assembly.GetTypes())
        {
            if (type.AssemblyQualifiedName.Contains("+Request"))
                _types.TryAdd(type.DeclaringType.Name, type.AssemblyQualifiedName);
        }
    }

    public static string Map(string key)
    {
        _types.TryGetValue(key, out string value);
        return value;
    }
}

