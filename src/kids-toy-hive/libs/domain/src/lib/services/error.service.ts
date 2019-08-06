import { HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { ProblemDetails } from '@kids-toy-hive/core';

export class ErrorService {
    public handleHttpError(response: HttpErrorResponse):Observable<ProblemDetails> {   
        return of({
          type:response.error.Type,
          title: response.error.Title,
          detail:JSON.parse(response.error.Detail)
        });
      }
}