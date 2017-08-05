// src/api/sessionApi.js;
import cookie from 'react-cookie';

class SessionApi {
 static login(credentials) {
   const request = new Request('http://127.0.0.1:4001/api/authenticate', {
     method: 'POST',
     headers: new Headers({
       'Content-Type': 'application/json'
     }),
     body: JSON.stringify({
       email: credentials.email,
       password: credentials.password
     })
   });


   return fetch(request)
 }

 static check() {
   const request = new Request('http://127.0.0.1:4001/api/check', {
     method: 'GET',
     headers: new Headers({
       'Content-Type': 'application/json',
       'Authorization': cookie.load('jwt')
     })
   });


   return fetch(request)
 }

 static register(credentials) {
   const request = new Request('http://127.0.0.1:4001/api/register', {
     method: 'POST',
     headers: new Headers({
       'Content-Type': 'application/json'
     }),
     body: JSON.stringify({
       login: credentials.login,
       email: credentials.email,
       password: credentials.password,
       addressfull: credentials.addressfull,
       latLng: credentials.latLng,
       address: credentials.address,
       phone: credentials.phone
     })
   });


   return fetch(request)
 }
}

export default SessionApi;
