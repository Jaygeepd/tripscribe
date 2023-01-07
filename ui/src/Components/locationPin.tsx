import React from 'react'
import { LocationOn } from '@mui/icons-material'
import { Stack } from '@mui/material'

interface ILocProps {
    pinText : string
};

function LocationPin(props: ILocProps) {
  return (
    <>
    <Stack direction="row">
        <LocationOn />
        <p>{props.pinText}</p>
    </Stack>
    </>
  )
}

export default LocationPin