import React, { Component } from 'react';



const Footer = () => {
    return (
        <div className="footer">
            <div class-name="sb__footer section__padding">
                <div className="sb__footer-links">
                    <div className="sb__footer-links">
                        <div className="sb__footer-links-div">
                            <h4>EstateExplorer</h4>
                            <p>EstateExplorer je gradjevinska firma koja svoje projekte
                            radi po najvisim standardima, koristeci
                            najkvalitetnije materijale. Za razliku od drugih, nama
                            je osnovni cilj da kupci budu zadovoljni.</p>
                        </div>
                        <div className="sb__footer-links-div">
                            <h4>Kontakt</h4>
                            <p>Broj telefona: 018 776 116</p>

                            <p>Email: nis70gt@gmail.com</p>

                            <p>Lokacija: Kralja Vukasina 5</p>


                        </div>
                        <div className = "sb__footer-links-div">
                            <h4>Partneri</h4>
                            <a href="https://www.maxi.rs/">
                                <p>Maxi</p>
                            </a>
                            <a href="https://cdn1.efbet.rs/efbet/landings/sport/?gclid=Cj0KCQjwgLOiBhC7ARIsAIeetVDuFbLJVF0AdqJTylq_jOozzCb3SnbxttC5HtwLvpf-dOBoz5pB0swaAlFnEALw_wcB">
                                <p>Admiral Bet</p>
                            </a>
                            <a href="https://www.bancaintesa.rs/">
                                <p>Banka Intesa</p>
                            </a>

                        </div>

                    </div>
                </div>
            </div>

            <hr></hr>

            <div className="sb__footer-below">
                <div className="sb__footer-copyright">
                    <p>
                        @{new Date().getFullYear() } ALFAsoft. All right reserved
                    </p>
                </div>

            </div>






        </div>


       

   
        )

}

export default Footer;