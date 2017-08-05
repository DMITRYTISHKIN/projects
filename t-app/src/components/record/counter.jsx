import React, { Component, } from 'react';
import Button from 'react-bootstrap/lib/Button';

class Counter extends Component {
  render() {
    const { onClick, value } = this.props;

    return (
      <div>
        <div className='counter-label'>
          Value: {value}
        </div>
        <Button onClick={onClick}>+</Button>
      </div>
    );
  }
}

export default Counter;
