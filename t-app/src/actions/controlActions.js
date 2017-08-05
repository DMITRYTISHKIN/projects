import controlApi from '../api/controlApi';

export function getControl() {
  return function(dispatch) {
    return controlApi.getControl()
    .then((response) => {
      if(response.ok) {
        return response.json();
      }
      throw new Error('error');
    })
    .then((response) => {
      dispatch({type: 'GET_DATA_CONTROL', users: response.data});
    })
    .catch((error) => {
      console.error(error);
    });
  };
}
