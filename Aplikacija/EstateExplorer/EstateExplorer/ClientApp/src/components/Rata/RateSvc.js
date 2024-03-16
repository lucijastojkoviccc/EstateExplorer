import axios from "axios";
import moment from "moment/moment";
import 'moment/locale/sr';
import { GridActionsCellItem } from "@mui/x-data-grid-pro";
import { UplataRate } from "./UplataRate";
import { isAdminRadnik, isInvestitor } from "../../services/UlogaSvc";

export default function getRateCols(onSuccessAdd)
{
    return [
        { field: 'iznosRadnik', headerName: 'Iznos', type: 'number', width: 75, visible: false, },
        { field: 'iznosKupac', headerName: 'Iznos', type: 'number', width: 75, },
    
        { field: 'valuta', headerName: 'Valuta', type: 'text', width: 50, },
        { field: 'kes', headerName: 'Kes/Ostalo', type: 'text', width: 50, },
    
        { field: 'datumKupac', headerName: 'Datum', type: 'date', align: 'right', flex: 1, maxWidth: 200,
            valueFormatter: params => moment(params.value).format('LLL'),
        },
        { field: 'datumRadnik', headerName: 'Datum', type: 'date', align: 'right', flex: 1, maxWidth: 200,
            valueFormatter: params => moment(params.value).format('LLL'),
        },
    
        { field: 'status', headerName: 'Status', type: 'text', flex: 1,
            valueGetter: (params) => {
                switch(params.value) {
                    case 1:
                        return "Uspesno";
                    case 2:
                        return "Ceka se evidencija";
                    case 3:
                        return "Pokrenut spor";
                    default:
                        return "Nepoznat";
                }
            },
            sortComparator: (v1, v2) => {
                if(v1 === "Uspesno") {
                    return 1;
                }
                else if(v2 === "Uspesno") {
                    return -1;
                }
                else {
                    if(v1 === "Ceka se evidencija") {
                        return 1;
                    }
                    else if(v2 === "Ceka se evidencija") {
                        return -1;
                    }
                }
                return 0;
            }
        },
    
        { field: 'uplata', headerName: 'Spor', type: 'string', minWidth: 150,
            renderCell: (props) => {
                return props.row.status === 3 && isInvestitor() ? 
                <UplataRate id={props.row.id} onSuccessAdd={onSuccessAdd} /> : 
                (props.row.status === 2 && isAdminRadnik() ? <UplataRate id={props.row.id} onSuccessAdd={onSuccessAdd} /> : <></>);
            }
        },
    ];
}