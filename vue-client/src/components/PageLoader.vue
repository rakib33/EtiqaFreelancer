<template>
    <!-- <div class="page-loader" v-if="!isloaded">
      <div class="cube"></div>
      <div class="cube"></div>
      <div class="cube"></div>
      <div class="cube"></div>
    </div>  -->
    <div id="loading" v-if="!isloaded">
        <div>Page Loading...</div>
        <!-- <img id="loading-image" src="path/to/ajax-loader.gif" alt="Loading..." /> -->
    </div>
  </template>
  
  <script>
    export default {
      data: () => {
        return {
          isloaded: false
        }
      },
      mounted() {
        console.log('page loading mounted is called.');
        document.onreadystatechange = () => {
            console.log('page loading onreadystatechange is called.state: '+document.readyState);
          if (document.readyState == "complete") { 
            this.isloaded = true;
            console.log('page loading isloaded:'+ this.isloaded);
          } 
        }
      },
    }
  </script>
  
<style>
#loading {
  position: fixed;
  display: block;
  width: 100%;
  height: 100%;
  top: 0;
  left: 0;
  text-align: center;
  opacity: 0.7;
  background-color: #fff;
  z-index: 99;
}

#loading-image {
  position: absolute;
  top: 100px;
  left: 240px;
  z-index: 100;
}
</style>


  <!-- <style lang="scss" scoped>
     $colors: #8CC271, #69BEEB, #F5AA39, #E9643B;
  
    // -----------------------------------------------------
  
    .page-loader {
      display: flex;
      justify-content: center;
      align-items: center;
      position: fixed;
      top: 0;
      left: 0;
      width: 100vw;
      height: 100vh;
      background-color: #333;
      z-index: 999;
    }
  
    // -----------------------------------------------------
  
    .cube{
      width: 40px;
      height: 40px;
      margin-right: 10px;
  
      @for $i from 1 through length($colors) {
        &:nth-child(#{$i}) {
          background-color: nth($colors, $i);
        }
      }
  
      &:first-child {
        animation: left 1s infinite;
      }
  
      &:last-child {
        animation: right 1s infinite .5s;
      }
    }
  
    // -----------------------------------------------------
  
    @keyframes left {
      40% {
        transform: translateX(-60px);
      }
      50% {
        transform: translateX(0);      
      }
    }
  
    @keyframes right {
      40% {
        transform: translateX(60px);
      }
      50% {
        transform: translateX(0);
      }
    }
  </style> -->