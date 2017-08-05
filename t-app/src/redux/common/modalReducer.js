const initialState = {
  showModal: false
};

export default function(state = initialState, action) {
  switch (action.type) {
    case 'TOGGLE_MODAL':
      return { showModal: !state.showModal };
    default:
      return state;
  }
}
