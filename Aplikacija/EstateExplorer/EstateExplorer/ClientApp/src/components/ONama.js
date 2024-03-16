import React, { Component, useState, useEffect } from 'react';
import { FaFacebook, FaInstagram, FaGithub } from "react-icons/fa"


const ONama=()=>{



    return (

        <main className="opis">

            <h1 className="o-nama-naslov">Dobrodosli na EstateExplorer!</h1>
            <hr/>   
            <h5 className="o-nama-tekst">
                <div>Naša priča počinje kada smo osnovali EstateExplorer sa ciljem da pojednostavimo proces pronalaženja savršene nekretnine. Od svog osnivanja, posvećeni smo pružanju visokokvalitetnih usluga i stvaranju izuzetnog iskustva za naše korisnike. Sa strašću za nekretnine i tehnologiju, postali smo lider u industriji i obezbedili smo širok spektar nekretnina širom sveta.</div>
                <br/>
                <h3 style={{ font: 'bold', color: '#D3D3D3' }}>Šta radimo?</h3>
                <hr className="hrP" />
                <div>EstateExplorer je vaš ključ za otključavanje vrata savršene nekretnine. Kao portal za pronalaženje i prodaju nekretnina, pružamo vam mogućnost da istražite hiljade nekretnina na najatraktivnijim lokacijama. Bez obzira da li tražite kuću, stan, poslovni prostor ili investicionu priliku, naša platforma vam omogućava da pregledate, filtrirate i prilagodite pretragu kako bi pronašli idealno rešenje za vaše potrebe.</div>

                <br />
                <h3 style={{ font: 'bold', color: '#D3D3D3' }}> Naši principi:</h3>
                <hr className="hrP" />

                <div>1. Kvalitet: U EstateExplorer-u, kvalitet je naša najviša vrednost. Svaka nekretnina koju prikazujemo na našoj platformi pažljivo je odabrana i prolazi kroz rigorozan proces procene. Na taj način vam obezbeđujemo samo vrhunske nekretnine koje zadovoljavaju najviše standarde.</div>
                 <br/>
                <div>2. Poverenje: Izgradnja poverenja sa našim korisnicima je od suštinske važnosti za nas. Verujemo da je transparentnost ključna za uspešno iskustvo u pronalaženju i prodaji nekretnina. Zato pružamo tačne informacije, profesionalnu podršku i otvoren dijalog kako bismo vam pomogli da donesete informisane odluke.</div>
                <br />
                <div>3. Personalizacija: Svesni smo da je svaka potraga za nekretninom jedinstvena. Zato pružamo personalizovane usluge kako bismo se prilagodili vašim specifičnim zahtevima. Naša platforma vam omogućava da prilagodite pretragu prema vašim preferencijama, kao što su cena, lokacija, veličina i mnogi drugi faktori.</div>
                <br />
                <br />
                <div>Još nešto što može biti korisno na ovoj stranici je informacija o našem timu stručnjaka koji su posvećeni da vam pomognu u svakom koraku. Naša vizija je da postanemo vaš pouzdan partner u svetu nekretnina i da vam pružimo najbolje iskustvo tokom celog procesa.</div>
                <div className="tim ">
                    <div className="programer">
                        <img src="user.png" ></img>
                        <h3>Filip Vidojkovic</h3>
                        <div className="info">
                            <p>Front-end developer</p>
                            <p>On je najveci car na svetu</p>
                        </div>
                        <div className="social">
                           

                            <a href="#" className="insta">
                                <FaInstagram/>
                            </a>

                            <a href="#" className="git">
                                <FaGithub />
                            </a>
                       
                            <a href="#" className="face">
                                <FaFacebook />

                            </a>                          
                        </div>
                    </div>
                    <div className="programer">
                        <img src="user.png" ></img>
                        <h3>Lucija Stojkovic</h3>
                        <div className="info">
                            <p>Back-end developer</p>
                            <p>On je najveci car na svetu</p>
                        </div>
                        <div className="social">


                            <a href="#" className="insta">
                                <FaInstagram />
                            </a>

                            <a href="#" className="git">
                                <FaGithub />
                            </a>

                            <a href="#" className="face">
                                <FaFacebook />

                            </a>
                        </div>




                    </div>

                    <div className="programer">
                        <img src="user.png" ></img>
                        <h3>Anastasija Tasic</h3>
                        <div className="info">
                            <p>Back-end developer</p>
                            <p>On je najveci car na svetu</p>
                        </div>
                        <div className="social">


                            <a href="#" className="insta">
                                <FaInstagram />
                            </a>

                            <a href="#" className="git">
                                <FaGithub />
                            </a>

                            <a href="#" className="face">
                                <FaFacebook />

                            </a>
                        </div>


                    </div>

                    <div className="programer">
                        <img src="user.png" ></img>
                        <h3>Aleksa Jovanovic</h3>
                        <div className="info">
                            <p>Front-end developer</p>
                            <p>On je najveci car na svetu</p>
                        </div>
                        <div className="social">


                            <a href="#" className="insta">
                                <FaInstagram />
                            </a>

                            <a href="#" className="git">
                                <FaGithub />
                            </a>

                            <a href="#" className="face">
                                <FaFacebook />

                            </a>
                        </div>


                    </div>

                   
                </div>
            </h5>

        </main>
    )
};


export default ONama;



