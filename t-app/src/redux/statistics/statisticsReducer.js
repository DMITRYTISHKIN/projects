const initialState = {
  workers: []
};

export default function(state = initialState, action) {
  switch (action.type) {
    case 'GET_DATA_STATISTICS':
      return { workers: action.workers };
    default:
      return state;
  }
}
