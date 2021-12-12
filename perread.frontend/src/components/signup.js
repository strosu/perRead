import React, { Component } from 'react';
  
export class SignUp extends Component{
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <div
        style={{
          display: 'flex',
          justifyContent: 'Right',
          alignItems: 'Right',
          height: '100vh'
        }}
      >
        <h1>Sign Up</h1>
      </div>
    );
  }
}