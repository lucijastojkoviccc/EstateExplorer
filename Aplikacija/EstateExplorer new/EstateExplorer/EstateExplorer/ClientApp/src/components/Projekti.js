import React, { Component, useEffect, useState } from 'react';
//import slika1 from "../logoStrana.png";
//import slika2 from "../POCETNA2.jpg"
import Zgrada from "./Zgrada";
import AppRoutes from '../AppRoutes';
import { useNavigate } from 'react-router-dom';
import App from '../App';
import axios from 'axios';
import { isAdminRadnik } from '../services/UlogaSvc';





const Projekti = () => {




    const navigate = useNavigate();


    function clickHandler(event) {

    }
    const [zgrade, setZgrada] = useState([]);
    const [loadingZgrada, setLoadingZgrada] = useState(true);

    useEffect(() => {
        ref();
    }, []);

    function ref() {
        axios.get(`api/zgrade`)
            .then(p => setZgrada(p.data.zgrade))
            .catch(p => setZgrada(new Zgrada()))
            .finally(p => setLoadingZgrada(false)); 
    }


    function deleteZgrada(id) {
        fetch(`api/ObrisiZgradu/${id}`, {
            method: 'DELETE'
        }

        ).then((result) => {
            result.json().then((resp) => {
                console.warn();
                ref();
            }
            )
        })
    }


    return (
        <main>
            <h1 className="projekti">
                Dostupni projekti
                <br />
                <br />

                {isAdminRadnik() && <button className="btn btn-primary btn-lg" onClick={() => navigate('/dodajzgradu', { state: { id: null } })}>Dodaj novu zgradu</button>}
                <div className="card-collection">
                    {
                        zgrade.map(zgrada => {

                            return (
                                <div key={zgrada.id} className="m-2" style={{ cursor: "pointer" }} >
                                    {isAdminRadnik() && <button className="btn btn-primary btn-lg" onClick={() => navigate('/dodajzgradu', { state: { id: zgrada.id } })}>Edit</button>}
                                    {/*{isAdminRadnik() && <button className="btn btn-primary btn-lg" onClick={() => deleteZgrada(zgrada.id)}>Delete</button>}*/}

                                    {
                                        <div>
                                            <img className="slike" onClick={() => navigate('/zgrada/' + zgrada.id)} src="https://www.hidrogradnjapromet.rs/images/projects/ustanicka-buy.jpg" alt="" />
                                            <br />
                                            <div className="naziv-zgrade">{zgrada.naziv}</div>

                                            <h4 className="ulica-zgrade">{zgrada.adresa}</h4>

                                        </div>
                                    }


                                </div>
                            )
                        })
                    }
                </div>


            </h1>

        </main>
    );
};

export default Projekti;

