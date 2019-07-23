import { DashboardCard } from './dashboard-card.model';

export interface Dashboard {
  dashboardId: string;
  name: string;
  dashboardCards: DashboardCard[];
  version: number;  
}
