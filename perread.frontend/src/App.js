import React from 'react';
import './App.css';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import {Home} from './components/Home'
import {NavBar} from './components/NavBar'
import {About} from './components/about'
import {SignUp} from './components/signup'
import {Login} from './components/login'
import {Authors} from './components/authors'
import {Articles} from './components/articles'
import {NewArticle} from './components/newArticle'
import {useRenderArticle} from './components/article'

function App() {
  return (
    <Router>
      <NavBar />
      <Switch>
        <Route path='/' exact component={Home} />
        <Route path='/about' component={About} />
        <Route path='/signup' component={SignUp} />
        <Route path='/login' component={Login} />
        <Route path='/authors' component={Authors} />
        <Route path='/articles' component={Articles} />
        <Route path='/article/new' component={NewArticle} />
        <Route path="/article/:id" component={useRenderArticle}/>
        {/* <Route path='/articles' component={SignUp} /> */}
      </Switch>
    </Router>
  );
}

export default App;