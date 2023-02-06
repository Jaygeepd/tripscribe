import { Stop } from "./stop";
import { Account } from "./account";

export type Trip = {
  id?: string;
  title: string;
  description: string;
  timestamp?: Date;
  publicView: boolean;
  accounts?: Account[];
  tripStartDate?: Date;
  tripEndDate?: Date;
  stops?: Stop[];
};
