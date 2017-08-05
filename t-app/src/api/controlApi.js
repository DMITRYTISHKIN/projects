import cookie from 'react-cookie';


class ControlApi {
  static getControl(data) {
    const request = new Request('http://127.0.0.1:4001/api/users', {
      method: 'GET',
      headers: new Headers({
        'Content-Type': 'application/json',
        'Authorization': cookie.load('jwt')
      })
    });
    return fetch(request)
  }

}
export default ControlApi;
