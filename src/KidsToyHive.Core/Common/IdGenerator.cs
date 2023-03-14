using System;

namespace KidsToyHive.Core.Common;

public static class IdGenerator
{
    public static string GetNextId() => $"{Guid.NewGuid()}";
}
