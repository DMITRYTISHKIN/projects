import cookie from 'react-cookie';


class StatisticsApi {
  static getWorkers() {
    const request = new Request('http://127.0.0.1:4001/api/workers/all', {
      method: 'GET',
      headers: new Headers({
        'Content-Type': 'application/json',
        'Authorization': cookie.load('jwt')
      })
    });
    return fetch(request)
  }


}

export default StatisticsApi;
