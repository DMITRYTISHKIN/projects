import React, { Component } from 'react';
import { Row, Col, FormGroup, Table, ControlLabel, FormControl, Form, HelpBlock, Panel, Button, ListGroupItem } from 'react-bootstrap';

import {Radio, RadioGroup} from 'react-icheck';

import * as controlActions from '../../actions/controlActions';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

class WaitingTable extends Component {
  render () {
    if(this.props.users){
      let tbody = [];
      this.props.users.forEach( (elem, i) => {
        tbody.push(
          <Rows key={i.toString()} i={i+1} users={elem} />
        );
      });

      return (
        <Table className="panelTable" responsive>
          <thead>
            <tr>
              <th>#</th>
              <th>ФИО</th>
              <th>E-mail</th>
              <th>Роль</th>
            </tr>
          </thead>
          <tbody>
            {tbody}
          </tbody>
        </Table>
      )
    }
    else
      return (
        <div className="noWaiting">На текущий момент заявок нет</div>
      )
  }
}

class Rows extends Component {
  render () {
    return (
      <tr>
        <td>{this.props.i}</td>
        <td>{this.props.users.login}</td>
        <td>{this.props.users.email}</td>
        <td>
          <FormControl componentClass="select">
            <option value="Пациент">Пациент</option>
            <option value="Врач">Врач</option>
            <option value="Администратор">Администратор</option>
          </FormControl>
        </td>
      </tr>
    )
  }
}

class Control extends Component {
  constructor(props) {
    super(props);

  }

  componentWillMount(){
    this.props.actions.getControl();
  }

  render() {
    return (
      <Row className="show-grid">
        <Col md={2}></Col>
        <Col xs={12} md={8}>
          <Panel header="Пользователи">
            <WaitingTable
              users={this.props.users}
            />
          </Panel>
        </Col>
        <Col md={2}></Col>
      </Row>
    )
  }
}

function mapStateToProps(store) {
  const { users } = store.control;

  return { users };
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(controlActions, dispatch)
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(Control);
