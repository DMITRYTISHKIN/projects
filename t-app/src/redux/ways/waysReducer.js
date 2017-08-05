const initialState = {
  ways: []
};

export default function(state = initialState, action) {
  switch (action.type) {
    case 'GET_DATA_WAYS':
      return { ways: action.ways };
    default:
      return state;
  }
}
