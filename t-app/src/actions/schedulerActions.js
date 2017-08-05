// src/actions/sessionActions.js

import * as types from './actionTypes';
import schedulerApi from '../api/schedulerApi';
import cookie from 'react-cookie';
import { browserHistory } from 'react-router';

export function addWorkEventSuccess(events) {
  return {type: 'SCHEDULER_EVENT_ADD', events: events}
}

export function addWorkEvent(data) {
  return function(dispatch) {
    return schedulerApi.add(data)
    .then((response) => {
      if(response.ok) {
        return response.json();
      }
      throw new Error('error');
    })
    .then((response) => {
      dispatch(addWorkEventSuccess(response.data));
    })
    .catch((error) => {
      console.error(error);
    });
  };
}

export function changeWorkEvent(data, newData) {
  return function(dispatch) {
    return schedulerApi.change(data)
    .then((response) => {
      if(response.ok) {
        return response.json();
      }
      throw new Error('error');
    })
    .then((response) => {
      dispatch(addWorkEventSuccess(response.data));
    })
    .catch((error) => {
      console.error(error);
    });
  };
}

export function getWorkEventSuccess(events) {
  return {type: 'SCHEDULER_EVENT_GET', events: events}
}

export function getWorkEvent(data) {
  return function(dispatch) {
    return schedulerApi.getEvents(data)
    .then((response) => {
      if(response.ok) {
        return response.json();
      }
      throw new Error('error');
    })
    .then((response) => {
      dispatch(getWorkEventSuccess(response.data));
    })
    .catch((error) => {
      console.error(error);
    });
  };
}
