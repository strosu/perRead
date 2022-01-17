import React, { Component } from 'react';
  
export class About extends Component{
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
        <h1>GeeksforGeeks is a Computer Science portal for geeks.</h1>
      </div>
    );
  }
}