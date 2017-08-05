import React, { Component } from 'react';
import { Button, Modal } from 'react-bootstrap';
import { connect } from 'react-redux';


class ModalDialog extends Component {
  constructor(props) {
    super(props);

    this.toggleModal = this.toggleModal.bind(this);
  }

  toggleModal() {
    this.props.dispatch({ type: 'TOGGLE_MODAL' });
  }


  render() {
    const { title, children, button, confirm, showModal } = this.props;

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
    console.log(children);

    return (
      <Button
        bsStyle="primary"
        onClick={this.toggleModal}
        className={button.className}
      >
        {button.title}
        <Modal
          show={showModal}
          onHide={this.toggleModal}
          animation={false}
        >
          <Modal.Header closeButton>
            <Modal.Title>{title}</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            {children}
          </Modal.Body>
          <Modal.Footer>
            {confirmButton}
            <Button onClick={this.toggleModal}>Отмена</Button>
          </Modal.Footer>
        </Modal>
      </Button>
    );
  }
}



function mapStateToProps(store) {
  const { showModal } = store.auth;

  return { showModal };
}


export default connect(mapStateToProps)(ModalDialog);
