<template>
          <div class="form-group mb-3 mt-3">
            <label :for="id">{{ label }}</label>
          <input
            class="form-control"
            :id="id"
            :name="id"
            :placeholder="placeholder"
            :class="{ 'is-invalid': showError}"
            :required="required"            
            :minlength="minLength"
            :maxlength="maxLength"
            v-model="inputValue"
            @input="updateInputValue"
            
          />
          <!-- <span v-if="!isValid" class="error-message">{{ errorMessage }}</span> -->
          <span v-if="showError" class="error-message">{{ error }}</span>
        </div>
  
</template>

<script>
import { number, String } from 'yup';

export default {
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
    maxLength:{
      type:number,
      default: 100,
    },
    minLength:{
      type:number,
      default: 0,    
    },
    isValid:{
      type:Boolean,
      default:true,
    }
    
  },
  data() {
    return {
      inputValue: '',
      error:'',
      showError:false,
    };
  },
 
  methods: {
    updateInputValue() {
      const length = this.inputValue.length;
      console.log('input field length: '+ length);
      console.log('min length: '+ this.minLength + ' max length : ' + this.maxLength);
      if(length >= this.minLength && length <= this.maxLength) //this.validator(this.inputValue) && 
      {
        console.log('showError false');
        this.error ='';
        this.showError = false;
      }
      else{
        console.log('return false');
        this.showError = true;
        
        if(length <= this.maxLength)
        this.error = 'Please enter a minimum of '+ this.minLength+ ' characters.';
        else
        this.error = 'Text cannot exceed '+ this.maxLength+ ' characters.';
      }
      console.log('inputed value:'+this.inputValue);
      this.$emit('update:modelValue', this.inputValue);
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
