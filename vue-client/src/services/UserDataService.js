import http from '../http-common';

class UserDataService {
    getAll() {
      return http.get("/Users");
    }
  
    get(id) {
      return http.get(`/Users/${id}`);
    }
  
    create(data) {
      const formData = new FormData();
      formData.append("userName", data.userName);
      formData.append("email", data.email);
      formData.append("phoneNumber", data.phoneNumber);
      formData.append("skillSets", data.skill);
      formData.append("hobby", data.hobby);
      formData.append("file", data.selectedFile);

      console.log('user save api is called with skill:'+data.skill);
      console.log('data username:'+data.userName);
      console.log('user post is called with formdata:'+formData);
      return http.post("/Users",formData);
    }
  
    update(id, data) {
      return http.put(`/Users/${id}`, data);
    }
  
    delete(id) {
      return http.delete(`/Users?id=${id}`);
    }
  
    deleteAll() {
      return http.delete(`/Users`);
    }
  
    findByTitle(key) {
      return http.get(`/Users?key=${key}`);
    }
  }
  
  export default new UserDataService();