<template>
  <loading-bar :is-loading="isLoading" :progress="progress"></loading-bar>
  <div class="row">
    <div class="col-md-9">
      <div class="input-group mb-3">
        <input type="text" class="form-control" placeholder="Search by key"
          v-model="key"/>
        <div class="input-group-append">
          <button class="btn btn-outline-secondary" type="button"
            @click="searchByKey">                 
           Search
          </button>
        </div>
      </div>
    </div>
    <div class="col-md-9 col-sm-9">
      <div class="card">
        <div class="card-header">
          <div class="col-md-4 flexContainer">
            <div> <h4>Users List</h4></div>
            <div> <button class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#myModal">Add User</button> </div>
          </div>
        </div>
        <div class="card-body">
          <table v-if="users" class="table table-hover">
            <thead>
                <tr>
                    <th></th>
                    <th>User Name</th>
                    <th>Email</th>
                    <th>Phone Number</th>
                    <th>Skills</th>
                    <th>Hobby</th>                   
                    <th>File Name</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>          
                <tr :class="{ active: index == currentIndex}" v-for="(user,index) in users" 
                @click="setActiveUser(user, index)" :key="index">
                    <td>{{index +1}}</td>
                    <td>{{user.userName}}</td>
                    <td>{{user.email}}</td>
                    <td>{{user.phoneNumber}}</td>
                    <td>{{user.skillSets}}</td>
                    <td>{{user.hobby}}</td>
                    <td>{{user.fileName}}</td>
                    <!--<td> <button class="btn btn-primary" data-toggle="modal" data-target="#myModal" @click="UpdateUser(user.id,index)">Update User</button></td>-->
                    <td> <button @click.stop="deleteNewUser(user.id)" class="btn btn-danger">
                      Delete</button></td>
                </tr>
            </tbody>
          </table>
         <p v-else> No user available</p>
        </div>
      </div>
    </div>
    <div class="col-md-3 col-sm-3">
      <div v-if="currentUser">
        <user-details v-bind:currentUser="currentUser"></user-details>
      </div>
      <div v-else>
        <div class="card">
          <div class="card-header">
            <p>Please click on a user...</p>
          </div>
        </div>
      </div>
    </div>

  </div>
  <!-- The Modal class="modal" -->
  <div id="myModal" class="modal fade" tabindex="-1" aria-hidden="true" data-bs-backdrop="static">
            <div class="modal-dialog">
                <div class="modal-content">

                    <!-- Modal Header -->
                    <div class="modal-header">
                        <h4 class="modal-title">Create a New User</h4>
                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                    </div>

                    <!-- Modal body -->
                    <div class="modal-body">                     
                      <add-user @refreshList="refreshList"></add-user>
                    </div>

                    <!-- Modal footer -->
                    <!-- <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                    </div> -->

                </div>
            </div>
  </div>
</template>

<script>
import UserDataService from '../services/UserDataService';
import UserDetails from './UserDetails.vue';
import AddUser from './AddUser.vue';
import LoadingBar from './LoadingBar.vue';

export default {
  name: "Users-list",
 components:{
    UserDetails,
    AddUser,
    LoadingBar,
},
  data() {
    return {
      users: [],
      currentUser: null,
      currentIndex: -1,
      key: "",
      selectedFile: null,
      url:null,
      status: '',
      isLoading:false,
      progress:0,
    };
  },
  methods: {
    openModal(){
      this.$refs.AddUser.isOpen = true;
    },
    getAllData() {
      // Simulating an API request (replace this with your actual API call)
      return new Promise((resolve) => {
        const interval = setInterval(() => {
          this.progress += 2; // You can adjust the increment value
          if (this.progress >= 100) {
            clearInterval(interval);
            resolve();
          }
        }, 100); // You can adjust the interval time
      });
    },
   async retrieveUsers() {
        console.log('get method is called');
        this.isLoading = true;
        this.progress = 0;
        
        UserDataService.getAll()
        .then(response => {         
          console.log(response.data);
          this.users = response.data.data;
          this.status = response.data.status;
          console.log(this.status);
          console.log('data' + this.users);
          console.log(response.data.data);

           // API request completed
          this.isLoading = false;
          alert("API request completed!");
        })
        .catch(e => {
          console.log('[exception]->'+e);
           // Handle API request error
           this.isLoading = false;
          console.error("API request failed:", e.error);
        });
    },
    async deleteNewUser(id) {
                console.log('id to be deleted' + id);
               UserDataService.delete(id).then(
                    (response) => {
                        console.log(response.data)                       
                        this.status = response.data.status
                        if (this.status === 'Success') {
                            this.refreshList();
                            alert("Delete Successful");
                        }                          
                        else
                            alert('Delete failed');
                    }
                )


            },
  async  refreshList() {
    console.log('Refresh List is called!');
      this.retrieveUsers();
      this.currentUser = null;
      this.currentIndex = -1;
    },

    setActiveUser(tutorial, index) {
      this.currentUser = tutorial;
      this.currentIndex = tutorial ? index : -1;
    },

    removeAllUsers() {
        UserDataService.deleteAll()
        .then(response => {
          console.log(response.data);
          this.refreshList();
        })
        .catch(e => {
          console.log(e);
        });
    },
    
    searchByKey() {
        UserDataService.findByKey(this.key)
        .then(response => {
          //this.users = response.data;
          this.users = response.data.data;
          this.setActiveUser(null);
          console.log('folered user data : '+this.users);
        })
        .catch(e => {
          console.log(e);
        });
    }
  },
  mounted() {
    this.retrieveUsers();
  }
};
</script>

<style>
.list {
  text-align: left;
  max-width: 750px;
  margin: auto;
}
</style>
