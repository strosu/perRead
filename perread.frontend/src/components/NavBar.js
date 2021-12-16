import React, { Component } from 'react';
import {
  Nav,
  NavLink,
  Bars,
  NavMenu,
  NavBtn,
  NavBtnLink,
} from './NavbarElements';
import {Sign} from './sign'
  
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
            <NavLink to='/authors' activeStyle>
              Authors
            </NavLink>
            <NavLink to='/articles' activeStyle>
              Articles
            </NavLink>            
            {/* Second Nav */}
            {/* <NavBtnLink to='/sign-in'>Sign In</NavBtnLink> */}
          </NavMenu>
          <Sign/>
        </Nav>
      </>
    );
  }
}