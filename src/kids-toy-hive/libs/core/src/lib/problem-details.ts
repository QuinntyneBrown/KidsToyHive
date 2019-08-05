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