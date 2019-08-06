import { HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { ProblemDetails } from '@kids-toy-hive/core';

export class ErrorService {
    public handleHttpError(response: HttpErrorResponse):Observable<ProblemDetails> { 
      const problemDetails = <ProblemDetails>{
        type:response.error.Type,
        title: response.error.Title
      };
        
      try {
        problemDetails.detail = JSON.parse(response.error.Detail);
      } catch (e) {
        problemDetails.detail = response.error.Detail;
      }
      
      return of(problemDetails);
    }
}