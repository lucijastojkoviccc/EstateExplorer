import * as React from 'react';
import { Alert, Box, FormControl, InputLabel, MenuItem, Select, Snackbar } from "@mui/material";
import { UplataRate } from "./UplataRate";
import { useEffect, useState } from "react";
import axios from "axios";
import 'moment/locale/sr';
import { DataGridPro, } from "@mui/x-data-grid-pro";
import getRateCols from "./RateSvc";
import { isAdminRadnik, isKupac, isKupacOnly } from "../../services/UlogaSvc";
import { Navigate } from "react-router";

export function RatePrikaz()
{
    const [openSnackbar, setOpenSnackbar] = useState(false);

    const handleCloseSnackbar = (event, reason) => {
        if (reason === 'clickaway') {
          return;
        }
        setOpenSnackbar(false);
    };

    const [rate, setRate] = useState([]);
    const [osveziPodatke, setOsveziPodatke] = useState(0);

    const onSuccessAdd = () => {
        setOsveziPodatke(osveziPodatke + 1);
        setOpenSnackbar(true);
    }

    const [pageState, setPageState] = useState({
        isLoading: false,
        total: 0,
        paginationModel: {
            page: 0,
            pageSize: 3,
        }
    });
    
    const [idNekretnine, setIdNekretnine] = useState(0);
    const [nekretnine, setNekretnine] = useState([]);

    useEffect(() => {
        if (isKupacOnly()) {
            axios.get(`api/VratiStanoveKorisnika`)
                .then(res => {
                    setNekretnine(res.data);
                    setIdNekretnine(res.data[0].id);
                })
        }
    }, []);

    useEffect(() => {
        if (isAdminRadnik()) {
            console.log("AdminR");
            axios.get(`api/VratiRateRoles`)
                .then(res => {
                    if (res.status === axios.HttpStatusCode.Ok) {
                        console.log(res.data);
                        setRate(res.data);
                    }
                })
                .catch(err => console.error(err));
        }
    }, [osveziPodatke])

    useEffect(() => {
        (async () => {
            if (isKupacOnly()) {
                setPageState(old => ({ ...old, isLoading: true }));
                // const response = await axios.get(`api/VratiRateRoles?_page=${pageState.paginationModel.page + 1}&_limit=${pageState.paginationModel.pageSize}`);
                const response = await axios.get(`api/VratiRateRolesUser/${idNekretnine}`);
                setRate(response.data.map(p => ({ ...p, datumKupac: new Date(p.datumKupac), datumRadnik: new Date(p.datumRadnik) })));
                setPageState(old => ({ ...old, isLoading: false, total: 15/* response.total */ }));
            }
        })();
      }, [pageState.paginationModel, osveziPodatke, idNekretnine]
    );

    const handleNekretninaChange = (event) => {
        console.log(event.target.value);
        setIdNekretnine(event.target.value);
    };
    
    if(!isKupac()) {
        return (<Navigate to="/Identity/Pages/Account/Login" replace />)
    }

    return (
        <Box sx={{
            backgroundColor: 'white',
            // height: 300,
            // width: '100%',
            '& .success': {
                backgroundColor: 'green',
            },
            '& .warning': {
                backgroundColor: 'orange',
            },
            '& .error': {
                backgroundColor: 'red',
            },
            pt: 1,
            pl: 1,
            }}>
            {isKupacOnly() && 
                <>
                    <FormControl>
                        <InputLabel id="demo-simple-select-label">Nekretnina</InputLabel>
                        <Select
                            labelId="demo-simple-select-label"
                            id="demo-simple-select"
                            value={idNekretnine}
                            label="Nekretnina"
                            onChange={handleNekretninaChange}
                            sx={{ minWidth: 120 }}
                        >
                            {nekretnine.map(n => <MenuItem value={n.id}>{n.naziv}</MenuItem>)}
                        </Select>
                    </FormControl>                
                    <UplataRate id={idNekretnine} onSuccessAdd={onSuccessAdd}/>
                </>
            }
            <DataGridPro 
                rows={rate} 
                columns={getRateCols(onSuccessAdd)}
                disabled={true}
                sx={{mt: 1, ml: -1}}
                getRowClassName={(params) => {
                    switch(params.row.status) {
                        case 2:
                            return 'warning';
                        case 3:
                            return 'error';
                        default:
                            return "";
                    }
                }}

                initialState={{
                    sorting: {
                        sortModel: [
                            { field: 'status', sort: 'asc' },
                            { field: isKupacOnly() ? 'datumKupac': 'datumRadnik', sort: 'desc' },
                        ],
                    },
                    columns: {
                        columnVisibilityModel: {
                            iznosKupac: isKupacOnly(),
                            datumKupac: isKupacOnly(),
                            iznosRadnik: isAdminRadnik(),
                            datumRadnik: isAdminRadnik(),
                            uplata: isAdminRadnik(),
                        },
                    },
                }}

                autoHeight
                rowCount={pageState.total}
                loading={pageState.isLoading}

                pagination
                paginationModel={pageState.paginationModel}
                pageSizeOptions={[3, 5, 10, 25]}
                paginationMode="server"
                onPaginationModelChange={(paginationModel) => setPageState(old => ({ ...old, paginationModel }))}
            />
            <Snackbar open={openSnackbar} autoHideDuration={6000} onClose={handleCloseSnackbar} sx={{backgroundColor: "green"}}>
                <Alert onClose={handleCloseSnackbar} severity="success" variant="filled" sx={{ width: '100%', backgroundColor: "green" }}>
                    Uplata rate je uspesno evidentirana!
                </Alert>
            </Snackbar>
        </Box>
    );
}