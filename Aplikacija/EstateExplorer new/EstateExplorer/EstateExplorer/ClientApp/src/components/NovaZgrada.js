import React, { Component, useState, useEffect } from 'react';
import { useParams } from "react-router-dom";
import Projekti from './Projekti';
import axios from 'axios';
import Zgrada from './Zgrada';
import { MenuItem, Select, Stack, Button, Box, Divider, CircularProgress, InputLabel, FormControl, } from '@mui/material';
import { useLocation, useNavigate } from 'react-router-dom';
import Stan from '../models/Stan';
import { getStanColumns, changeGroupingOrder } from '../services/StanSvc';
import {Checkbox, FormControlLabel, TextField } from '@mui/material';

import AddIcon from '@mui/icons-material/Add';
import DeleteIcon from '@mui/icons-material/Delete';    
import ContentCopyOutlinedIcon from '@mui/icons-material/ContentCopyOutlined';
import { green } from '@mui/material/colors';
import { DataGridPremium } from '@mui/x-data-grid-premium';








const NovaZgrada = () => {

    const [zgrade, setZgrada] = useState([]);
    const [loadingZgrada, setLoadingZgrada] = useState(true);
    const [submiting, setSubmiting] = React.useState(false);

    const navigate = useNavigate();

    useEffect(() => {
        axios.get(`http://localhost:3000/zgrade`)
            .then(p => setZgrada(p.data))
            .catch(p => setZgrada(new Zgrada()))
            .finally(p => setLoadingZgrada(false));
    }, []);


    const handleSubmit = (event) => {
        event.preventDefault();

        setSubmiting(true);

        let req;
        if (zgrade.id >= 0) {
            req = axios.put(`http://localhost:3000/zgrade/${zgrade.id}`, zgrade);
        }
        else {
            req = axios.post("http://localhost:3000/zgrade", zgrade)
        }
        req.then(p => {
            if (p.status === axios.HttpStatusCode.Ok || p.status === axios.HttpStatusCode.Created) {
                navigate('/dodajstanove', {
                    state: zgrade.Stanovi
                });
            }
            else {
                setSubmiting(false);
            }
        })
            .catch(p => {
                setSubmiting(false);
                console.error(p);
            });
    }

    const sxFormElements = { mx: 1, my: 1, p: 0, };


    return (

        <main className="dodaj">
            <TextField name="Naziv" label="Naziv" value={zgrade.Naziv}  sx={sxFormElements} required />
            <TextField name="Ulica" label="Ulica" value={zgrade.Ulica} sx={sxFormElements} required />
            <TextField name="BrojZgrade" label="Broj zgrade" value={zgrade.BrojZgrade} sx={sxFormElements} required />
            
            <TextField name="BrojKatastarskeParcele" label="Broj katastarske parcele" value={zgrade.BrojKatastarskeParcele} sx={sxFormElements} required />
            <TextField name="KatastarskaOpstina" label="Katastarska opština" value={zgrade.KatastarskaOpstina} sx={sxFormElements} required />
            <FormControlLabel sx={{ mx: 1, }} label="Lift" control={
                <Checkbox name="Lift" checked={zgrade.Lift} />
            } />
            <TextField name="BrojSpratova" label="Broj spratova" type="number" value={zgrade.BrojSpratova} sx={sxFormElements} required />
            <TextField name="Grejanje" label="Grejanje" value={zgrade.Grejanje}  sx={sxFormElements} required />
            <TextField name="Opis" label="Opis" value={zgrade.Opis}  sx={sxFormElements} multiline required />
            <Box sx={{ display: 'flex', flexDirection: 'row-reverse', alignItems: 'center' }}>
                <Box sx={{ m: 1, position: 'relative' }}>
                    <Button type="submit" variant="contained">
                        Pošalji
                    </Button>
                   
                    
                </Box>
            </Box>
            
        </main>
    );
};

export default NovaZgrada;

