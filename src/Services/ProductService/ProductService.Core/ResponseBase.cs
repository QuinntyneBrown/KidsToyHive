// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ProductService.Core;

public class ResponseBase
{
    public ResponseBase(){
        Errors = new List<string>();
    }

    public List<string> Errors { get; set; }
}


