// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace KidsToyHive.Core.Models;

public class Video : BaseModel
{
    public Guid VideoId { get; set; }
    public string Category { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public string Slug { get; set; }
    public string YouTubeVideoId { get; set; }
    public string Abstract { get; set; }
    public int DurationInSeconds { get; set; }
    public double Rating { get; set; }
    public string Description { get; set; }
    public DateTime? Published { get; set; }
}

