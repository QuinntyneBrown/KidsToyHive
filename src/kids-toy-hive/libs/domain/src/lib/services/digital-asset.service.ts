import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { baseUrl } from '@kids-toy-hive/core';

@Injectable()
export class DigitalAssetService {
  constructor(
    @Inject(baseUrl) private _baseUrl:string,
    private _client: HttpClient
  ) { }


}
