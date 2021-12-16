import { Component } from "react";
import {
    NavBtn,
    NavBtnLink,
  } from './NavbarElements';

export class Sign extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <NavBtn>
                <NavBtnLink to='/sign-up'>Sign In</NavBtnLink>
            </NavBtn>);
    }
}