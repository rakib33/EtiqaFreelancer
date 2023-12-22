import http from '../http-common';

class UserDataService {
    getAll() {
      return http.get("/Users");
    }
  
    get(id) {
      return http.get(`/Users/${id}`);
    }
  
    create(data) {
      return http.post("/Users", data);
    }
  
    update(id, data) {
      return http.put(`/Users/${id}`, data);
    }
  
    delete(id) {
      return http.delete(`/Users/${id}`);
    }
  
    deleteAll() {
      return http.delete(`/Users`);
    }
  
    findByTitle(key) {
      return http.get(`/Users?key=${key}`);
    }
  }
  
  export default new UserDataService();