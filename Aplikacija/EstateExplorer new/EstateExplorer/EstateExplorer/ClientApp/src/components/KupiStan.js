import { useParams } from "react-router-dom";
import React, { Component, useEffect, useState } from 'react';
import Zgrada from "../models/Zgrada";
import AppRoutes from '../AppRoutes';
import { useNavigate } from 'react-router-dom';
import App from '../App';
import axios from 'axios';
import { useLocation } from "react-router-dom";
import { isKupac } from "../services/UlogaSvc";






const KupiStan = () => {




    const navigate = useNavigate();
    const [query, setQuery] = useState("");
    const [povrsina, setPov] = useState("");
    const [cena, setCena] = useState("");

    const { zgradaid } = useParams();




    const [zgrada, setZgrada] = useState(new Zgrada());




    const [loadingZgrada, setLoadingZgrada] = useState(true);
    const { state } = useLocation();
    const [loadintStan, setLoadingStan] = useState(true);



    useEffect(() => {
        let idz = zgradaid;



        axios.get(`api/GetZgradePoParametruPretrage?searchParam=${idz}`)
            .then(p => setZgrada(p.data.zgrade[0]))
            .catch(p => setZgrada(new Zgrada()))
            .finally(p => setLoadingZgrada(false));
    }, []);










    function brSoba(broj) {





        if (broj == 2) {



            return (
                <div className="stanovi">
                    {<img className="slike" src="https://www.hidrogradnjapromet.rs/images/2room.png" />}
                    <div>Dvosoban stan</div>
                </div>
            )




        }
        else if (broj == 3) {
            return (
                <div className="stanovi">
                    {<img className="slike" src="https://www.hidrogradnjapromet.rs/images/3room.png" />}
                    <div>Trosoban stan</div>
                </div>
            )



        }
        else if (broj == 4) {
            return (
                <div className="stanovi">
                    {<img className="slike" src="https://www.hidrogradnjapromet.rs/images/4room.png" />}
                    <div>Cetvorosoban stan</div>
                </div>
            )



        }
        else if (broj == 5) {
            return (
                <div className="stanovi">
                    {<img className="slike" src="https://www.hidrogradnjapromet.rs/images/5room.png" />}
                    <div>Petosoban stan</div>
                </div>



            )
        }
    }





    return (



        <div className="kupi-stan">

            <h1>{zgrada.naziv}</h1>
            <h4>{zgrada.ulica}</h4>
            <br />
            <hr />
            <div className="Pretrazi">
                <h3>Pretrazivac</h3>
                <h5>Broj soba</h5>
                <div className="filter-sobe" >
                    <select onChange={(e) => setQuery(e.target.value)}>
                        <option value="">Sve</option>
                        {Array.from(
                            new Set(zgrada.stanovi.map((z) => z.brojSoba))
                        ).map((bSoba) => (
                            <option key={bSoba}>{bSoba}</option>
                        ))}
                    </select>
                </div>

                <h5>Povrsina</h5>
                <div className="filter-povrsina" >
                    <select onChange={(e) => setPov(e.target.value)}>
                        <option value="">Sve</option>
                        {Array.from(
                            new Set(zgrada.stanovi.map((z) => z.povrsina))
                        ).map((Povrsina) => (
                            <option key={Povrsina}>{Povrsina}</option>
                        ))}
                    </select>

                </div>

                <h5>Cena po kvadratu</h5>
                <div className="filter-cena" >
                    <select onChange={(e) => setCena(e.target.value)}>
                        <option value="">Sve</option>
                        {Array.from(
                            new Set(zgrada.stanovi.map((z) => z.cenaPoKvadratuBezPDV))
                        ).map((c) => (
                            <option key={c}>{c}</option>
                        ))}
                    </select>



                </div>




            </div>
            <hr />




            <br />
            <div className="Stanovi">
                <h2>Stanovi</h2>
                <div className="card-collection">



                    {zgrada.stanovi.filter((stan) => stan.brojSoba.toString().includes(query) && stan.povrsina.toString().includes(povrsina) && stan.cenaPoKvadratuBezPDV.toString().includes(cena)).map(stan => (
                        <div key={stan.id}>

                            <div className="m-1">
                                <h5 className="kupi-stan">{brSoba(stan.brojSoba)}</h5>
                                <div className="opis-stanova">
                                    <div>Povrsina: {stan.povrsina}m2</div>
                                    <div>Sprat: {stan.sprat}</div>
                                    <div>Orijentacija: {stan.orijentacija}</div>
                                    <div>Broj ulaza: {stan.brojUlaza}</div>
                                    <div>{stan.opis}</div>
                                    {isKupac() && <button className="btn btn-primary btn-md" onClick={() => navigate('/kupiStan/' + zgrada.id + "/" + stan.id, {
                                        state: stan
                                    })}>{stan.cenaPoKvadratuBezPDV} EUR</button>}
                                </div>
                            </div>



                        </div>

                    ))}
                </div>
            </div>
        </div>
    );






};



export default KupiStan;  