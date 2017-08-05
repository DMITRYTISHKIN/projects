const initialState = {
  events: [],
  record: new Object()
};

export default function(state = initialState, action) {
  switch (action.type) {
    case 'RECORD_GET':
        return { events: state.events, record: action.data };
    case 'RECORD_DEL':
        return { events: state.events, record: {} };
    case 'RECORD_SET':
        return { events: state.events, record: action.data };
    case 'RECORD_EVENT_GET':
        action.events.forEach((item) => {
          item.start = new Date(item.start);
          item.end = new Date(item.end);
          item.value = item.id_doctor.login;
          item.email = item.id_doctor.email;
          item.label = [item.id_doctor.login, item.start.toString()];
        });
        return { events: action.events, record: state.record };
    default:
      return state;
  }
}
