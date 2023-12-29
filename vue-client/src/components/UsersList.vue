<template>
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
            <div> <button @click="openModal" class="btn btn-primary">Add User</button> </div>
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
</template>

<script>
import UserDataService from '../services/UserDataService';
import UserDetails from './UserDetails.vue';

export default {
  name: "Users-list",
 components:{
  UserDetails,
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
    };
  },
  methods: {
    openModal(){
      this.$refs.AddUser.isOpen = true;
    },
   async retrieveUsers() {
        console.log('get method is called');
        UserDataService.getAll()
        .then(response => {
         
          console.log(response.data);
          this.users = response.data.data;
          this.status = response.data.status;
          console.log(this.status);
          console.log('data' + this.users);
          console.log(response.data.data);
        })
        .catch(e => {
          console.log('[exception]->'+e);
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
