<template>
     <div class="form-group mb-3 mt-3">
        <label :for="id">{{ label }}</label>        
        <vue-tel-input
        class="form-control telephone-field"
        :id="id"
        :name="id"
        :class="className"
        v-model="phone" 
        @input="updateInputValue"        
        mode="international">
        </vue-tel-input>
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
    methods:{
        updateInputValue() {
      console.log('inputed value:'+this.phone);
      this.$emit('update:modelValue', this.phone);
    },
    }
   
}
</script>