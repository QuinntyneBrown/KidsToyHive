import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { constants } from "../constants";
import { Job } from "../models/job.model";

@Injectable()
export class JobService {
  constructor(
    @Inject(constants.BASE_URL) private _baseUrl:string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Job>> {
    return this._client.get<{ jobs: Array<Job> }>(`${this._baseUrl}/api/jobs`)
      .pipe(
        map(x => x.jobs)
      );
  }

  public getById(options: { jobId: number }): Observable<Job> {
    return this._client.get<{ job: Job }>(`${this._baseUrl}/api/jobs/${options.jobId}`)
      .pipe(
        map(x => x.job)
      );
  }

  public remove(options: { job: Job }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}/api/jobs/${options.job.jobId}`);
  }

  public save(options: { job: Job }): Observable<{ jobId: number }> {
    return this._client.post<{ jobId: number }>(`${this._baseUrl}/api/jobs`, { job: options.job });
  }
}
