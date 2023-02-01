import React, { useState } from 'react';
import { AccountService, TripService } from '../../services';
import { AuthContext } from '../../contexts';
import { LoginUtils } from '../../utils';
import toast from 'react-hot-toast';
import { Account } from '../../types/account';
import { Trip } from '../../types/trip';

function ProfilePage() {

  const [isLoading, setIsLoading] = useState(true);
  const [loggedAccount, setLoggedAccount] = useState<Account>();
  const [accountTrips, setAccountTrips] = useState<Trip[]>();

  const { state } = AuthContext.useLogin();
  const accountId = LoginUtils.getAccountId(state.accessToken);

  const LoadInitData = async (accountId: string) => {

    const [foundAccount, foundTrips] = await Promise.all([
      AccountService.getAccount(accountId), TripService.getTripsByAccount(accountId)
    ]);

    setLoggedAccount(await foundAccount.json());
    setAccountTrips(await foundTrips.json());

  }
  

  const accountReducer = (state: Account, action: any) => {
    switch(action.type) {
      case "Update":
        return {
          ...state,
          [action.field]: action.value,
        };
      default:
        return state;
    }
  }

  return (
    <>
    
    </>
  )
}



export default ProfilePage