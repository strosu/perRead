import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Counter } from './components/Counter';
import { FetchData } from './components/FetchData';
import { FetchAuthors } from './components/FetchAuthors';
import { FetchArticles } from './components/FetchArticles';
import { Login } from './components/Login';
import { Logout } from './components/Logout';

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/counter' component={Counter} />
                <Route path='/fetch-data' component={FetchData} />
                <Route path='/fetch-authors' component={FetchAuthors} />
                {/*<Route path='/fetch-acrticles' component={FetchArticles} />*/}
                {/*<Route path='/login' component={Login} />*/}
                {/*<Route path='/logout' component={Logout} />*/}
            </Layout>
        );
    }
}
