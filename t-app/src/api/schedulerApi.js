import cookie from 'react-cookie';


class SchedulerApi {
 static add(data) {
   const request = new Request('http://127.0.0.1:4001/api/scheduler', {
     method: 'POST',
     headers: new Headers({
       'Content-Type': 'application/json',
       'Authorization': cookie.load('jwt')
     }),
     body: JSON.stringify({
       title: data.title,
       start: data.start,
       end: data.end
     })
   });


   return fetch(request)
 }

 static getEvents(id) {
   const request = new Request('http://127.0.0.1:4001/api/scheduler?id_user=' + id, {
     method: 'GET',
     headers: new Headers({
       'Content-Type': 'application/json',
       'Authorization': cookie.load('jwt')
     })
   });


   return fetch(request)
 }

 static change(data) {
   const request = new Request('http://127.0.0.1:4001/api/scheduler/' + data.id, {
     method: 'PATCH',
     headers: new Headers({
       'Content-Type': 'application/json',
       'Authorization': cookie.load('jwt')
     }),
     body: JSON.stringify({
       id: data.id,
       title: data.title,
       start: data.start,
       end: data.end
     })
   });


   return fetch(request)
 }

 static deleteEvent(data) {
   const request = new Request('http://127.0.0.1:4001/api/scheduler', {
     method: 'DELETE',
     headers: new Headers({
       'Content-Type': 'application/json',
       'Authorization': 'Bearer ' + cookie.load('jwt')
     }),
     body: JSON.stringify({
       id: data.id
     })
   });


   return fetch(request)
 }
}

export default SchedulerApi;
