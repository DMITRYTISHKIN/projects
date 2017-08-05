const initialState = {
  showEventModal: false,
  date: {
    start: undefined,
    end: undefined
  }
};

export default function(state = initialState, action) {
  switch (action.type) {
    case 'TOGGLE_EVENT_MODAL':
      if(action.date)
        return { showEventModal: !state.showEventModal, date: action.date };
      else
        return { showEventModal: !state.showEventModal, date: {} };
    default:
      return state;
  }
}
