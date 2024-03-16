import axios from "axios";
import fileDownload from "js-file-download";
import { Navigate, useParams } from "react-router-dom";
import React, { Component, useEffect, useState } from 'react';
import Zgrada from "../models/Zgrada";
import AppRoutes from '../AppRoutes';
import { useNavigate } from 'react-router-dom';

import { useLocation } from "react-router-dom";
import { isKupac } from "../services/UlogaSvc";




const Stan = (props) => {


    const navigate = useNavigate();
    const [query, setQuery] = useState("");


    const { zgradaid } = useParams();




    const location = useLocation();
    console.log(location);
    const [zgrada, setZgrada] = useState(new Zgrada());


    const [loadingZgrada, setLoadingZgrada] = useState(true);
    const { state } = useLocation();
    const [loadintStan, setLoadingStan] = useState(true);

    useEffect(() => {
        let idz = zgradaid;
           

        axios.get(`http://localhost:3000/zgrade/${idz}`)
            .then(p => setZgrada(p.data))
            .catch(p => setZgrada(new Zgrada()))
            .finally(p => setLoadingZgrada(false));
    }, []);


    const downloadPDF=(e)=>{
        e.preventDefault()    
        axios({
            url: "https://localhost:44472",
            method: "GET",
            responsiveType:"blob"
        }).then((res) => {
            fileDownload(res.data, "UGOVOR.pdf")
        })
    }
    
    const zgradaNaziv = zgrada.naziv;
    const zgradaUlica = zgrada.ulica;
    const zgradaCena = location.state.cenaKv * location.state.povrsina;
    const brUlaza = location.state.brojUlaza;
    const sprat = location.state.sprat;
    const broj = location.state.broj;

    const url = "http://localhost:3000/info";
    const [data, setData] = useState({
        ime: "",
        prezime: "",
        maticniBroj: "",
        mesto: "",
        adresa: "",
        zgradaN: "",
        zgradaUlica: "",
        zgradaCena: "",
        brojUlaza: "",
        sprat: "",
        broj:""

    })

    function handle(e) {
        const newdata = { ...data };
        newdata[e.target.id] = e.target.value;
        setData(newdata);
        console.log(newdata);

    }

    function submit(e) {
        e.preventDefault();
        if (data.maticniBroj.length == 13) {
            axios.post(url, {
                ime: data.ime,
                prezime: data.prezime,
                maticniBroj: data.maticniBroj,
                mesto: data.mesto,
                adresa: data.adresa,
                zgradaN: zgradaNaziv,
                zgradaUlica: zgradaUlica,
                zgradaCena: zgradaCena,
                brojUlaza: brUlaza,
                sprat: sprat,
                broj: broj

            }).then(res => {
                console.log(res.data);
            })
        }
        else {
            alert("Nije validan maticni broj!!!");
        }
      
        

    }

    if(!isKupac()) {
        return (<Navigate to="/Identity/Pages/Account/Login" replace />);
    }

    return (
        <main className="glavni">
          <div className="levo">
            <h1 className="opis">Upisite vase podatke</h1>
            <form onSubmit={(e) => submit(e)}>
                <input required onChange={(e) => handle(e)} id="ime" value={data.ime} placeholder="Ime" type="text"></input>
               
                <input required onChange={(e) => handle(e)} id="prezime" value={data.prezime} placeholder="Prezime" type="text"></input>
               
                <input required onChange={(e) => handle(e)} id="maticniBroj" value={data.maticniBroj} placeholder="Maticni broj" type="text"></input>

                <input required onChange={(e) => handle(e)} id="mesto" value={data.mesto} placeholder="Mesto stanovanja" type="text"></input>

                <input required onChange={(e) => handle(e)} id="adresa" value={data.adresa} placeholder="Adresa" type="text"></input>

                

                <br/>
                    <button className="btn btn-primary btn-md" /*onClick={(e) => downloadPDF(e)}*/>Kreirajte ugovor</button>
                </form>
            </div>
            <div className="desno opis">
                <img className="slike" src={zgrada.slika} alt="" />
                <h2 >{zgrada.naziv}</h2>
                <h5>{zgrada.ulica}</h5>
                <div>
                    <h2>Stan</h2>
                    <h5>Povrsina:{location.state.povrsina}m2</h5>
                    <h5>Broj soba:{location.state.brojSoba}</h5>
                    <h5>Broj ulaza:{location.state.brojUlaza}</h5>
                    <h5>Sprat:{location.state.sprat}</h5>
                    <h5>Broj stana:{location.state.broj}</h5>
                    <hr />
                    <h1 className="UC">Ukupna cena: {location.state.cenaKv * location.state.povrsina}$</h1>

                </div>

                
            </div>
                


          





        </main>

    )
}



export default Stan;