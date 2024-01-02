<template>
  <div>
    <label :for="id">{{ label }}</label>
    <input
      :id="id"
      v-model="inputValue"
      @input="updateInputValue"
      :class="{ 'is-invalid': !isValid }"
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
    validator: {
      type: Function,
      default: () => () => true,
    },
  },
  data() {
    return {
      inputValue: '',
    };
  },
  computed: {
    isValid() {
      return this.validator(this.inputValue);
    },
    errorMessage() {
      return this.isValid ? '' : `Invalid ${this.label.toLowerCase()}`;
    },
  },
  methods: {
    updateInputValue() {
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
