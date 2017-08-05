import cookie from 'react-cookie';


class RecordApi {
  static delRecord(data) {
    const request = new Request('http://127.0.0.1:4001/api/record', {
      method: 'DELETE',
      headers: new Headers({
        'Content-Type': 'application/json',
        'Authorization': cookie.load('jwt')
      })
    });
    return fetch(request)
  }

  static setRecord(data) {
    const request = new Request('http://127.0.0.1:4001/api/record', {
      method: 'POST',
      headers: new Headers({
        'Content-Type': 'application/json',
        'Authorization': cookie.load('jwt')
      }),
      body: JSON.stringify(data)
    });
    return fetch(request)
  }

  static getRecord() {
    const request = new Request('http://127.0.0.1:4001/api/record', {
      method: 'GET',
      headers: new Headers({
        'Content-Type': 'application/json',
        'Authorization': cookie.load('jwt')
      })
    });
    return fetch(request)
  }

 static getWorkers() {
   const request = new Request('http://127.0.0.1:4001/api/workers', {
     method: 'GET',
     headers: new Headers({
       'Content-Type': 'application/json',
       'Authorization': cookie.load('jwt')
     })
   });
   return fetch(request)
 }

}
export default RecordApi;
