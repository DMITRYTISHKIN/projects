import waysApi from '../api/waysApi';

export function getWays() {
  return function(dispatch) {
    return waysApi.getWays()
    .then((response) => {
      if(response.ok) {
        return response.json();
      }
      throw new Error('error');
    })
    .then((response) => {
      dispatch({type: 'GET_DATA_WAYS', ways: response.data});
    })
    .catch((error) => {
      console.error(error);
    });
  };
}
