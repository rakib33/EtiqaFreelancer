<template>
    <div v-if="isLoading" class="loading-bar">
      <div class="bar" :style="{ width: `${progress}%` }"></div>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        isLoading: false,
        progress: 0,
      };
    },
    methods: {
      startLoading() {
        this.isLoading = true;
        this.progress = 0;
  
        const interval = setInterval(() => {
          this.progress += 2; // You can adjust the increment value
          if (this.progress >= 100) {
            clearInterval(interval);
            this.isLoading = false;
          }
        }, 100); // You can adjust the interval time
      },
    },
  };
  </script>
  
  <style scoped>
  .loading-bar {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 3px;
    background-color: #007bff; /* You can customize the color */
  }
  
  .bar {
    height: 100%;
    background-color: #4caf50; /* You can customize the color */
  }
  </style>
  