import React from 'react'
import PlacesAutocomplete, { geocodeByAddress, getLatLng, geocodeByPlaceId } from 'react-places-autocomplete'


class SimpleForm extends React.Component {
  constructor(props) {
    super(props)
    this.state = { address: 'San Francisco, CA' }
    this.onChangeAddress = this.onChangeAddress.bind(this);

  }

  /*handleFormSubmit = (event) => {
    event.preventDefault()

    geocodeByAddress(this.state.address)
      .then(results => getLatLng(results[0]))
      .then(latLng => console.log('Success', latLng))
      .catch(error => console.error('Error', error))
  }*/
  onChangeAddress(address){
    this.setState({ address });
    this.props.onChangeAddress({ address });
  }

  render() {
    const inputProps = {
      value: this.state.address,
      onChange: this.onChangeAddress
    }

    return (
      <PlacesAutocomplete inputProps={inputProps} />
    )
  }
}

export default SimpleForm
