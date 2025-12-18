// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable } from '@angular/core';
import { BaseHttpService } from '../../../core/services';
import { Brand } from '../models/brand.model';

@Injectable({
  providedIn: 'root'
})
export class BrandService extends BaseHttpService<Brand> {
  protected readonly endpoint = 'api/brands';
}
