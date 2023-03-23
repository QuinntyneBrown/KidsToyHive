// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { DashboardCard } from './dashboard-card.model';

export interface Dashboard {
  dashboardId: string;
  name: string;
  dashboardCards: DashboardCard[];
  version: number;  
}

