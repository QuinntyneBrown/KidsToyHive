import { Product } from './product.model';

export interface BookingDetail {
  bookingDetailId?: string;
  name?: string;
  product?: Product;
  productId: string;
  quantity:number;
  version?: number;
}
