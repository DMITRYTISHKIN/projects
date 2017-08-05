import React, { Component } from 'react';
import StateCounter from './state';
import { Row, Col, FormGroup, ControlLabel, FormControl, Form, HelpBlock, Panel, Button, ListGroupItem } from 'react-bootstrap';
import SelectComponent from './select';

import {Radio, RadioGroup} from 'react-icheck';

import * as recordActions from '../../actions/recordActions';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

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

class Record extends Component {
  constructor(props) {
    super(props);

    this.onChangeRecord = this.onChangeRecord.bind(this);
    this.onSubmitRecord = this.onSubmitRecord.bind(this);
    this.onChangeSelect = this.onChangeSelect.bind(this);
    this.onResetRecord = this.onResetRecord.bind(this);

    this.recorddata = {
      priority: 'low',
      comment: '',
      id_worker: ''
    };
  }

  componentWillMount(){
    this.props.actions.getRecord();
  }

  onChangeRecord(event) {
    const field = event.target.name;
    this.recorddata[field] = event.target.value;
  }
  onSubmitRecord(event){
    event.preventDefault();
    this.props.actions.setRecord(this.recorddata);
  }
  onResetRecord(event){
    event.preventDefault();
    this.props.actions.delRecord();
  }
  onChangeSelect(value){
    this.recorddata["id_worker"] = value._id;
  }

  render() {
    let val = {
      select: "",
      priority: "low",
      comment: "Введите текст"
    };

    let isDisabled = true;
    if(Object.keys(this.props.record) == ''){
      isDisabled = false;
    }
    else{
      val = {
        select: this.props.record.id_worker,
        priority: this.props.record.priority,
        comment: this.props.record.comment
      };
    }

    return (
      <Row className="show-grid">
        <Col md={2}></Col>
        <Col xs={12} md={8}>
          <Panel header="Записаться на прием">
            <Form className="recordForm">
              <fieldset disabled={isDisabled}>
                <FormGroup controlId="formControlsSelect">
                  <ControlLabel>Врач</ControlLabel>
                  <SelectComponent
                    val={val.select}
                    isDisabled={isDisabled}
                    onChangeSelect={this.onChangeSelect}
                  />
                </FormGroup>

                <ControlLabel>Приоритет</ControlLabel>
                <RadioGroup
                  className="recordRadioGroup"
                  name="priority"
                  value={val.priority}
                  onChange={this.onChangeRecord}
                >
                    <Radio
                      value="low"
                      radioClass="iradio_flat-green"
                      increaseArea="20%"
                      label="Низкий"
                      disabled={isDisabled}
                    />
                    <Radio
                      value="middle"
                      radioClass="iradio_flat-yellow"
                      increaseArea="20%"
                      label="Средний"
                      disabled={isDisabled}
                    />
                    <Radio
                      value="high"
                      radioClass="iradio_flat-red"
                      increaseArea="20%"
                      label="Высокий"
                      disabled={isDisabled}
                    />
                </RadioGroup>
                <FieldGroup
                  name="login"
                  id="formControlsText"
                  type="text"
                  label="Комментарий"
                  placeholder={val.comment}
                  name="comment"
                  onChange={this.onChangeRecord}
                />
              <ListGroupItem
                bsStyle="warning"
              >
                <strong>Внимание!</strong> Ваши данные будут взяты из профиля! (ФИО, адрес и тд.)
                <Button bsStyle="link">Изменить данные</Button>
              </ListGroupItem>
              </fieldset>
              <Button
                bsStyle="primary"
                onClick={this.onSubmitRecord}
                disabled={isDisabled}
              >
                Записаться
              </Button>
              {' '}
              <Button
                bsStyle="warning"
                onClick={this.onResetRecord}
                disabled={!isDisabled}
              >
                Отменить
              </Button>
            </Form>
          </Panel>
        </Col>
        <Col md={2}></Col>
      </Row>
    )
  }
}

function mapStateToProps(store) {
  const { record } = store.record;

  return { record };
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(recordActions, dispatch)
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(Record);
