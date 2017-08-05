import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Navbar, Nav, DropdownButton, MenuItem } from 'react-bootstrap';
import { Route } from 'react-router';

import {connect} from 'react-redux';
import {bindActionCreators} from 'redux';
import * as sessionActions from '../../../actions/sessionActions';

class LogOut extends Component {
  constructor(props) {
    super(props);
    this.logOut = this.logOut.bind(this);
  }

  logOut(event) {
    event.preventDefault();
    this.props.actions.logOutUser();
  }

  render() {
    return (
      <DropdownButton id="drop" bsStyle="primary" className="btn-signIn navbar-btn navbar-right" title={this.props.user.login || "noname"}>
        <MenuItem eventKey="2">Профиль</MenuItem>
        <MenuItem eventKey="2">Настройки</MenuItem>
        <MenuItem divider />
        <MenuItem eventKey="3" onClick={this.logOut}>Выйти</MenuItem>
      </DropdownButton>
    );
  }
}

function mapStateToProps(store) {
  const { user } = store.session;
  return { user };
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(sessionActions, dispatch)
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(LogOut);
