import React, { Component, useState, useEffect } from 'react';
import { FaFacebook, FaInstagram, FaGithub } from "react-icons/fa"




const ONama = () => {





    return (



        <main className="opis">



            <h1 className="o-nama-naslov">Dobrodosli na EstateExplorer!</h1>
            <hr />
            <h5 className="o-nama-tekst">
                <div>Nasa prica pocinje kada smo osnovali EstateExplorer sa ciljem da pojednostavimo proces pronalazenja savrsene nekretnine. Od svog osnivanja, posveceni smo pruzanju visokokvalitetnih usluga i stvaranju izuzetnog iskustva za nase korisnike. Sa strascu za nekretnine i tehnologiju, postali smo lider u industriji i obezbedili smo sirok spektar nekretnina sirom sveta.</div>
                <br />
                <h3 style={{ font: 'bold', color: '#D3D3D3' }}>sta radimo?</h3>
                <hr className="hrP" />
                <div>EstateExplorer je vas kljuc za otkljucavanje vrata savrsene nekretnine. Kao portal za pronalazenje i prodaju nekretnina, pruzamo vam mogucnost da istrazite hiljade nekretnina na najatraktivnijim lokacijama. Bez obzira da li trazite kucu, stan, poslovni prostor ili investicionu priliku, nasa platforma vam omogucava da pregledate, filtrirate i prilagodite pretragu kako bi pronasli idealno resenje za vase potrebe.</div>



                <br />
                <h3 style={{ font: 'bold', color: '#D3D3D3' }}> Nasi principi:</h3>
                <hr className="hrP" />



                <div>1. Kvalitet: U EstateExplorer-u, kvalitet je nasa najvisa vrednost. Svaka nekretnina koju prikazujemo na nasoj platformi pazljivo je odabrana i prolazi kroz rigorozan proces procene. Na taj nacin vam obezbeđujemo samo vrhunske nekretnine koje zadovoljavaju najvise standarde.</div>
                <br />
                <div>2. Poverenje: Izgradnja poverenja sa nasim korisnicima je od sustinske vaznosti za nas. Verujemo da je transparentnost kljucna za uspesno iskustvo u pronalazenju i prodaji nekretnina. Zato pruzamo tacne informacije, profesionalnu podrsku i otvoren dijalog kako bismo vam pomogli da donesete informisane odluke.</div>
                <br />
                <div>3. Personalizacija: Svesni smo da je svaka potraga za nekretninom jedinstvena. Zato pruzamo personalizovane usluge kako bismo se prilagodili vasim specificnim zahtevima. Nasa platforma vam omogucava da prilagodite pretragu prema vasim preferencijama, kao sto su cena, lokacija, velicina i mnogi drugi faktori.</div>
                <br />
                <br />
                <div>Jos nesto sto moze biti korisno na ovoj stranici je informacija o nasem timu strucnjaka koji su posveceni da vam pomognu u svakom koraku. Nasa vizija je da postanemo vas pouzdan partner u svetu nekretnina i da vam pruzimo najbolje iskustvo tokom celog procesa.</div>
                <div className="tim ">
                    <div className="programer">
                        <img src="user.png" ></img>
                        <h3>Filip Vidojkovic</h3>
                        <div className="info">
                            <p>Front-end developer</p>
                            <p>Odliccan developer</p>
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
                        <h3>Lucija Stojkovic</h3>
                        <div className="info">
                            <p>Back-end developer</p>
                            <p>Odlicno barata sa bazom</p>
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
                        <h3>Anastasija Vasic</h3>
                        <div className="info">
                            <p>Back-end developer</p>
                            <p>Super se snalazi sa svim</p>
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
                            <p>Odlicno programira</p>
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

