import React, { Component } from 'react';
import {
  Nav,
  NavLink,
  Bars,
  NavMenu,
  NavBtn,
  NavBtnLink,
} from './NavbarElements';
  
export class NavBar extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <>
        <Nav>
          <Bars />
          <NavMenu>
            <NavLink to='/' activeStyle>
              Home
            </NavLink>
          </NavMenu>
          <NavMenu>
            <NavLink to='/about' activeStyle>
              About
            </NavLink>
            <NavLink to='/sign-up' activeStyle>
              Sign Up
            </NavLink>
            <NavLink to='/authors' activeStyle>
              Authors
            </NavLink>
            {/* Second Nav */}
            {/* <NavBtnLink to='/sign-in'>Sign In</NavBtnLink> */}
          </NavMenu>
          <NavBtn>
            <NavBtnLink to='/signin'>Sign In</NavBtnLink>
          </NavBtn>
        </Nav>
      </>
    );
  }
}