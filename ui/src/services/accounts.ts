import userEvent from "@testing-library/user-event";
import { Account } from "../types/account";
import { FetchUtils } from "../utils";

const createAccount = async (newAccount: Account) => {
  const { firstName, lastName, email, password } = newAccount;
  return await FetchUtils.fetchInstance(`accounts`, {
    method: "POST",
    body: JSON.stringify({
      firstName,
      lastName,
      email,
      password,
    }),
  });
};

const updateAccount = async (updateAccount: Account) => {
  return await FetchUtils.fetchInstance(`accounts/${updateAccount.id}`, {
    method: "PATCH",
  });
};

const deleteAccount = async (accountId: string) => {
  return await FetchUtils.fetchInstance(`accounts/${accountId}`, {
    method: "DELETE",
  });
};

const getAccount = async (accountId: string) => {
  return await FetchUtils.fetchInstance(`accounts/${accountId}`, {
    method: "GET",
  });
};

const getAccountsByTrip = async (tripId: string) => {
  return (
    await FetchUtils.fetchInstance(`/trips/${tripId}/accounts`),
    {
      method: "GET",
    }
  );
};

export default {
  createAccount,
  updateAccount,
  deleteAccount,
  getAccount,
  getAccountsByTrip
};