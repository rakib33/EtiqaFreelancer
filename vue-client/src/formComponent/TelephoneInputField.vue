<template>
     <div class="form-group mb-3 mt-3">
        <label :for="id">{{ label }}</label>        
        <vue-tel-input
        class="form-control telephone-field"
        :id="id"
        :name="id"        
        v-model="phone"
        v-bind="bindProps"
        @input="updateInputValue"    
        @validate="phoneObject"    
        mode="international">
        </vue-tel-input>
        <div v-if="inputTouch&&!isValid" class="error-message">{{ error }}</div>
    </div>
</template>
<script>
  import { VueTelInput } from 'vue-tel-input';
  import 'vue-tel-input/vue-tel-input.css';
export default {
    components:{
        VueTelInput,
    },
    props:{
        id: {
      type: String,
      required: true,
    },
    label: {
      type: String,
      required: true,
    },
    className:{
     type: String,
     default:'',
    },
    required:{
      type: Boolean, default: false
    },
    validator: {
      type: Function,
      default: () => () => false,
    },
    placeholder:{
      type: String,
      required:false,
      default:'',
    }, 
    
    },
    data(){
        return{
            phone:'',
            error:'',
            inputTouch:false,
            isValid:false,
            bindProps:{
                mode: "international",
                defaultCountry: "BD",
                // disabledFetchingCountry: false,
                // disabled: false,
                // disabledFormatting: false,
                // placeholder: "Enter a phone number",
                // required: true,
                // enabledCountryCode: true,
                // enabledFlags: true,
                preferredCountries: ["USA", "BD"],
                // onlyCountries: [],
                // ignoredCountries: [],
                // autocomplete: "on",
                // name: "telephone",
                // maxLen: 25,
                // wrapperClasses: "",
                // inputClasses: "",
                // dropdownOptions: {
                // disabledDialCode: true,
                
                // },
                inputOptions: {
                showDialCode: true
                },
                
            }
        }
    },
    
    methods:{
        updateInputValue() {
            this.inputTouch = true;
      console.log('inputed value:'+this.phone + ' input touch: '+this.inputTouch);
      this.$emit('update:modelValue', this.phone);
    },
        phoneObject: function(object) {
        console.log('phoneObject: ' +object.valid);
        this.isValid = object.valid;
        if(object.valid === true)
          {
            this.error ='';
          }else{
            this.error='Invalid phone number.';
          }
        }
    }
   
}
</script>
<style scoped>
.is-invalid {
  border: 1px solid red;
}
.error-message {
  color: red;
  font-size: 0.8em;
  margin-top: 0.2em;
}
</style>