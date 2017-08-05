import React, { Component } from 'react';
import { Panel } from 'react-bootstrap';


class Target extends Component {
  render() {
    if(this.props.data){

      return (
        <Panel header="Текущий пункт назначения">
          <div className="r_row"><div className="r_header">Адрес: </div><div className="r_elem">{this.props.data.id_patient.address}</div></div>
          <div className="r_row"><div className="r_header">Пациент: </div><div className="r_elem">{this.props.data.id_patient.login}</div></div>
          <div className="r_row"><div className="r_header">Телефон: </div><div className="r_elem">{this.props.data.id_patient.phone}</div></div>
        </Panel>
      );
    }
    else {
      return (
        <div></div>
      )
    }
  }
}

export default Target;
