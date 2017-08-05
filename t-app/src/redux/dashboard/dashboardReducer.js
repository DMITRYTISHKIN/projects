const initialState = {
  patients: []
};

export default function(state = initialState, action) {
  switch (action.type) {
    case 'GET_DATA_DASHBOARD':
      return { patients: action.patients };
    case 'DEL_DATA_DASHBOARD':
      let newPatients = state.patients.filter((elem, i) => {
        if(elem._id != action.id)
          return elem;
      });
      return { patients: newPatients };
    default:
      return state;
  }
}
