const initialState = {
  events: []
};

export default function(state = initialState, action) {
  switch (action.type) {
    case 'SCHEDULER_EVENT_GET':
        action.events.forEach((item) => {
          item.start = new Date(item.start);
          item.end = new Date(item.end);
        });
        return { events: action.events };
    case 'SCHEDULER_EVENT_ADD':
        action.events.start = new Date(action.events.start);
        action.events.end = new Date(action.events.end);
        return { events: [ ...state.events, action.events] };
    default:
      return state;
  }
}
