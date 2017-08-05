import React, { Component } from 'react';
import { Panel } from 'react-bootstrap';
import {Doughnut} from 'react-chartjs-2';
const data = {
  datasets: [{
    data: [
      0,
      0,
      0
    ],
    backgroundColor: [
        'rgba(255, 99, 132, 0.2)',
        'rgba(255, 206, 86, 0.2)',
        'rgba(145, 220, 90, 0.2)'
    ],
    borderColor: [
        'rgba(255,99,132,1)',
        'rgba(255, 206, 86, 1)',
        'rgba(145, 220, 90, 1)'
    ],
    borderWidth: 1,
    label: 'Заявки' // for legend
  }],
  labels: [
    'Высокий',
    'Средний',
    'Низкий'

  ]
};

const data1 = {
	labels: [
		'-',
		'Завершенные',
		'Ожидают'
	],
	datasets: [{
		data: [12, 2, 4],
		backgroundColor: [
		'#CCC',
		'rgba(145, 220, 90, 1)',
		'#FFCE56'
		]
	}]
};

const option = {
  scales: {
      xAxes: [{
          stacked: true
      }],
      yAxes: [{
          stacked: true
      }]
  }
}

class Schedule extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    if(this.props.data){
      data.datasets[0].data[0] = this.props.data.high.length;
      data.datasets[0].data[1] = this.props.data.middle.length;
      data.datasets[0].data[2] = this.props.data.low.length;
    }
    else{
      data.datasets[0].data[0] = 0;
      data.datasets[0].data[1] = 0;
      data.datasets[0].data[2] = 0;
    }

    return (
      <Panel header="Статистика заявок">
        <Doughnut data={data1} redraw={true} />
      </Panel>
    );
  }
}

export default Schedule;
