// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { ProductCategory } from './product-category.model';
import { ProductImage } from './product-image.model';

export interface Product {
  productId: string;
  category: ProductCategory;
  name: string;
  productImages: ProductImage[];
  version: number;
}

