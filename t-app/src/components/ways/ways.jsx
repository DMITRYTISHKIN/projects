import React, { Component, PropTypes } from 'react';
import { Panel } from 'react-bootstrap';
import { withGoogleMap, GoogleMap, Marker,   Circle,
  InfoWindow, } from "react-google-maps";

import withScriptjs from "react-google-maps/lib/async/withScriptjs";

import canUseDOM from "can-use-dom";

import raf from "raf";

import * as waysActions from '../../actions/waysActions';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import _ from 'lodash';
import svg from '../../img/circle.png';


const geolocation = (
  canUseDOM && navigator.geolocation ?
  navigator.geolocation :
  ({
    getCurrentPosition(success, failure) {
      failure(`Your browser doesn't support geolocation.`);
    },
  })
);

const AsyncGettingStartedExampleGoogleMap = _.flowRight(
  withScriptjs,
  withGoogleMap,
)(props => (
  <GoogleMap
    ref={props.onMapLoad}
    defaultZoom={3}
    defaultCenter={{ lat: -25.363882, lng: 131.044922 }}
    onClick={props.onMapClick}
  >
    {props.center && (
      <InfoWindow position={props.center}>
        <div>{props.content}</div>
      </InfoWindow>
    )}
    {props.center && (
      <Marker
        className={"ggg"}
        icon={svg}
        position={props.center}
      />
    )}
    {props.markers.map(marker => {
      return (
      <Marker
        position={marker.id_patient.latLng}
        onRightClick={() => props.onMarkerRightClick(marker)}
      />
    )})}
  </GoogleMap>
));

class Ways extends Component {


  constructor(props) {
    super(props);


    this.handleMapLoad = this.handleMapLoad.bind(this);
    this.handleMapClick = this.handleMapClick.bind(this);
    this.handleMarkerRightClick = this.handleMarkerRightClick.bind(this);
  }

  handleMapLoad(map) {
     this._mapComponent = map;
     if (map) {
       console.log(map.getZoom());
     }
  }

  handleMapClick(event) {

  }

  handleMarkerRightClick(targetMarker) {

  }

  componentWillMount(){
    this.props.actions.getWays();
  }


  state = {
    center: null,
    content: null,
    radius: 25,
  };

  isUnmounted = false;
  componentDidMount(){
    const tick = () => {
      if (this.isUnmounted) {
        return;
      }
      this.setState({ radius: Math.max(this.state.radius - 20, 0) });

      if (this.state.radius > 200) {
        raf(tick);
      }
    };
    geolocation.getCurrentPosition((position) => {
      if (this.isUnmounted) {
        return;
      }
      this.setState({
        center: {
          lat: position.coords.latitude,
          lng: position.coords.longitude,
        },
        content: `Location found using HTML5.`,
      });

      raf(tick);
    }, (reason) => {
      if (this.isUnmounted) {
        return;
      }
      this.setState({
        center: {
          lat: 60,
          lng: 105,
        },
        content: `Error: The Geolocation service failed (${reason}).`,
      });
    });
  }


  render() {


    if(this.props.ways)
      return (
        <AsyncGettingStartedExampleGoogleMap
          googleMapURL="https://maps.googleapis.com/maps/api/js?v=3.exp&libraries=geometry,drawing,places&key=AIzaSyC4R6AN7SmujjPUIGKdyao2Kqitzr1kiRg"
          loadingElement={
            <div style={{ height: `100%` }}>
              <div
                style={{
                  display: `block`,
                  width: `80px`,
                  height: `80px`,
                  margin: `150px auto`,
                }}
              />
            </div>
          }
          containerElement={
            <div style={{ height: `calc(100% - 50px)` }} />
          }
          mapElement={
            <div style={{ height: `100%` }} />
          }
          onMapLoad={this.handleMapLoad}
          onMapClick={this.handleMapClick}
          markers={this.props.ways}
          onMarkerRightClick={this.handleMarkerRightClick}
          center={this.state.center}
          content={this.state.content}
          radius={this.state.radius}
        />
      )
    else {
      return null;
    }

  }

}



function mapStateToProps(store) {
  const { ways } = store.ways;

  return { ways };
}
function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(waysActions, dispatch)
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(Ways);
