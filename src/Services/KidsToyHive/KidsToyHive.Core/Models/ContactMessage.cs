// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace KidsToyHive.Core.Models;

public class ContactMessage
{
    public Guid ContactMessageId { get; set; }
    public string Value { get; set; }
    public int Version { get; set; }
}

