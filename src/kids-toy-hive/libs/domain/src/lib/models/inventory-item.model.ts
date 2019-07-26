import { Product } from './product.model';

export interface InventoryItem {
  inventoryItemId: string;
  productId:string;
  name: string;
  version: number;
  product: Product;
}
