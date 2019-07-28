import { Address } from './address.model';

export interface Location {
  locationId: string;
  name: string;
  version: number;
  address: Address
}
