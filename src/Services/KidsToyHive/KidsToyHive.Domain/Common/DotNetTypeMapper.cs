using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace KidsToyHive.Domain.Common;

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
