import cookie from 'react-cookie';


class DashboardApi {
  static getDashboard() {
    const request = new Request('http://127.0.0.1:4001/api/dashboard', {
      method: 'GET',
      headers: new Headers({
        'Content-Type': 'application/json',
        'Authorization': cookie.load('jwt')
      })
    });
    return fetch(request)
  }

  static delDashboard(id) {
    const request = new Request('http://127.0.0.1:4001/api/dashboard', {
      method: 'DELETE',
      headers: new Headers({
        'Content-Type': 'application/json',
        'Authorization': cookie.load('jwt')
      }),
      body: JSON.stringify({id: id})
    });
    return fetch(request)
  }
}

export default DashboardApi;
