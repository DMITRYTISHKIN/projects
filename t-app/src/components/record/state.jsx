import React, { Component, PropTypes } from 'react';
import Counter from './counter';
import { connect } from 'react-redux';

const propTypes = {
  dispatch: PropTypes.func.isRequired,
  value: PropTypes.number.isRequired
};

class StateCounter extends Component {
  constructor(props) {
    super(props);

    this.handleClick = this.handleClick.bind(this);
  }

  handleClick() {
    this.props.dispatch({ type: 'INCREMENT_COUNTER' });
  }

  render() {
    return <Counter value={this.props.value} onClick={this.handleClick} />;
  }
}

StateCounter.propTypes = propTypes;

function mapStateToProps(state) {
  const { value } = state.counter;

  return { value };
}

export default connect(mapStateToProps)(StateCounter);
