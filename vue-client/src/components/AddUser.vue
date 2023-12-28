<template>
    <input type="text" v-model="user.id" hidden/>
    <div class="submit-form">
      <div v-if="!submitted">
        <div class="form-group">
          <label for="userName">User Name</label>
          <input
            type="text"
            class="form-control"
            id="userName"
            required
            v-model="user.userName"
            name="userName"
            placeholder="Enter user name"
          />
        </div>
  
        <div class="form-group">
          <label for="email">Email</label>
          <input
            class="form-control"
            id="email"
            required
            v-model="user.email"
            name="email"
            placeholder="Enter email"
          />
        </div>
  
        <div class="form-group">
          <label for="phoneNumber">Phone Number</label>
          <input
            class="form-control"
            id="phoneNumber"
            required
            v-model="user.phoneNumber"
            name="phoneNumber"
            placeholder="Enter phone number"
          />
        </div>
        <div class="form-group">
          <label for="skill">Skill</label>
          <textarea
            class="form-control"
            id="skill"
            required
            v-model="user.skill"
            name="skill"
            placeholder="Enter skill"
          />
        </div>
        <div class="form-group">
          <label for="hobby">Hobby</label>
          <textarea
            class="form-control"
            id="hobby"
            required
            v-model="user.hobby"
            name="hobby"
            placeholder="Enter phone number"
          />
        </div>
        <div class="form-group">
          <label for="file">Upload User Image</label>
          <input type="file"
            class="form-control"
            id="file"            
            @change="handleFileUpload" accept="image/*"                        
          />
          <div class="col-sm-8 mt-3" id="preview">
            <img height="200" width="200" style="justify-content:center; align-items:center;" v-if="url" :src="url" />
          </div>
        </div>
        <button @click="saveUser" class="btn btn-success">Submit</button>
      </div>
  
      <div v-else>
        <h4>You submitted successfully!</h4>
        <button class="btn btn-success" @click="newTutorial">Add</button>
      </div>
    </div>
  </template>
  
  <script>
  import UserDataService from "../services/UserDataService";
  
  export default {
    name: "add-user",
    data() {
      return {      
        user: {
          id: null,
          userName: "",
          email: "",
          phoneNumber: "",
          skill: "",
          hobby: "",
          fileName: ""
        },
        url:null,
        selectedFile: null,
        submitted: false
      };
    },
    methods: {
      saveUser() {
        var data = {
          userName: this.user.userName,
          email: this.user.email,
          phoneNumber: this.user.phoneNumber,
          skill: this.user.skill,
          hobby: this.user.hobby,
          selectedFile: this.selectedFile
        };
       
        UserDataService.create(data)
          .then(response => {
           //this.user.id = response.data.id;
            console.log(response.data);
            this.submitted = true;
          })
          .catch(e => {
            console.log(e);
          });
      },
      
      newUser() {
        this.submitted = false;
        this.user = {};
        this.url = null;
      },
      async handleFileUpload(event) {
                console.log('selected file is clicked');
                const file = event.target.files[0];
                this.url = URL.createObjectURL(file);
                this.selectedFile = file;
                console.log('selected file length: ' + this.selectedFile);
                //display preview image               
            }
    }
  };
  </script>
  
  <style>
  .submit-form {
    max-width: 300px;
    margin: auto;
  }
  </style>
  