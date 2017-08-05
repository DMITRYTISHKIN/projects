import * as types from '../actions/actionTypes';
import { push } from 'react-router-redux'
import cookie from "react-cookie";

const initialState = {
  session: !!cookie.load('jwt'),
  user: {}
}

export default function sessionReducer(state = initialState, action) {
  switch(action.type) {
    case types.LOG_IN_SUCCESS:
      return { session: !!cookie.load('jwt'), user: action.user };
    case types.LOG_IN_CHECK_SUCCESS:
      return { session: true, user: action.user };
    case types.LOG_IN_CHECK_ERROR:
      return { session: false, user: {} };
    case types.LOG_OUT:
      return { session: !!cookie.load('jwt'), user: {} };
    default:
      return state;
  }
}
