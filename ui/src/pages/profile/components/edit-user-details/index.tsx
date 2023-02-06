import { Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, useMediaQuery, useTheme, Stack, TextField, Button } from '@mui/material';
import React, { useReducer } from 'react'
import { toast } from 'react-hot-toast';
import { AccountService } from '../../../../services';
import { Account } from '../../../../types/account';

interface IHookProps {
  initialAccount: Account,
  dialogState: boolean,
  setState: any
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
};

function EditUserDetails({initialAccount, dialogState, setState} : IHookProps) {
  const theme = useTheme();
  const fullScreen: any = useMediaQuery(theme.breakpoints.down("sm"));

  const [updateAccount, dispatch] = useReducer(accountReducer, initialAccount)

  const onUpdateAccount = async (updateAccount: Account) => {
    const response = await AccountService.updateAccount(updateAccount);

    if(response.status === 204) {
      setState(false);
      toast.success("Account updated successfully!");
    } else {
      toast.error(
        "There was a problem updating the account, please try again"
      );
    }
  };

  return (
    <>
      <Dialog
        fullScreen={fullScreen}
        open={dialogState}
        onClose={setState}
      >
        <DialogTitle>Edit Details</DialogTitle>

        <DialogContent>
          <DialogContentText>
            Enter the new details for your account below
          </DialogContentText>

          <Stack spacing={2} sx={{ paddingTop: "2vh "}}>
          <TextField
              label="First Name"
              variant="filled"
              onChange={(e) =>
                dispatch({
                  value: e.target.value,
                  type: "Update",
                  field: "firstName",
                })
              }
              value={updateAccount.firstName}
            />

            <TextField
              label="Last Name"
              variant="filled"
              onChange={(e) =>
                dispatch({
                  value: e.target.value,
                  type: "Update",
                  field: "lastName",
                })
              }
              value={updateAccount.lastName}
            />
          </Stack>
        </DialogContent>

        <DialogActions>
          <Button variant="contained" onClick={() => onUpdateAccount(updateAccount)}>
              Update Account
          </Button>
          <Button onClick={setState}>Back</Button>
        </DialogActions>
      </Dialog>
    </>
  )
}

export default EditUserDetails;