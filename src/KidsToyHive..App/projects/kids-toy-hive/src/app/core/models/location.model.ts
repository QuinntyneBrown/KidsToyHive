// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Address } from './address.model';

export interface Location {
  locationId?: string;
  name?: string;
  version?: number;
  address: Address
}

