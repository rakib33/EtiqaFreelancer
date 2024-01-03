<template>
          <div class="form-group mb-3 mt-3">
            <label :for="id">{{ label }}</label>
          <input
            class="form-control"
            :id="id"
            :name="id"
            :placeholder="placeholder"
            :class="{ 'is-invalid': !isValid }"
            :required="required"            
            v-model="inputValue"
            @input="updateInputValue"
            
          />
          <span v-if="!isValid" class="error-message">{{ errorMessage }}</span>
        </div>
  
</template>

<script>
import { number } from 'yup';

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
      default: 50
    },
    minLength:{
      type:number,
      default: 3
    },
    
  },
  data() {
    return {
      inputValue: '',
      //errorMessage:'',
    };
  },
  computed: {
    isValid(props) {
      const length = this.inputValue.length;
      console.log('input text field isValid is called!');
      if(this.validator(this.inputValue) && length>= props.minLength && length <= props.maxLength)
      {
        console.log('is valid true');
        return true;
      }
      else{
        console.log('return false');
        return false;
      }
    },
    errorMessage(props) {
      console.log('inputText error message is called.');
      return this.isValid ? '' : `Invalid ${this.label.toLowerCase()}.length minimum:${props.minLength.toLowerCase()} maximum:${props.maxLength.toLowerCase()}`;
    },
  },
  methods: {
    updateInputValue() {
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
