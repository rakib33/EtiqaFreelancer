<template>
         <div class="form-group mb-3 mt-3">
            <label :for="id">{{ label }}</label>
            <input
            class="form-control"
            v-model="email"
            type="email"
            :class="{ 'is-invalid': showError}"
            :placeholder="placeholder"
            :id="id"
            :name="id"
            :required="required"
            @input="validateEmail"
            />
            <div v-if="showError" class="error-message">{{ error }}</div>
         </div>   
</template>
<script>
export default{
    props: {
    id: {
      type: String,
      required: true,
    },
    label: {
      type: String,
      required: true,
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
    isEmailValid:{
      type:Boolean,
      default:false,
    }
  },
 data(){
    return{
        email:'',
        error:'',
        showError:false,
    };
 },
 methods:{
  // isValid(props){
  //   return props.isEmailValid;
  // },
    validateEmail(){
      console.log('émail valiadetEmail is called.');
         // Basic email validation using a regular expression
      const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      if(emailRegex.test(this.email)){
      this.error ='';
      this.showError = false;
    
      }else{
        this.showError = true;
        this.error = 'Invalid email address';
      }
    
      this.$emit('update:modelValue', this.email);
    },
 },

};
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