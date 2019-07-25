import { CustomerLocation } from './customer-location.model';
import { Address } from './address.model';

export interface Customer {
  customerId: string;
  firstName:string;
  lastName:string;
  phoneNumber: string;
  email:string;
  address: Address;
  customerLocations: CustomerLocation[];
  version: number;
}
