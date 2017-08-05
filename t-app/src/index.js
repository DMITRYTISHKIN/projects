import React from 'react';
import ReactDOM from 'react-dom';
import { Router, Route, browserHistory } from 'react-router';
import { Provider } from 'react-redux';
import configureStore from './redux/store';
import { syncHistoryWithStore } from 'react-router-redux';
import App from './components/app';

import Record from './components/record/record';
import Dashboard from './components/dashboard/dashboard';
import Scheduler from './components/scheduler/scheduler';
import Ways from './components/ways/ways';
import Statistics from './components/statistics/statistics';
import Control from './components/control/control';

import 'icheck/skins/all.css';
import 'bootstrap/dist/css/bootstrap.css';
import './App.css';


const initialState = window.REDUX_INITIAL_STATE || {};
const store = configureStore(initialState);
const history = syncHistoryWithStore(browserHistory, store)


ReactDOM.render((
  <Provider store={store}>
    <Router history={browserHistory}>
      <Route path="/" component={App} >
        <Route path="/dashboard" component={Dashboard} />
        <Route path="/scheduler" component={Scheduler} />
        <Route path="/record" component={Record} />
        <Route path="/ways" component={Ways} />
        <Route path="/statistics" component={Statistics} />
        <Route path="/control" component={Control} />
      </Route>
    </Router>
  </Provider>
  ), document.getElementById('root')
);
