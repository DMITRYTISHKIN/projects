import statisticsApi from '../api/statisticsApi';

export function getWorkers() {
  return function(dispatch) {
    return statisticsApi.getWorkers()
    .then((response) => {
      if(response.ok) {
        return response.json();
      }
      throw new Error('error');
    })
    .then((response) => {
      debugger
      dispatch({type: 'GET_DATA_STATISTICS', workers: response.data});
    })
    .catch((error) => {
      console.error(error);
    });
  };
}
