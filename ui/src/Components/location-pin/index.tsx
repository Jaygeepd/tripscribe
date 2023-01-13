import React from 'react'
import { LocationOn } from '@mui/icons-material'
import { Stack } from '@mui/material'
import { red } from '@mui/material/colors'

interface ILocProps {
    lat: number
    lng: number
    pinText : string
};

function LocationPin(props: ILocProps) {
  return (
    <>
      <div>
        <Stack direction="row">
          <LocationOn sx={{color: red[900]}} />
          <b>{props.pinText}</b>
        </Stack>
          
      </div>
    </>
  )
}

export default LocationPin