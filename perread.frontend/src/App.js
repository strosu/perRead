import React from 'react';
import './App.css';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import {Home} from './components/Home'
import {NavBar} from './components/NavBar'
import {About} from './components/about'
import {SignUp} from './components/signup'
import {Authors} from './components/authors'
import {Articles} from './components/articles'

function App() {
  return (
    <Router>
      <NavBar />
      <Switch>
        <Route path='/' exact component={Home} />
        <Route path='/about' component={About} />
        <Route path='/sign-up' component={SignUp} />
        <Route path='/authors' component={Authors} />
        <Route path='/articles' component={Articles} />
        {/* <Route path='/articles' component={SignUp} /> */}
      </Switch>
    </Router>
  );
}
  
export default App;