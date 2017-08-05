import React, { Component } from 'react';
import { Row, Col } from 'react-bootstrap';
import Target from './target';
import Waiting from './waiting';
import Schedule from './schedule';

import * as dashboardActions from '../../actions/dashboardActions';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

class Dashboard extends Component {
  constructor(props) {
    super(props);

    this.completePatient = this.completePatient.bind(this);
  }

  componentDidMount(){
    this.props.actions.getDashboard();
  }

  completePatient(e){
    this.props.actions.delDashboard(e.target.getAttribute('id'));
  }

  render() {
    let dataSchedule, dataWaiting, dataTarget;

    if(this.props.patients.length){
      dataSchedule = {
        low: this.props.patients.filter( (elem) => {
          return elem.priority == "low"
        }),
        middle: this.props.patients.filter( (elem) => {
          return elem.priority == "middle"
        }),
        high:this.props.patients.filter( (elem) => {
          return elem.priority == "high"
        }),
      }
      dataWaiting = this.props.patients;
      dataTarget = this.props.patients[0];
    }

    return (
      <div>
        <Row className="show-grid">
          <Col xs={12} md={7}>
            <Waiting data={dataWaiting} onCompletePatient={this.completePatient}/>
          </Col>
          <Col xs={12} md={5}>
            <Target data={dataTarget}/>
            <Schedule data={dataSchedule}/>
          </Col>
        </Row>
      </div>
    );
  }
}
function mapStateToProps(store) {
  const { patients } = store.dashboard;

  return { patients };
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(dashboardActions, dispatch)
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(Dashboard);
