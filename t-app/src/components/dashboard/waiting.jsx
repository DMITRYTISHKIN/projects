import React, { Component } from 'react';
import { Panel, Button, Table } from 'react-bootstrap';
import high from '../../img/priority/high.svg';
import middle from '../../img/priority/middle.svg';
import low from '../../img/priority/low.svg';


function GetTable(data) {
  let tbody = [];
  data.forEach( (elem, i) => {
    tbody.push(
      <Row key={i} record={elem} />
    );
  });
  return (
    <Table className="panelTable" responsive>
      <thead>
        <tr>
          <th>#</th>
          <th>ФИО</th>
          <th>Адрес</th>
          <th>Прибытие</th>
          <th></th>
        </tr>
      </thead>
      {tbody}
    </Table>
  );
}
class WaitingTable extends Component {
  render () {
    if(this.props.data){
      let tbody = [];
      this.props.data.forEach( (elem, i) => {
        tbody.push(
          <Row key={i.toString()} i={i+1} record={elem} onCompletePatient={this.props.onCompletePatient}/>
        );
      });

      return (
        <Table className="panelTable" responsive>
          <thead>
            <tr>
              <th>#</th>
              <th>ФИО</th>
              <th>Адрес</th>
              <th>Прибытие</th>
              <th></th>
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

class Row extends Component {
  render () {
    let src = low;
    /*switch (this.props.record.priority) {
      case "low":
        src = low;
        break;
      case "middle":
        src = middle;
        break;
      case "high":
        src = high;
        break;;
    }*/
    let d = new Date(this.props.record.time_target)
    return (
      <tr>
        <td>{this.props.i}</td>
        <td>{this.props.record.id_patient.login}</td>
        <td>{this.props.record.id_patient.address}</td>
        <td>{d.toTimeString()}</td>
        <td><Button id={this.props.record._id} onClick={this.props.onCompletePatient}>Завершить</Button></td>
      </tr>
    )
  }
}

class Waiting extends Component {
  constructor(props) {
    super(props);
  }


  render() {
    return (
      <Panel header="Ожидающие">
        <WaitingTable data={this.props.data} onCompletePatient={this.props.onCompletePatient}/>
      </Panel>
    );
  }
}

export default Waiting;
