import { applyMiddleware, combineReducers, createStore } from 'redux';
import thunk from 'redux-thunk';
import counterReducer from './counter/counterReducer';
import schedulerReducer from './scheduler/schedulerReducer';
import schedulerEventsReducer from './scheduler/schedulerEventsReducer';
import recordReducer from './record/recordReducer';
import dashboardReducer from './dashboard/dashboardReducer';
import waysReducer from './ways/waysReducer';
import controlReducer from './control/controlReducer';
import statisticsReducer from './statistics/statisticsReducer';

import modalReducer from './common/modalReducer';
import sessionReducer from './sessionReducer';
import {routerReducer} from 'react-router-redux';
//import authReducer from './header/auth/authReducer';



export default function (initialState = {}) {
  const appReducer = combineReducers({
    routing: routerReducer,
    counter: counterReducer,
    auth: modalReducer,
    session: sessionReducer,
    scheduler: schedulerReducer,
    schedulerEvents: schedulerEventsReducer,
    record: recordReducer,
    dashboard: dashboardReducer,
    ways: waysReducer,
    control: controlReducer,
    statistics: statisticsReducer
  })

  const rootReducer = (state, action) => {
    if (action.type === 'LOG_OUT') {
      state = undefined
    }

    return appReducer(state, action)
  }

  return createStore(rootReducer, initialState, applyMiddleware(thunk));
}
