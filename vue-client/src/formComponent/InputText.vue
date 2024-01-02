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
    }
    
  },
  data() {
    return {
      inputValue: '',
      //errorMessage:'',
    };
  },
  computed: {
    isValid() {
      console.log('input text field isValid is called!');
      return this.validator(this.inputValue);
    },
    errorMessage() {
      console.log('inputText error message is called.');
      return this.isValid ? '' : `Invalid ${this.label.toLowerCase()}`;
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
