// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace KidsToyHive.Core.Models;

public class DashboardCard
{
    public Guid DashboardCardId { get; set; }
    public Guid DashboardId { get; set; }
    public string Options { get; set; }
    public Guid CardId { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
}

