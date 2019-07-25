import { BookingDetail } from './booking-detail.model';

export interface Booking {
  bookingId: string;
  name: string;
  bookingDetails: BookingDetail[];
  version: number;
}
