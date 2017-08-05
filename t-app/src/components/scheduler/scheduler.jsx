import React, { Component } from 'react';
import { Row, Col, FormGroup, ControlLabel, FormControl, HelpBlock, Panel, Button } from 'react-bootstrap';
import BigCalendar from 'react-big-calendar';
import moment from 'moment';
import { DragDropContext } from 'react-dnd'
import HTML5Backend from 'react-dnd-html5-backend'
import withDragAndDrop from 'react-big-calendar/lib/addons/dragAndDrop';
import SetEventDialog from './setEventDialog.jsx';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as schedulerActions from '../../actions/schedulerActions';
//require('globalize/lib/cultures/globalize.culture.fr');
import 'react-big-calendar/lib/addons/dragAndDrop/styles.css';
import 'react-big-calendar/lib/css/react-big-calendar.css';

BigCalendar.momentLocalizer(moment);
const DragAndDropCalendar = withDragAndDrop(BigCalendar);
class Scheduler extends Component {
  constructor (props) {
    super(props)
    this.range = {start: moment(), end: moment()};

    this.moveEvent = this.moveEvent.bind(this)
    this.selectSlot = this.selectSlot.bind(this)
    this.addEvent = this.addEvent.bind(this);

  }

  componentDidMount(){
    this.props.actions.getWorkEvent(this.props.user._id);
  }

  moveEvent({ event, start, end }) {
    const { events } = this.props;

    const idx = events.indexOf(event);
    const updatedEvent = { ...event, start, end };

    const nextEvents = [...events]
    nextEvents.splice(idx, 1, updatedEvent)

    this.setState({
      events: nextEvents
    })

    console.log(`${event.title} was dropped onto ${event.start}`);
  }

  selectSlot(data){
    this.range.start = data.start;
    this.range.end = data.end;
    this.props.toggleModal();
  }

  addEvent(e){
    let date = {
      'title': "new",
      'start': this.range.start,
      'end': this.range.end
    }
    this.props.actions.addWorkEvent(date);
  }

  render() {
    const { date } = this.props;

    let button = ({
        className: "btn-signIn navbar-btn navbar-right",
        title: "Добавить"
    });

    let confirmButton = ({
        onClick: this.addEvent

    });

    return (
      <Row className="show-grid">
        <SetEventDialog
          button={button}
          confirm={confirmButton}
        >

        </SetEventDialog>
        <Col xs={12} md={12}>
          <Panel header="Расписание">
            <DragAndDropCalendar
              culture={'ru'}
              onEventDrop={this.moveEvent}
              selectable
              events={this.props.events}
              defaultView='week'
              defaultDate={new Date()}
              onSelectEvent={event => console.log(event.title)}
              onSelectSlot={this.selectSlot}
            />
          </Panel>
        </Col>
      </Row>
    )
  }
}

var events = []

function mapDispatchToProps(dispatch) {
  return {
    toggleModal: () => {
      dispatch({ type: 'TOGGLE_EVENT_MODAL', date: this.range});
    },
    actions: bindActionCreators(schedulerActions, dispatch)
  };
}

function mapStateToProps(store) {
  const { events } = store.schedulerEvents;
  const { showEventModal, date } = store.scheduler;
  const { user } = store.session;

  return { showEventModal, date, events, user };
}

Scheduler = DragDropContext(HTML5Backend)(Scheduler)
export default connect(mapStateToProps, mapDispatchToProps)(Scheduler);
