<template>
<div class="card">
    <div class="card-header">
        <h4>{{ currentUser.userName }}</h4>
    </div>
    <div class="card-body">  
  
    <div>
      <label><strong>Email:</strong></label> 
      <label>{{ currentUser.email }}</label>
    </div>
    <div>
      <label><strong>Phone Number:</strong></label> <label> {{ currentUser.phoneNumber }}</label>
    </div>
    <div>
      <label><strong>Skill Sets:</strong></label> <label>{{ currentUser.skillSets }}</label>
    </div>
    <div>
      <label><strong>Hobby:</strong></label> <label> {{ currentUser.hobby }}</label>
    </div>
    <!-- <div>
      <label><strong>Status:</strong></label> {{ currentUser.published ? "Published" : "Pending" }}
    </div> -->

    <router-link :to="'/users/' + currentUser.id" class="badge badge-warning">Edit</router-link>
    </div>         
  </div>
</template>

<script>

//using props
export default{
    props:['currentUser'],
    setup(props){
        console.log('Selected User: '+ props.currentUser);
    }    
};
</script>