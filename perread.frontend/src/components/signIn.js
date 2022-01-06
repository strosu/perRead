import { Component } from "react";
import {
    NavBtn,
    NavBtnLink,
  } from './NavbarElements';

export class SignIn extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>Sign In Form HERE</div>
        );
        // return (
        //     <NavBtn>
        //         <NavBtnLink to='/sign-up'>Sign In</NavBtnLink>
        //     </NavBtn>);
    }
}