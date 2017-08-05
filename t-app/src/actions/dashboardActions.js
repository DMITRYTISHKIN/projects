import dashboardApi from '../api/dashboardApi';

export function getDashboard() {
  return function(dispatch) {
    return dashboardApi.getDashboard()
    .then((response) => {
      if(response.ok) {
        return response.json();
      }
      throw new Error('error');
    })
    .then((response) => {
      dispatch({type: 'GET_DATA_DASHBOARD', patients: response.data});
    })
    .catch((error) => {
      console.error(error);
    });
  };
}

export function delDashboard(id) {
  return function(dispatch) {
    return dashboardApi.delDashboard(id)
    .then((response) => {
      if(response.ok) {
        return response.json();
      }
      throw new Error('error');
    })
    .then((response) => {
      debugger
      dispatch({type: 'DEL_DATA_DASHBOARD', id: response.message});
    })
    .catch((error) => {
      console.error(error);
    });
  };
}
