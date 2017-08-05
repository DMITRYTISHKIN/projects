import React, { Component } from 'react';
import { Tabs, Tab, FormControl, ControlLabel, HelpBlock, FormGroup } from 'react-bootstrap';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as sessionActions from '../../../actions/sessionActions';
import ModalDialog from '../../common/dialog';
import SimpleForm from './address';

import { geocodeByAddress, getLatLng } from 'react-places-autocomplete'


function FieldGroup({ id, label, help, ...props }) {
  let controlLabel = "";
  if(label)
    controlLabel = <ControlLabel>{label}</ControlLabel>;

  return (
    <FormGroup controlId={id}>
      {controlLabel}
      <FormControl {...props} />
      {help && <HelpBlock>{help}</HelpBlock>}
    </FormGroup>
  );
}



class AuthModal extends Component {
  constructor(props) {
    super(props);

    this.onChangeAuth = this.onChangeAuth.bind(this);
    this.onSubmitAuth = this.onSubmitAuth.bind(this);
    this.credentials = {email: '', password: ''};

    this.onChangeRegister = this.onChangeRegister.bind(this);
    this.onSubmitRegister = this.onSubmitRegister.bind(this);
    this.regdata = {
      login: '',
      email: '',
      password: '',
      password_confirm: '',
      address: '',
      addressfull: '',
      phone: '',
      latLng: {}
    };

    this.handleSelect = this.handleSelect.bind(this);
    this.state = {
      key: "Вход"
    };

    this.onChangeAddress = this.onChangeAddress.bind(this);

  }



  handleChange(e) {
    this.setState({ value: e.target.value });
  }

  handleSelect(key) {
    console.log(key);

    this.setState({key});
  }

  onChangeAuth(event) {
    const field = event.target.name;
    this.credentials[field] = event.target.value;
  }
  onSubmitAuth(event){
    event.preventDefault();
    this.props.actions.logInUser(this.credentials);
  }

  onChangeRegister(event) {
    const field = event.target.name;
    this.regdata[field] = event.target.value;
  }
  onSubmitRegister(event){
    event.preventDefault();
    geocodeByAddress(this.regdata.address)
      .then(results => getLatLng(results[0]))
      .then(latLng => {
        this.regdata["latLng"] = latLng
        this.props.actions.registerUser(this.regdata);
      })
      .catch(error => console.error('Error', error))
  }
  onChangeAddress(address){
    this.regdata["address"] = address.address;
  }

  render() {
    console.log("rend");
    let button = ({
        className: "btn-signIn navbar-btn navbar-right",
        title: "Авторизация"
    });

    let key = this.state.key
    let confirmButton = ({
        title: key,
        onClick: key === "Вход" ? this.onSubmitAuth : this.onSubmitRegister

    });

    return (
      <ModalDialog
        title="Авторизация"
        button={button}
        confirm={confirmButton}
      >
        <form>
        <Tabs
          activeKey={this.state.key}
          onSelect={this.handleSelect}
          className="authTabs"
          id="controlled-tab-example"
        >
          <Tab eventKey={"Вход"} title="Вход">
            <FieldGroup
              name="email"
              id="formControlsText"
              type="text"
              label="Адрес электронной почты"
              placeholder="Введите адрес электронной почты"
              onChange={this.onChangeAuth}
            />
            <FieldGroup
              name="password"
              id="formControlsPassword"
              label="Пароль"
              type="password"
              placeholder="Введите пароль"
              onChange={this.onChangeAuth}
            />
          </Tab>
          <Tab eventKey={"Регистрация"} title="Регистрация">
            <FieldGroup
              name="email"
              id="formControlsEmail"
              type="email"
              label="Адрес электронной почты"
              placeholder="Введите адрес электронной почты"
              onChange={this.onChangeRegister}
            />

            <FieldGroup
              name="password"
              id="formControlsPassword"
              label="Пароль"
              type="password"
              placeholder="Введите пароль"
              onChange={this.onChangeRegister}
            />
            <FieldGroup
              name="password_confirm"
              id="formControlsPassword"
              type="password"
              placeholder="Подтвердите пароль"
              onChange={this.onChangeRegister}
            />
            <FieldGroup
              name="login"
              id="formControlsLogin"
              type="text"
              label="Данные"
              placeholder="ФИО"
              onChange={this.onChangeRegister}
            />
            <SimpleForm onChangeAddress={this.onChangeAddress}/>
            <FieldGroup
              name="addressfull"
              id="formControlsAddress"
              type="text"
              placeholder="Квартира"
              onChange={this.onChangeRegister}
            />
            <FieldGroup
              name="phone"
              id="formControlsPhone"
              type="text"
              placeholder="Телефон"
              onChange={this.onChangeRegister}
            />
          </Tab>
        </Tabs>
        </form>
      </ModalDialog>
    );
  }
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(sessionActions, dispatch)
  };
}

export default connect(null, mapDispatchToProps)(AuthModal);
