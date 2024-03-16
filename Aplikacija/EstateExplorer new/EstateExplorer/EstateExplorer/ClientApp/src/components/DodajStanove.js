import * as React from 'react';
import { MenuItem, Select, Stack, Button, Box, Divider, CircularProgress, InputLabel, FormControl, } from '@mui/material';
import { Navigate, useLocation, useNavigate } from 'react-router-dom';
import Stan from '../models/Stan';
import { getStanColumns, changeGroupingOrder } from '../services/StanSvc';
import axios from 'axios';
import AddIcon from '@mui/icons-material/Add';
import DeleteIcon from '@mui/icons-material/Delete';
import ContentCopyOutlinedIcon from '@mui/icons-material/ContentCopyOutlined';
import { green } from '@mui/material/colors';
import { DataGridPremium } from '@mui/x-data-grid-premium';
import { isAdminRadnik } from "../services/UlogaSvc";

function round2Decimals(value) {
  let x = +value;
  x *= 100;
  x = Math.round(x);
  x /= 100;
  return x;
}

function Footer(props)
{
  const [submitingStanovi, setSubmitingStanovi] = React.useState(false);

  const navigate = useNavigate();

  const handleSubmit = () => {
      setSubmitingStanovi(true);
      console.log("saljem stanove");
      axios.put(`api/DodajStanoveUZgradu/${props.id}/stanovi`, props.stanovi.map(stan => ({
          broj: stan.broj,
          povrsina: stan.povrsina,
          brojListaNepokretnosti: stan.brojListaNepokretnosti,
          brojSoba: stan.brojSoba,
          cenaPoKvadratuBezPDV: stan.cenaKv,
          sprat: stan.sprat,
          brojUlaza: stan.brojUlaza,
          orijentacija: stan.orijentacija,
          opis: stan.opis
      })))
        .then(p => {
            console.log(p);
      if(p.status === axios.HttpStatusCode.Ok || p.status === axios.HttpStatusCode.Created) {
        navigate(-2);        
      }
      else {
        setSubmitingStanovi(false);
      }
    })
    .catch(p => {
      console.error(p);
      setSubmitingStanovi(false);
    });
  }

  const ukupnaCenaBezPDVa = props.cenaUk.reduce((acc, curr) => acc += curr, 0);

  return (
    <Box 
      sx={{flex: 1, }}
    >
      <Stack 
        sx={{ p: '10px', width:'fit-content', fontSize:'20px', textAlign: "right", float: 'right', }}   
      >
        <p>Ukupna cena bez PDV-a: {round2Decimals(ukupnaCenaBezPDVa)} €</p>
        <p>PDV: {round2Decimals(ukupnaCenaBezPDVa * 0.2)} €</p>
        <Divider sx={{ backgroundColor:'black', borderWidth: 3 }} />
        <p>Ukupna cena sa PDV-om: {round2Decimals(ukupnaCenaBezPDVa * 1.2)} €</p>

        <Box sx={{ display: 'flex', flexDirection: 'row-reverse', alignItems: 'center' }}>
          <Box sx={{ m: 1, position: 'relative', }}>
            <Button variant="contained" disabled={submitingStanovi} onClick={handleSubmit}>
                Pošalji
            </Button>
            {submitingStanovi && (
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
      </Stack>
    </Box>
  );
}

export default function DodajStanove() {
  let { state } = useLocation();
  //console.log(state);
  if(state.stanovi === null) {
      state.stanovi = [];

  }

    const [rows, setRows] = React.useState(() => state.stanovi.map(stan => ({
        id: stan.id,
        broj: stan.broj,
        povrsina: stan.povrsina,
        brojListaNepokretnosti: stan.brojListaNepokretnosti,
        brojSoba: stan.brojSoba,
        cenaKv: stan.cenaPoKvadratuBezPDV,
        sprat: stan.sprat,
        brojUlaza: stan.brojUlaza,
        orijentacija: stan.orijentacija,
        opis: stan.opis
    })));
    console.log(rows);
    const brojStana = React.useRef(Math.max(...(rows.map(p => p.broj))));
    const idStana = React.useRef(Math.max(...(rows.map(p => p.id))));
    if (rows.length === 0) {
        brojStana.current = 0;
        idStana.current = 0;
    }
  const[RSD4EUR, SetRSD4EUR] = React.useState(0);

  React.useEffect(() => {
    axios.get("api/VratiSrednjiKurs")
    .then(p => SetRSD4EUR(p.data.rsd4eur))
    .catch(p => console.error(p))
  }
  , []);

  const handleCellEditStop  = (params, event) => {
    if(event.target !== undefined && event.target.value !== undefined) {
      params.row[params.field] = event.target.value;
      setRows((prevRows) => {
        const arr = [...prevRows];
        for(let i = 0; i < arr.length; i++) {
          if(arr[i].id === params.row.id) {
            arr[i] = params.row;
            break;
          }
        }
        return arr;
      });
    }
  };



  function newRow(oldRow)
  {
    idStana.current++;
    brojStana.current++;
    return {
      ...(oldRow),
      id: idStana.current,
      broj: brojStana.current,
    }
  }

  const handleAddRow = () => {
    setRows((prevRows) => {
      return [...prevRows, newRow(new Stan())]});
  };

  const selectedRowsIndex = React.useRef([]);

  const handleDeleteRows = (event) => {
    setRows((prevRows) => prevRows.filter(row => !selectedRowsIndex.current.includes(row.id)));
    brojStana.current = Math.max(...(rows.map(p => p.broj)));
  };

  const handleCopyRows = (event) => {
    selectedRowsIndex.current.forEach(selectedRowIndex => {
      rows
      .filter(row => row.id === selectedRowIndex)
      .forEach(selectedRow => {
        setRows((prevRows) => [...prevRows, newRow(selectedRow)])
      })
    });
  }
  const [colGroups, SetColGroups] = React.useState(changeGroupingOrder(0));
  const [selLbl, SetSelLbl] = React.useState(0);

  const handleSelectChange = (event) => {
    //event.preventDefault();
    SetSelLbl(event.target.value);
    SetColGroups(changeGroupingOrder(event.target.value));
  }


  const columns = getStanColumns(RSD4EUR);

  if(!isAdminRadnik()) {
    return (<Navigate to="/Identity/Pages/Account/Login" replace />);
  }

  return (
    <Box sx={{ backgroundColor: 'white', width: '100%' }}>
      <Stack 
        direction='row'
        alignItems='stretch'
        justifyContent='space-evenly'//{{sm: "flex-start", sx:'center'}}
        flexWrap='wrap-reverse'
        spacing={0}
        sx={{ pt: 1 }}
      >
        <Button size='large' variant="contained" onClick={handleAddRow} startIcon={<AddIcon />} sx={{fontSize: 14, mb: 1, }}>
          Dodaj stan
        </Button>
        <Button size='large' variant="contained"  onClick={handleDeleteRows} startIcon={<DeleteIcon />} sx={{fontSize: 14, mb: 1, }}>
          Obrisi stanove
        </Button>
        <Button size='large' variant="contained"  onClick={handleCopyRows} startIcon={<ContentCopyOutlinedIcon />} sx={{fontSize: 14, mb: 1, }}>
          Kopiraj stanove
        </Button>
        <FormControl sx={{ mb: 1, }} size="small">
          <InputLabel id="demo-select-small-label">Grupisanje kolona</InputLabel>
          <Select
            labelId="demo-simple-select-label"
            id="demo-simple-select"
            value={selLbl}
            label="Redosled"
            onChange={handleSelectChange}
            sx={{fontSize: 16, minWidth: 100, p: 0,  }}
          >
            <MenuItem value="0" sx={{m: 0}}>Valuta, PDV, JM</MenuItem>
            <MenuItem value="1">Valuta, JM, PDV</MenuItem>
            <MenuItem value="2">JM, Valuta, PDV</MenuItem>
            <MenuItem value="3">JM, PDV, Valuta</MenuItem>
            <MenuItem value="4">PDV, Valuta, JM</MenuItem>
            <MenuItem value="5">PDV, JM, Valuta</MenuItem>
          </Select>
        </FormControl>
      </Stack>

      <Box sx={{ height: 'fit-content', mt: 1 }}>
        <DataGridPremium
          rows={rows} 
          columns={columns}
          onCellEditStop={handleCellEditStop}
          experimentalFeatures={{ columnGrouping: true }}
          columnGroupingModel={colGroups}
          checkboxSelection 
          onRowSelectionModelChange={
            (p) => {selectedRowsIndex.current = p}
          }
          slots={{
            footer: Footer,
          }}
          slotProps={{
            footer: { 
                  cenaUk: rows.map(p => p.cenaKv * p.povrsina),
                  id: state.id,
                  stanovi: rows,
            },
          }}
          initialState={{
            aggregation: {
              model: {
                povrsina: 'sum',
                cenaKv: 'avg',
                cenaKvPDV: 'avg',
                cenaUk: 'sum',
                cenaUkPDV: 'sum',
                cenaKvRSD: 'avg',
                cenaKvPDVRSD: 'avg',
                cenaUkRSD: 'sum',
                cenaUkPDVRSD: 'sum',
              },
            },
          }}
        />
      </Box>
    </Box>
  );
}