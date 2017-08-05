import React, { Component } from 'react';
import Select from 'react-select';
import 'react-select/dist/react-select.css';
import Gravatar from 'react-gravatar';

import * as recordActions from '../../actions/recordActions';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

const USERS = [
	{ value: 'John Smith', label: ['John Smith', "13:32" ], email: 'john@smith.com'},
	{ value: 'Merry Jane', label: ['Merry Jane', "15:31" ], email: 'merry@jane.com'},
	{ value: 'Stan Hoper', label: ['Stan Hoper', "14:57" ], email: 'stan@hoper.com'},
  { value: 'Dmitry Tishkin', label: ['Dmitry Tishkin', "14:57" ], email: 'dmitrytishkin@yandex.ru'}
];

const GRAVATAR_SIZE = 45;

class GravatarOption extends Component {
  constructor (props) {
    super(props);

    this.handleMouseDown = this.handleMouseDown.bind(this);
    this.handleMouseEnter = this.handleMouseEnter.bind(this);
    this.handleMouseMove = this.handleMouseMove.bind(this);

  }


	handleMouseDown (event) {
		event.preventDefault();
		event.stopPropagation();
		this.props.onSelect(this.props.option, event);
	}
	handleMouseEnter (event) {
		this.props.onFocus(this.props.option, event);
	}
	handleMouseMove (event) {
		if (this.props.isFocused) return;
		this.props.onFocus(this.props.option, event);
	}
	render () {

		let gravatarStyle = {
			borderRadius: 3,
			float: 'left',
			marginRight: 10,
			position: 'relative',
			top: -2,
			verticalAlign: 'middle',
		};
		return (
			<div className={this.props.className}
				onMouseDown={this.handleMouseDown}
				onMouseEnter={this.handleMouseEnter}
				onMouseMove={this.handleMouseMove}
				title={this.props.option.title}>
				<Gravatar email={this.props.option.email} size={GRAVATAR_SIZE} style={gravatarStyle} />
				<label>{this.props.children[0]}</label>
        <div>Прибытие не ранее: {new Date(this.props.children[1]).toTimeString()}</div>
			</div>
		);
	}
}

class GravatarValue extends Component {
	render () {
		var gravatarStyle = {
			borderRadius: 3,
			display: 'inline-block',
			marginRight: 10,
			position: 'relative',
			top: -2,
			verticalAlign: 'middle',
		};
    let val;
    if(this.props.children != undefined)
      val = this.props.children[0];

		return (
			<div className="Select-value" title={this.props.value.title}>
				<span className="Select-value-label">
					<Gravatar email={this.props.value.email} size={15} style={gravatarStyle} />
					{val}
				</span>
			</div>
		);
	}
}

class SelectComponent extends Component {
  constructor (props) {
    super(props);

    this.setValue = this.setValue.bind(this);



    this.state = {
      value: null
    };
		debugger
		if(this.props.val.length){
			let value;
			this.props.events.forEach( (item) => {
				if(item._id === this.props.val)
					this.state.value = item;
			});
		}
  }
	componentDidMount (){
		this.props.actions.getWorkersForSelect();
	}

	setValue (value) {
		this.props.onChangeSelect(value);
		this.setState({ value });
	}
	render () {
		var placeholder = <span>Выберите врача</span>;

		return (
			<div className="section">
				<Select
					disabled={this.props.isDisabled}
					onChange={this.setValue}
					optionComponent={GravatarOption}
					options={this.props.events}
					placeholder={placeholder}
					value={this.state.value}
					valueComponent={GravatarValue}
					/>
			</div>
		);
	}
}
function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(recordActions, dispatch)
  };
}
function mapStateToProps(store) {
  const { events } = store.record;
  return { events };
}

export default connect(mapStateToProps, mapDispatchToProps)(SelectComponent);
