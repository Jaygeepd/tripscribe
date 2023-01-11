import { EditAttributes } from '@mui/icons-material'
import { Grid, IconButton, Paper, Stack } from '@mui/material'
import React from 'react'
import { Map } from "../../Components"
import { ITripProps } from '../../Components/trip-details'


function TripViewPage() {
  return (
    <>
    <Paper sx={{ padding: "5vw", height: "100vh", overflow: "auto" }}>
        <Grid container>
            <Grid item xs={4}>
                <h1>Bleh</h1>
            </Grid>
            <Grid item xs={8} justifyContent="flex-end">
                <Stack>
                    <IconButton>
                        <EditAttributes />
                    </IconButton>
                </Stack>
            </Grid>
        </Grid>
    </Paper>
    </>
  )
}

export default TripViewPage