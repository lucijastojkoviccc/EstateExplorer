import * as React from 'react';
import { useEffect, useState } from 'react';
import Zgrada from '../models/Zgrada';
import { Button, Checkbox, FormControlLabel, TextField } from '@mui/material';
import Box from '@mui/material/Box';
import CircularProgress from '@mui/material/CircularProgress';
import { blue, green } from '@mui/material/colors';
import axios from 'axios';
import { Navigate, useLocation, useNavigate } from "react-router-dom";
import { isAdminRadnik } from "../services/UlogaSvc";

export default function DodajZgradu() {
    const { state } = useLocation();

    const [zgrada, setZgrada] = useState(new Zgrada());
    const [loadingZgrada, setLoadingZgrada] = useState(true);

    useEffect(() => {
        //let id = Math.floor(Math.random() * 5);
        if (state && state.id !== null && state.id !== undefined) {
            axios.get(`api/GetZgradePoParametruPretrage?searchParam=${state.id}`)
                .then(p => { console.log(p.data); setZgrada(p.data.zgrade[0]) })
                .catch(p => setZgrada(new Zgrada()))
                .finally(p => setLoadingZgrada(false));
        }
        else {
            setZgrada(new Zgrada());
            setLoadingZgrada(false);
        }

    }, []);

    const handleInputChange = (event) => {
        const { name, value, type, checked } = event.target;
        setZgrada(prevState => {
            let z = new Zgrada(prevState);
            if (type === 'checkbox') {
                z[name] = checked;
            }
            else {
                z[name] = value;
            }
            return z;
        });
    }

    const [submiting, setSubmiting] = React.useState(false);
    const navigate = useNavigate();

    const handleSubmit = (event) => {
        event.preventDefault(); 

        setSubmiting(true);

        //const formData = new FormData();
        //formData.append('upload', imageFile);

       

        let req;
        if (zgrada.id >= 0) {
            req = axios.put(`api/AzurirajZgradu`, zgrada, { withCredentials: true });
        }
        else {
            req = axios.post("api/DodajZgradu", zgrada, { withCredentials: true })
        }
        req.then(p => {

            if (p.status === axios.HttpStatusCode.Ok || p.status === axios.HttpStatusCode.Created || p.status === axios.HttpStatusCode.NoContent) {
                //axios.post(`http://localhost:3000/${zgrada.id}`, formData)

                navigate('/dodajstanove', {
                    state: {
                        stanovi: zgrada.stanovi,
                        id: zgrada.id,
                    }
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

    /*const [imageFile, setImageFile] = useState(null);*/

    //const handleImageChange = (event) => {
    //    const file = event.target.files[0];
    //    setImageFile(file);
    //};

    if(!isAdminRadnik()) {
        return (<Navigate to="/Identity/Pages/Account/Login" replace />);
    }

    return (
        <>
            {loadingZgrada && (
                <CircularProgress
                    size={72}
                    sx={{
                        color: blue[500],
                        position: 'relative',
                        top: '0%',
                        left: '50%',
                        marginTop: '-12px',
                        marginLeft: '-12px',
                    }}
                />
            )}
            {!loadingZgrada && (
                <Box sx={
                    {
                        margin: "auto",
                        backgroundColor: "white",
                        maxWidth: "500px",
                        display: "flex",
                        flexDirection: "column",
                    }
                }>
                    <TextField name="naziv" label="Naziv" value={zgrada.naziv} onChange={handleInputChange} sx={sxFormElements} required />
                    <TextField name="ulica" label="Ulica" value={zgrada.ulica} onChange={handleInputChange} sx={sxFormElements} required />
                    <TextField name="brojZgrade" label="Broj zgrade" value={zgrada.brojZgrade} onChange={handleInputChange} sx={sxFormElements} required />
                    <TextField name="brojKatastarskeParcele" label="Broj katastarske parcele" value={zgrada.brojKatastarskeParcele} onChange={handleInputChange} sx={sxFormElements} required />
                    <TextField name="katastarskaOpstina" label="Katastarska opština" value={zgrada.katastarskaOpstina} onChange={handleInputChange} sx={sxFormElements} required />
                    <FormControlLabel sx={{ mx: 1, }} label="Lift" control={
                        <Checkbox name="lift" checked={zgrada.lift} onChange={handleInputChange} />
                    } />
                    <TextField name="brojSpratova" label="Broj spratova" type="number" value={zgrada.brojSpratova} onChange={handleInputChange} sx={sxFormElements} required />
                    <TextField name="grejanje" label="Grejanje" value={zgrada.grejanje} onChange={handleInputChange} sx={sxFormElements} required />
                    <TextField name="opis" label="Opis" value={zgrada.opis} onChange={handleInputChange} sx={sxFormElements} multiline required />
                  {/*  <input type="file" accept="image/*" onChange={handleImageChange} />*/}
                    <Box sx={{ display: 'flex', flexDirection: 'row-reverse', alignItems: 'center' }}>
                        <Box sx={{ m: 1, position: 'relative' }}>
                            <Button type="submit" variant="contained" disabled={submiting} onClick={handleSubmit}>
                                Pošalji
                            </Button>
                            {submiting && (
                                <CircularProgress
                                    size={24}
                                    sx={{
                                        color: green[500],
                                        position: 'absolute',
                                        top: '50%',
                                        left: '50%',
                                        marginTop: '-12px',
                                        marginLeft: '-12px',
                                    }}
                                />
                            )}
                        </Box>
                    </Box>
                </Box>
            )}
        </>
    );
}