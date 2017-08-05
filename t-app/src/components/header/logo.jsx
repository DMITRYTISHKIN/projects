import React, { Component } from 'react';
import { Navbar } from 'react-bootstrap';
import svg from '../../img/nurse.svg';
class Logo extends Component {
  render() {
    return (
      <Navbar.Header className="Logo">
        <Navbar.Brand>
          <img src={svg} alt=""/>
        </Navbar.Brand>
      </Navbar.Header>
    );
  }
}

export default Logo;
