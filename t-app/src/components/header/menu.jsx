import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Navbar, Nav, DropdownButton, MenuItem, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import { Route } from 'react-router';
import Logo from './logo';
import {connect} from 'react-redux';
import AuthModal from './auth/auth.jsx';
import LogOut from './auth/logout';

class Menu extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    if (this.props.logged_in.session) {
      return (
        <Navbar.Collapse>
          <Nav navbar>
            <LinkContainer to="/dashboard">
              <NavItem>Сводка</NavItem>
            </LinkContainer>
            <LinkContainer to="/scheduler">
              <NavItem>Планирование</NavItem>
            </LinkContainer>
            <LinkContainer to="/ways">
              <NavItem>Маршрут</NavItem>
            </LinkContainer>
            <LinkContainer to="/record">
              <NavItem>Запись</NavItem>
            </LinkContainer>
            <LinkContainer to="/statistics">
              <NavItem>Статистика</NavItem>
            </LinkContainer>
            <LinkContainer to="/control">
              <NavItem>Управление</NavItem>
            </LinkContainer>
          </Nav>
          <Logo />
          <Nav pullRight>
            <LogOut bsStyle="primary" login={this.props.logged_in.user.login}/>
          </Nav>
        </Navbar.Collapse>

      );
    }
    else {
      return (
        <Navbar.Collapse>
          <Logo />
          <AuthModal />
        </Navbar.Collapse>
      );
    }
  }
}


function mapStateToProps(state, ownProps) {
  return {
    logged_in: state.session,
    current_location: state.routing
  };
}

export default connect(mapStateToProps)(Menu);
