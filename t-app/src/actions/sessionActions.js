// src/actions/sessionActions.js

import * as types from './actionTypes';
import sessionApi from '../api/sessionApi';
import cookie from 'react-cookie';
import { browserHistory } from 'react-router';

export function loginSuccess(user) {
  browserHistory.push("dashboard");
  return {type: types.LOG_IN_SUCCESS, user: user}
}

export function logInUser(credentials) {
  return function(dispatch) {
    return sessionApi.login(credentials)
    .then((response) => {
      if(response.ok) {
        return response.json();
      }
      throw new Error('Authentication is error');
    })
    .then((response) => {
      cookie.save('jwt', response.token, { path: '/' });
      dispatch(loginSuccess(response.user));
    })
    .catch((error) => {
      console.error(error);
    });
  };
}

export function registerUser(credentials) {
  return function(dispatch) {
    return sessionApi.register(credentials)
    .then((response) => {
      if(response.ok) {
        return response.json();
      }
      throw new Error('Registration is error');
    })
    .then((response) => {
      console.log("register true")
    })
    .catch((error) => {
      console.error(error);
    });
  };
}

export function checkUserSuccess(user) {
  return { type: types.LOG_IN_CHECK_SUCCESS, user: user }
}
export function checkUserError(user) {
  cookie.remove('jwt', { path: '/' });
  browserHistory.push("/");
  return { type: types.LOG_IN_CHECK_ERROR }
}

export function checkUser() {
  return function(dispatch) {
    return sessionApi.check()
    .then((response) => {
      if(response.ok) {
        return response.json();
      }
      throw new Error('Check is error');
    })
    .then((response) => {
      dispatch(checkUserSuccess(response.data))
    })
    .catch((error) => {
      dispatch(checkUserError())
    });
  };
}

export function logOutUser() {
  browserHistory.push("/");
  cookie.remove('jwt', { path: '/' });
  return {type: types.LOG_OUT}
}
