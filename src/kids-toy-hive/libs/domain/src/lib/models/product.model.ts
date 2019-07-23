import { ProductCategory } from './product-category.model';

export interface Product {
  productId: string;
  category: ProductCategory;
  name: string;
  version: number;
}
