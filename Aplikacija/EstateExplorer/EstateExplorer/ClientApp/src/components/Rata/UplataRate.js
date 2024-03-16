import * as React from 'react';
import { Backdrop, Button, CircularProgress, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, FormControlLabel, FormLabel, Radio, RadioGroup, Snackbar, TextField } from "@mui/material";
import MuiAlert from '@mui/material/Alert';
import { RataUplata } from '../../models/RataUplata';
import axios from 'axios';

const Alert = React.forwardRef(function Alert(props, ref) {
  return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
});

export function UplataRate(props)
{
    const [open, setOpen] = React.useState(false);
    const [sending, setSending] = React.useState(false);
    const [rata, setRata] = React.useState(new RataUplata());

    const handleClickOpen = () => {
      setOpen(true);
    };
  
    const handleAccept = () => {
      setSending(true);

      axios.put(`http://localhost:3000/rate/${props.id}`, rata)
      .then(res => {
        if(res.status === axios.HttpStatusCode.Ok 
          || res.status === axios.HttpStatusCode.Created 
          || res.status === axios.HttpStatusCode.Accepted) {
            setSending(false);
            setOpen(false);
            props.onSuccessAdd();
        }
        setSending(false);
      })
      .catch(err => {
        console.error(err);
        setSending(false);
      });    
    }

    const handleReject = () => {
      setOpen(false);
    };
  
    const handleInputChange = (event) => {
      setRata(prevRata => {
        const { name, value } = event.target;
        return {...prevRata, [name]: value};
      });
    }

    return (
      <>
        <Button variant="contained" onClick={handleClickOpen} color="primary" sx={{m: 1,}}>
          Uplati ratu
        </Button>

        <Dialog open={open} onClose={handleReject}>
          <DialogTitle>Uplata rate</DialogTitle>

          <DialogContent>
            <DialogContentText>Neki tekst vezan za uplatu rate.</DialogContentText>
            <FormLabel>Valuta</FormLabel>
            <RadioGroup value={rata.valuta} onChange={handleInputChange} >
                <FormControlLabel name="valuta" label="EUR" control={<Radio />} value="EUR" />
                <FormControlLabel name="valuta" label="RSD" control={<Radio />} value="RSD" />
            </RadioGroup>
            <TextField type="number" name="iznos" label="Iznos" variant="outlined" value={rata.iznos} onChange={handleInputChange} />
            <br></br>
            <FormLabel>Kes/Ostalo</FormLabel>
            <RadioGroup value={rata.kes} onChange={handleInputChange}>
                <FormControlLabel name="kes" label="Kes" control={<Radio />} value="Kes" />
                <FormControlLabel name="kes" label="Ostalo" control={<Radio />} value="Ostalo" />
            </RadioGroup>
          </DialogContent>

          <DialogActions>
            <Button onClick={handleAccept} color="success"><b>Plati</b></Button>
            <Button onClick={handleReject} color="error"><b>Zatvori</b></Button>
            <Backdrop sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }} open={sending} >
              <CircularProgress color="inherit" />
            </Backdrop>
          </DialogActions>
        </Dialog>
      </>
    );
}