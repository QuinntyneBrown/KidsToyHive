import { ProductCategory } from './product-category.model';
import { ProductImage } from './product-image.model';

export interface Product {
  productId: string;
  category: ProductCategory;
  name: string;
  productImages: ProductImage[];
  version: number;
}
