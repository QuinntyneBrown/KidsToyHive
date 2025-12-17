// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Product } from '../../core';

export interface BookingDetail {
  bookingDetailId?: string;
  name?: string;
  product?: Product;
  productId: string;
  quantity:number;
  version?: number;
}

