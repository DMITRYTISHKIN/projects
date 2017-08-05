import React, { Component } from 'react';
import Header from './header/header'

import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as sessionActions from '../actions/sessionActions';

class App extends Component {
  constructor(props){
    super(props);
  }

  componentDidMount(){
    this.props.actions.checkUser();
  }

  render() {
    console.log("Render");
    return (
      <div className="App">
        <Header />
        {this.props.children}
      </div>
    );
  }
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(sessionActions, dispatch)
  };
}

export default connect(null, mapDispatchToProps)(App);
