import * as React from 'react';
import { useParams, Navigate } from 'react-router-dom';
import { setUloga } from '../services/UlogaSvc';

export default function SetRole() {

    let { uloga } = useParams();
    if (uloga == "remove") {
        uloga = "";
    }
    setUloga(uloga);
    return (<Navigate to="/" replace />);
}
