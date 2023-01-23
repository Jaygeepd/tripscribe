import { Stop } from "./stop";
import { Account } from "./account";

export type Trip = {
  id?: string;
  title: string;
  tripDesc: string;
  tripTimestamp: Date;
  public: boolean;
  attachedAccounts?: Account[];
  tripStartDate?: Date;
  tripEndDate?: Date;
  tripStops?: Stop[];
};
