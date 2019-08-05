export interface ProblemDetails {
    type:string;
    detail:string;    
}

export function isProblemDetails(object:any):boolean {
    try {
        return 'type' in object 
        && 'detail' in object;
    }
    catch 
    {
        return false;
    }
}