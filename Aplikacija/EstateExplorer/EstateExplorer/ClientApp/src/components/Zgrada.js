import React, { Component, useState, useEffect } from 'react';
import { useParams } from "react-router-dom";
import Projekti from './Projekti';
import axios from 'axios';
import { useLocation} from "react-router-dom";
import { useNavigate } from 'react-router-dom';







const Zgrada = () => {


    const navigate = useNavigate();
    const { zgradaid } = useParams();
    //const p = zgrade.zgrade.find((zgrada) => zgrada.id == zgradaid);
    const [zgrade, setZgrada] = useState([]);
    const [loadingZgrada, setLoadingZgrada] = useState(true);
    const { state } = useLocation();

    useEffect(() => {
        let id = zgradaid;

        axios.get(`http://localhost:3000/zgrade/${id}`)
            .then(p => setZgrada(p.data))
            .catch(p => setZgrada(new Zgrada()))
            .finally(p => setLoadingZgrada(false));
    }, []);

    function lift(){
       return (zgrade.lift == true) ? <h5>Lift: Ima</h5> : <h5>Lift: Nema</h5>
    }


    
    return (

        <main>

            <div>
                <h1 className="ime-zgrade-zg">{zgrade.naziv}</h1>
                
                <br />
                <h1 className="opis-zgrade-zg">Opis zgrade</h1>
                <hr className="hr-opis" />
                <div className="opis">
                    <h5>{zgrade.opis}</h5>
                </div>
                <br />

                <div className="dodatna-info">
                    <h4>Dodatne informacije o zgradi</h4>
                    <hr className="hr2"/>
                    <div>{lift()}</div>
                    <br />
                    <h5>Broj zgrade: {zgrade.brojZgrade}</h5>
                    <br />
                    <h5>Grejanje: {zgrade.grejanje}</h5>
                    <br />
                    <h5>Katastarska opstina: {zgrade.katastarskaOpstina }</h5>
                </div>
                <hr />

                <div className="card-collection">

                    <div className="m-1 box" style={{ cursor: "pointer" }} >
                        <a href={zgrade.lokacija}>

                            <img src="https://www.hidrogradnjapromet.rs/images/location.jpg" alt="" height={200} width={300} />
                        <h2 className="lokacija-text">Lokacija</h2>
                        </a>
                    </div>
                    

                    <div className="m-1 box" onClick={() => navigate('/kupiStan/' + zgrade.id)} style={{ cursor: "pointer" }}>
                            <img src="https://www.hidrogradnjapromet.rs/images/buyer.jpg" alt="" height={200} width={300} />
                            <h2 className="kupi-text">Kupi stan</h2>
                        </div>
                        
                    </div>
                

            </div>
        </main>
    );
};

export default Zgrada;

