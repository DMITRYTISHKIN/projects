// src/actions/sessionActions.js

import recordApi from '../api/recordApi';

export function delRecord() {
  return function(dispatch) {
    return recordApi.delRecord()
    .then((response) => {
      if(response.ok) {
        return response.json();
      }
      throw new Error('error');
    })
    .then((response) => {
      dispatch({type: 'RECORD_DEL', data: response.data});
    })
    .catch((error) => {
      console.error(error);
    });
  };
}

export function setRecord(data) {
  return function(dispatch) {
    return recordApi.setRecord(data)
    .then((response) => {
      if(response.ok) {
        return response.json();
      }
      throw new Error('error');
    })
    .then((response) => {
      dispatch({type: 'RECORD_SET', data: response.data});
    })
    .catch((error) => {
      console.error(error);
    });
  };
}

export function getRecord() {
  return function(dispatch) {
    return recordApi.getRecord()
    .then((response) => {
      if(response.ok) {
        return response.json();
      }
      throw new Error('error');
    })
    .then((response) => {
      dispatch({type: 'RECORD_GET', data: response.data});
    })
    .catch((error) => {
      console.error(error);
    });
  };
}

export function getWorkersForSelectSuccess(events) {
  return {type: 'RECORD_EVENT_GET', events: events}
}
export function getWorkersForSelect() {
  return function(dispatch) {
    return recordApi.getWorkers()
    .then((response) => {
      if(response.ok) {
        return response.json();
      }
      throw new Error('error');
    })
    .then((response) => {
      dispatch(getWorkersForSelectSuccess(response.data));
    })
    .catch((error) => {
      console.error(error);
    });
  };
}
