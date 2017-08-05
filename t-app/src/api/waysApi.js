import cookie from 'react-cookie';


class WaysApi {
  static getWays() {
    const request = new Request('http://127.0.0.1:4001/api/ways', {
      method: 'GET',
      headers: new Headers({
        'Content-Type': 'application/json',
        'Authorization': cookie.load('jwt')
      })
    });
    return fetch(request)
  }
}

export default WaysApi;
