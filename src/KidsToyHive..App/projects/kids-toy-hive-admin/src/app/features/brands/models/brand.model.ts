// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { BaseEntity } from '../../../core/models';

export interface Brand extends BaseEntity {
  name: string;
  description?: string;
  logoUrl?: string;
  websiteUrl?: string;
  isActive: boolean;
}
