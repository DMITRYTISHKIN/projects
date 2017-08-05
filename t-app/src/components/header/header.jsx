import React, { Component } from 'react';

import Menu from './menu';
import { Navbar } from 'react-bootstrap';


class Header extends Component {
  render() {
    return (
      <Navbar inverse>
        <Menu />
      </Navbar>
    );
  }
}

export default Header;
