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
                    <th></th>
                </tr>
            </thead>
            <tbody>          
                <tr :class="{ active: index == currentIndex}" v-for="(user,index) in paginatedUser" 
                @click="setActiveUser(user, index)" :key="index">
                    <td>{{index + this.startIndex +1}}</td>
                    <td>{{user.userName}}</td>
                    <td>{{user.email}}</td>
                    <td>{{user.phoneNumber}}</td>
                    <td>{{user.skillSets}}</td>
                    <td>{{user.hobby}}</td>                 
                    <!--<td> <button class="btn btn-primary" data-toggle="modal" data-target="#myModal" @click="UpdateUser(user.id,index)">Update User</button></td>-->
                    <td> 
                      <button @click.stop="deleteNewUser(user.id)" class="btn btn-danger">
                        Delete
                      </button>
                    </td>
                </tr>
            </tbody>
          </table>
         <p v-else> No user available</p>
         <custom-pagination v-if="Ispagination" :currentPage="currentPage" :totalPages="totalPages" :changePage="changePage"></custom-pagination>
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
import CustomPagination from '../formComponent/CustomPagination.vue';
import UserDataService from '../services/UserDataService';
import UserDetails from './UserDetails.vue';
import AddUser from './AddUser.vue';
import userData from '@/data/userData.json';
export default {
  name: "Users-list",
 components:{
    UserDetails,
    AddUser,
    CustomPagination,
},
  data() {
    return {
      currentPage:1,
      totalPages:5,
      itemPerPage:5,
      startIndex:0,
      users: [],
      paginatedUser:[],
      currentUser: null,
      currentIndex: -1,
      key: "",
      selectedFile: null,
      url:null,
      status: '',
      isLoading:false,
      progress:0,
      Ispagination:false,
    };
  },
  methods: {

    retrieveUsers() {
        console.log('retrieveUsers get method is called');
        //this.getUserDataJsonFile();
        //this.getUserDataFromServer();
        UserDataService.getAll()
        .then(response => {
         
          console.log(response.data);
          this.users = response.data.data;
          this.status = response.data.status;
          console.log(this.status);
         
          console.log(response.data.data);
          if(this.users){
            console.log('data' + this.users);
            this.calculateTotalPage();
            this.changePage(this.currentPage); //inial page 1   
            this.Ispagination = true;
          }
        })
        .catch(e => {
          console.log('[exception]->'+e);
        });
    },
   async getUserDataJsonFile(){
          this.users = userData.data;
          this.status = userData.status;
          console.log(this.status);
          console.log('data' + this.users);
          console.log(userData.data.data);     
    },
    getUserDataFromServer(){
        UserDataService.getAll()
        .then(response => {         
          console.log(response.data);
          this.users = response.data.data;
          this.status = response.data.status;
          console.log(this.status);
          console.log('data: ' + this.users);
          console.log(response.data.data);
        })
        .catch(e => {
          console.log('[exception]->'+e);   
          console.error("API request failed:", e.error);
        });
    },
    deleteNewUser(id) {
              console.log('id to be deleted' + id);       
             
               UserDataService.delete(id).then(
                    (response) => {
                        console.log(response.data)                       
                        this.status = response.data.status
                        if (this.status === 'Success') {  
                            this.removeObjectFromArray(id);    
                            this.calculateTotalPage(); 
                            this.calculatePaginatedUsers();   
                            alert("Delete Successful");
                        }                          
                        else
                            alert('Delete failed');
                    }
                )


            },
  removeObjectFromArray(idtoRemove){
    console.log('remove object from array list id: '+ idtoRemove);
    //filter will take all data excluding id of removal object
    this.users = this.users.filter(obj => obj.id !== idtoRemove);
   
    //this.setActiveUser(null,-1);
  },
  refreshList() {
    console.log('Refresh List is called!');
      this.retrieveUsers();     
      //this.setActiveUser(null,-1);
      //this.changePage(this.currentPage);
    },

    setActiveUser(user, index) {
      this.currentUser = user;
      this.currentIndex = user ? index : -1;
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
          this.users = response.data.data;
          this.setActiveUser(null);
          console.log('filtered user data : '+this.users);
        })
        .catch(e => {
          console.log(e);
        });
    },
     // Your method for changing the page in the table
     changePage(page) {
      console.log('change page is hited: '+ page);
      // Update your table data based on the selected page
      // For example, fetch data from an API with the new page number
      this.currentPage = page;
      // ... fetch data based on the new page
      this.calculatePaginatedUsers();
    },
    getUsersLength() {
      // Get the length of the users array
      const length = this.users.length;
      console.log("Number of users:", length);
      return length;
    },
    calculateTotalPage(){
      this.totalPages = Math.ceil(this.getUsersLength() / this.itemPerPage);
      console.log(' total page: '+ this.totalPages);
    },
    calculatePaginatedUsers() {
      if(this.currentPage > this.totalPages)
        this.currentPage = this.totalPages;
      console.log('total page: '+ this.totalPages+' current page :'+ this.currentPage);
      this.startIndex = this.currentPage > 1 ? (this.currentPage - 1) * this.itemPerPage : 0;
      console.log('Item per page'+this.itemPerPage);
      const endIndex = this.startIndex + this.itemPerPage;
      console.log('start index:'+ this.startIndex + ' endIndex: '+endIndex);
      console.log('paginate user data : '+this.users);
      this.paginatedUser = this.users.slice(this.startIndex, endIndex);
      console.log('paginated user: '+  this.paginatedUser);
      this.currentUser = this.paginatedUser[0];
    }
  },
  mounted() {
    this.retrieveUsers();   
    // this.calculateTotalPage();
    // this.changePage(1);    
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
