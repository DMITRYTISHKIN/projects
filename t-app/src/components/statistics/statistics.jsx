import React, { Component } from 'react';
import { Row, Col, FormGroup, Table, ControlLabel, FormControl, Form, HelpBlock, Panel, Button, ListGroupItem } from 'react-bootstrap';
import moment from 'moment';
import {Radio, RadioGroup} from 'react-icheck';

import * as statisticsActions from '../../actions/statisticsActions';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

class WaitingTable extends Component {
  render () {
    if(this.props.workers){
      let tbody = [];
      this.props.workers.forEach( (elem, i) => {
        tbody.push(
          <Rows key={i.toString()} i={i+1} workers={elem} />
        );
      });

      return (
        <Table className="panelTable" responsive>
          <thead>
            <tr>
              <th>#</th>
              <th>ФИО</th>
              <th>E-mail</th>
              <th>График</th>
              <th>Оптимальное</th>
              <th>Фактическое</th>
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
    let start = this.props.workers.start;
    let end = this.props.workers.end;
    return (
      <tr>
        <td>{this.props.i}</td>
        <td>{this.props.workers.id_doctor.login}</td>
        <td>{this.props.workers.id_doctor.email}</td>
        <td><div>{start}</div><div>{end}</div></td>
        <td>{end}</td>
        <td>{end}</td>
      </tr>
    )
  }
}

class Statistics extends Component {
  constructor(props) {
    super(props);

  }

  componentWillMount(){
    this.props.actions.getWorkers();
  }

  render() {
    return (
      <Row className="show-grid">
        <Col md={2}></Col>
        <Col xs={12} md={8}>
          <Panel header="Пользователи">
            <WaitingTable
              workers={this.props.workers}
            />
          </Panel>
        </Col>
        <Col md={2}></Col>
      </Row>
    )
  }
}

function mapStateToProps(store) {
  const { workers } = store.statistics;

  return { workers };
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(statisticsActions, dispatch)
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(Statistics);
