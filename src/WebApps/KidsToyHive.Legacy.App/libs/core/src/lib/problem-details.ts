// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

export interface ProblemDetails {
    detail:string;
    title: string;
    type:string;
}

export function isProblemDetails(object:any):boolean {
    try {
        return 'type' in object 
        && 'detail' in object
        && 'title' in object;
    }
    catch 
    {
        return false;
    }
}
