import { BookingDetail } from './booking-detail.model';
import { Location } from './location.model';

export interface Booking {
  bookingId?: string;
  name?: string;
  bookingDetails: BookingDetail[];
  location:Location;
  version?: number;
  bookingTimeSlot:number;
}
