<template>
  <form @submit.prevent="submitForm">
    <input type="text" v-model="user.id" hidden/>
    <div class="submit-form">
      <div class="container" v-if="!submitted">
        <div class="form-group mb-3 mt-3">
          <!-- :validator="validateUsername" -->
          <InputText id="userName" label="User Name"  v-model="user.userName" :maxLength="30" :minLength="5" :required=true></InputText>
        </div>

        <div class="form-group mb-3 mt-3">
         <EmailInputField id="email" label="Email" v-model="user.email" required=true is-email-valid=false placeholder="Enter email address"></EmailInputField>
        </div>

        <div class="form-group mb-3 mt-3">
         <!-- <PhoneNumberInputField v-model="user.phoneNumber" placeholder="Enter phone number"></PhoneNumberInputField> -->
         <TelephoneInputField v-model="user.phoneNumber" id="telephone" label="Telephone"></TelephoneInputField>
        </div>
        <div class="form-group mb-3 mt-3">
          <TextAreaInputField :label="Skill" :required="true" :maxLength="100" :minLength="5" placeholder="Enter skill" v-model="user.skill"></TextAreaInputField>         
        </div>
        <div class="form-group mb-3 mt-3">
          <TextAreaInputField :label="Hobby" :required="true" :maxLength="100" :minLength="5" placeholder="Enter hobby" v-model="user.hobby"></TextAreaInputField>  
        </div>
        <div class="form-group mb-3">
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
        <button type="submit" class="btn btn-success mt-3">Submit</button>
        <!-- @click="saveUser" -->
      </div>

      <div v-else>
        <h4>{{ Message }}</h4>
        <!-- <button class="btn btn-success" @click="newTutorial">Add</button> -->
      </div>
    </div>
  </form>
  </template>

  <script>
  import InputText from '@/formComponent/InputText.vue';
  import EmailInputField from '@/formComponent/EmailInputField.vue';
  //import PhoneNumberInputField from '@/formComponent/PhoneNumberInputField.vue';
  import TelephoneInputField from '@/formComponent/TelephoneInputField.vue';
  import TextAreaInputField from '@/formComponent/TextAreaInputField.vue';
 // import { useVuelidate } from '@vuelidate/core';
  import UserDataService from "../services/UserDataService";

  export default {
    components:{
      InputText,
      EmailInputField,
      //PhoneNumberInputField,
      TelephoneInputField,
      TextAreaInputField,
    },
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
        submitted: false,
        Message:""
      };
    },

    methods: {
      validateUsername(userName){
        console.log('validate user name is clicked');
        const alphanumericRegex = /^[a-zA-Z0-9]+$/;
        return alphanumericRegex.test(userName);

      },
      submitForm() {

        var data = {
          userName: this.user.userName,
          email: this.user.email,
          phoneNumber: this.user.phoneNumber,
          skill: this.user.skill,
          hobby: this.user.hobby,
          selectedFile: this.selectedFile
        };

        console.log('Submit Form: username: ' + this.user.userName + ' email:' + this.user.email);
        console.log(' Phone Number:' + this.user.phoneNumber);
        UserDataService.create(data)
          .then(response => {
           //this.user.id = response.data.id;
            console.log(response.data);
            this.submitted = true;
            this.Message = "submitted successfully!";
             // Emit a custom event to notify the parent to refresh the list.
            console.log('Form submitted! call parent refreshList.');
            this.newUser();
            this.$emit('refreshList');
          })
          .catch(e => {
            this.newUser();
            console.log(e);
            alert("Error:" + e);
          });

      },


      newUser() {
        console.log('New user method is called.');
        this.submitted = false;
        this.user = {};
        this.url = null;
        this.file = null;
      },
      async handleFileUpload(event) {
                console.log('selected file is clicked');
                const file = event.target.files[0];
                this.url = URL.createObjectURL(file);
                this.selectedFile = file;
                console.log('selected file length: ' + this.selectedFile);
                //display preview image
            }
    },
    mounted(){
      console.log('mounted new user is called');
      this.newUser();
    }
  };
  </script>

  <style>
  .submit-form {
    max-width: 300px;
    margin: auto;
  }
  </style>
