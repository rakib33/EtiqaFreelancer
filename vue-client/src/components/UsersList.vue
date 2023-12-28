<template>
  <div class="row">
    <div class="col-md-8">
      <div class="input-group mb-3">
        <input type="text" class="form-control" placeholder="Search by key"
          v-model="key"/>
        <div class="input-group-append">
          <button class="btn btn-outline-secondary" type="button"
            @click="searchByKey"
          >
          <font-awesome-icon icon="search" />
           Search
          </button>
        </div>
      </div>
    </div>
    <div class="col-md-10">
      <h4>Users List</h4>
      <button @click="openModal">Add User</button>     
      <table class="table table-hover">
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
              <!-- @click="setActiveUser(user, index)" -->
                <tr :class="{ active: index == currentIndex}" v-for="(user,index) in users" 
                :key="index">
                    <td>{{index +1}}</td>
                    <td>{{user.userName}}</td>
                    <td>{{user.email}}</td>
                    <td>{{user.phoneNumber}}</td>
                    <td>{{user.skillSets}}</td>
                    <td>{{user.hobby}}</td>
                    <td>{{user.fileName}}</td>
                    <!--<td> <button class="btn btn-primary" data-toggle="modal" data-target="#myModal" @click="UpdateUser(user.id,index)">Update User</button></td>-->
                    <td> <button @click="deleteNewUser(user.id)" class="btn btn-danger">
                      <font-awesome-icon icon="trash" style="color: red;"></font-awesome-icon></button></td>
                </tr>
            </tbody>
        </table>

    </div>
    <div class="col-md-2">
      <div v-if="currentUser">
        <h4>User</h4>
        <div>
          <label><strong>User Name:</strong></label> {{ currentUser.userName }}
        </div>
        <div>
          <label><strong>Email:</strong></label> {{ currentUser.email }}
        </div>
        <div>
          <label><strong>Phone Number:</strong></label> {{ currentUser.phoneNumber }}
        </div>
        <div>
          <label><strong>Skill Sets:</strong></label> {{ currentUser.skillSets }}
        </div>
        <div>
          <label><strong>Hobby:</strong></label> {{ currentUser.hobby }}
        </div>
        <!-- <div>
          <label><strong>Status:</strong></label> {{ currentUser.published ? "Published" : "Pending" }}
        </div> -->

        <router-link :to="'/users/' + currentUser.id" class="badge badge-warning">Edit</router-link>
      </div>
      <div v-else>
        <br />
        <p>Please click on a user...</p>
      </div>
    </div>
  </div>
</template>

<script>


import UserDataService from '../services/UserDataService';

export default {
  name: "Users-list",
 
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
        UserDataService.findByTitle(this.title)
        .then(response => {
          this.users = response.data;
          this.setActiveUser(null);
          console.log(response.data);
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
