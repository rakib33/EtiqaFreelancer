<template>
  <form @submit.prevent="submitForm">
    <input type="text" v-model="user.id" hidden/>
    <div class="submit-form">
      <div class="container" v-if="!submitted">
        <div class="form-group mb-3 mt-3">
          <!-- :validator="validateUsername" -->
          <InputTextField id="userName" label="User Name"  v-model="user.userName" :maxLength="30" :minLength="5" :required=true></InputTextField>
        </div>

        <div class="form-group mb-3 mt-3">
         <EmailInputField id="email" label="Email" v-model="user.email" required=true is-email-valid=false placeholder="Enter email address"></EmailInputField>
        </div>

        <div class="form-group mb-3 mt-3">
         <!-- <PhoneNumberInputField v-model="user.phoneNumber" placeholder="Enter phone number"></PhoneNumberInputField> -->
         <TelephoneInputField v-model="user.phoneNumber" id="telephone" label="Telephone"></TelephoneInputField>
        </div>
        <div class="form-group mb-3 mt-3">
          <TextAreaInputField label="Skill" :required="true" :maxLength="100" :minLength="5" placeholder="Enter skill" v-model="user.skill"></TextAreaInputField>         
        </div>
        <div class="form-group mb-3 mt-3">
          <TextAreaInputField label="Hobby" :required="true" :maxLength="100" :minLength="5" placeholder="Enter hobby" v-model="user.hobby"></TextAreaInputField>  
        </div>
        <div class="form-group mb-3">         
          <FileUploadField label="Upload File" :isMultiple="false" accept="image/*" placeholder="Enter user image." :required="true" @file-selected="handleFileSelected"></FileUploadField>
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
  import InputTextField from '@/formComponent/InputTextField.vue';
  import EmailInputField from '@/formComponent/EmailInputField.vue';
  //import PhoneNumberInputField from '@/formComponent/PhoneNumberInputField.vue';
  import TelephoneInputField from '@/formComponent/TelephoneInputField.vue';
  import TextAreaInputField from '@/formComponent/TextAreaInputField.vue';
  import FileUploadField from '@/formComponent/FileUploadField.vue';
 // import { useVuelidate } from '@vuelidate/core';
  import UserDataService from "../services/UserDataService";

  export default {
    components:{
      InputTextField,
      EmailInputField,
      //PhoneNumberInputField,
      TelephoneInputField,
      TextAreaInputField,
      FileUploadField,
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
        uploadedFile: null,
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
          uploadedFile: this.uploadedFile,
        };

        console.log('Submit Form: username: ' + this.user.userName + ' email:' + this.user.email + 'phone: '+this.user.phoneNumber+' skill:'+this.user.skill+' hobby :'+this.user.hobby);
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
      async handleFileSelected(selectedFile) {           
        this.uploadedFile = selectedFile;    
                console.log('selected file length: ' + this.uploadedFile);
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
