<template>
<div class="form-group mb-3">
<label :for="id">{{ label }}</label>
<input type="file"
    class="form-control"
    id="file"
    :multiple="isMultiple"
    @change="handleFileChange"
    :accept="accept"
    :placeholder="placeholder"
    :required="required"
/>

<!-- accept="image/*" accept=".pdf, .doc, .docx"-->
<div class="col-sm-8 mt-3" id="preview">
<img :height="imgPreviewHeight" :width="imgPreviewWidth" style="justify-content:center; align-items:center;" v-if="url" :src="url" />
</div>
<span v-if="error" class="error-message">{{ error }}</span>
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

accept:{
type:String,
default:''
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
imgPreviewHeight:{
type:number,
default: 200,
},
imgPreviewWidth:{
type:number,
default: 200,
},
isValid:{
type:Boolean,
default:true,
}

},
data() {
return {
  isMultiple:false, 
  error:'',
  url:null,
};
},

methods: {

    async handleFileChange(event) {
                console.log('selected file is clicked');
                this.selectedFile = event.target.files;
                this.url = URL.createObjectURL(event.target.files[0]);
                
                 // Emit the selected file to the parent component
                this.$emit('file-selected', this.selectedFile);
                console.log('selected file length: ' + this.selectedFile);               
            },
   async clearImagePreview() {
      this.url = null;
    },
},
mounted(){
 //this.clearImagePreview();
}
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
