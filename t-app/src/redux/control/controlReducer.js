const initialState = {
  users: []
};

export default function(state = initialState, action) {
  switch (action.type) {
    case 'GET_DATA_CONTROL':
      return { users: action.users };
    default:
      return state;
  }
}
