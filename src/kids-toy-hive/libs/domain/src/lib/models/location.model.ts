import { Address } from 'cluster';

export interface Location {
  locationId: string;
  name: string;
  version: number;
  address: Address
}
