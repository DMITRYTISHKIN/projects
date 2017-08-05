import React, { Component } from 'react';
import { Button, Modal, FormGroup, ControlLabel } from 'react-bootstrap';
import { connect } from 'react-redux';
import 'rc-time-picker/assets/index.css';
import moment from 'moment';
import TimePicker from 'rc-time-picker';

class SetEventDialog extends Component {
  constructor(props) {
    super(props);

    this.toggleModal = this.toggleModal.bind(this);
  }

  toggleModal() {
    this.props.dispatch({ type: 'TOGGLE_EVENT_MODAL'});
  }


  render() {
    const { title, children, button, confirm, showEventModal, date } = this.props;

    let confirmButton = ""
    if(confirm){
      confirmButton = (
        <Button
          bsStyle="primary"
          onClick={confirm.onClick}
        >
          {confirm.title}
        </Button>
      )
    }

    return (
      <Modal
        show={showEventModal}
        onHide={this.toggleModal}
        animation={false}
        bsSize="small"
      >
        <Modal.Body>
          <form>
            <FormGroup>
              <ControlLabel>Период рабочего дня</ControlLabel>
              <TimePicker name="start" defaultValue={ moment() } showSecond={false} />
              <TimePicker name="end" defaultValue={ moment() } showSecond={false} />
            </FormGroup>
          </form>
        </Modal.Body>
        <Modal.Footer>
          {confirmButton}
          <Button onClick={this.toggleModal}>Отмена</Button>
        </Modal.Footer>
      </Modal>
    );
  }
}




function mapStateToProps(store) {
  const { showEventModal} = store.scheduler;

  return { showEventModal};
}


export default connect(mapStateToProps)(SetEventDialog);
