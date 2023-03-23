// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace KidsToyHive.Core.Models;

public class EmailTemplate
{
    public Guid EmailTemplateId { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}

