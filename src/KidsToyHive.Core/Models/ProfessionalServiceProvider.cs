// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Enums;
using System;

namespace KidsToyHive.Core.Models;

public class ProfessionalServiceProvider : BaseModel
{
    public Guid ProfessionalServiceProviderId { get; set; }
    public string Title { get; set; }
    public string FullName { get; set; }
    public string ImageUrl { get; set; }
    public ProfessionalServiceProviderType Type { get; set; }
}

