<!-- PhoneNumberInput.vue -->

<template>
    <div>
      <label for="phone">Phone Number:</label>
      <div class="phone-input">
        <input          
          v-model="countryCode"
          type="text"
          id="countryCode"
          name="countryCode"
          placeholder="+"
          class="form-control country-code"
        />
        <input
          v-model="phoneNumber"
          type="text"
          id="phoneNumber"
          name="phoneNumber"
          @input="validatePhoneNumber"
          class="form-control phone-number"
        />
        <input
          v-model="extension"
          type="text"
          id="extension"
          name="extension"
          placeholder="Ext"
          class="form-control extension"
        />
      </div>
      <div v-if="error" class="error-message">{{ error }}</div>
    </div>
  </template>
  
  <script>
  export default {

    data() {
      return {
        countryCode: '',
        phoneNumber: '',
        extension: '',
        error: '',
      };
    },
    methods: {
      validatePhoneNumber() {
        // Regular expressions for country code and phone number
        const countryCodeRegex = /^\+?[0-9]+$/;
        const phoneNumberRegex = /^[0-9]+$/;
  
        if (!countryCodeRegex.test(this.countryCode)) {
          this.error = 'Invalid country code';
        } else if (!phoneNumberRegex.test(this.phoneNumber)) {
          this.error = 'Invalid phone number';
        } else {
          this.error = '';
        }

        if(this.error === ''){
            // Emit an event with the input values
          this.$emit('phone-input', {
          countryCode: this.countryCode,
          phoneNumber: this.phoneNumber,
          extension: this.extension,
        }); 
        }
      },
    },
  };
  </script>
  
  <style scoped>
  .phone-input {
    display: flex;
  }
  
  .country-code,
  .phone-number,
  .extension {
    margin-right: 5px;
  }
  
  .error-message {
    color: red;
    margin-top: 5px;
  }
  </style>
  