// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { BookingDetail } from '../../core';
import { Location } from '../../core';

export interface Booking {
  bookingId?: string;
  name?: string;
  bookingDetails: BookingDetail[];
  location:Location;
  version?: number;
  bookingTimeSlot:number;
  date:string;
}

