import { OrderItem } from './order-item.model';

export interface Order {
  orderId: string;
  name: string;
  orderItems: OrderItem[];
  version: number;
}
