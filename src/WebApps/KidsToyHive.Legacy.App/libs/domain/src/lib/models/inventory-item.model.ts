// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Product } from './product.model';

export interface InventoryItem {
  inventoryItemId: string;
  productId:string;
  name: string;
  version: number;
  product: Product;
}

