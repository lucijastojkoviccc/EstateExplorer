import React, { Component } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { LoginMenu } from './api-authorization/LoginMenu';
import './NavMenu.css';

export class NavMenu extends Component {
  static displayName = NavMenu.name;
   
  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }
  logoStranaURL = "./Slike/logoStrana.png";
  render() {
    return (
      <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white" container light>
                <NavbarBrand tag={Link} to="/"><img src={this.logoStranaURL} style={{ width: 90 }} className="logo bg-transparent" /></NavbarBrand>
          <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
          <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                    <ul className="navbar-nav flex-grow">
              <NavItem className="nav">
              <strong>
                <NavLink tag={Link} className="text-light" to="/">Pocetna</NavLink>
              </strong>
              </NavItem>
              <NavItem className="nav">
              <strong>
                <NavLink tag={Link} className="text-light" to="/projekti">Projekti</NavLink>
              </strong>
              </NavItem>
              <NavItem className="nav">
              <strong>
                <NavLink tag={Link} className="text-light" to="/rate">Rate</NavLink>
              </strong>
              </NavItem>
              <NavItem className="nav">
              <strong>
                <NavLink tag={Link} className="text-light" to="/fetch-data">Galerija</NavLink>
              </strong>
              </NavItem>
             
              <LoginMenu>
              </LoginMenu>
            </ul>
          </Collapse>
        </Navbar>
      </header>
    );
  }
}
