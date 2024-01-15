<template>
  <div id="app">
    <PageLoader />
    <nav class="navbar navbar-expand navbar-dark bg-dark">
      <router-link to="/" class="navbar-brand">Users</router-link>
      <div class="navbar-nav mr-auto">
        <li class="nav-item">
          <router-link to="/users" class="nav-link">Users</router-link>
        </li>
        <li class="nav-item">
          <router-link to="/add" class="nav-link">Add</router-link>
        </li>
      </div>
    </nav>

    <div class="container mt-3">
      <router-view />
    </div>
  </div>
</template>

<script>
import PageLoader from './components/PageLoader.vue';
export default {
  name: "app",
  components:{
    PageLoader,
  }
};
</script>