"use strict";(self["webpackChunkvue_client"]=self["webpackChunkvue_client"]||[]).push([[843],{2843:function(e,s,l){l.r(s),l.d(s,{default:function(){return Q}});var t=l(3396),r=l(9242),a=l(7139);const n={class:"row"},o={class:"col-md-9"},i={class:"input-group mb-3"},d={class:"input-group-append"},c={class:"col-md-9 col-sm-9"},u={class:"card"},h=(0,t._)("div",{class:"card-header"},[(0,t._)("div",{class:"col-md-4 flexContainer"},[(0,t._)("div",null,[(0,t._)("h4",null,"Users List")]),(0,t._)("div",null,[(0,t._)("button",{class:"btn btn-primary","data-bs-toggle":"modal","data-bs-target":"#myModal"},"Add User")])])],-1),_={class:"card-body"},g={key:0,class:"table table-hover"},b=(0,t._)("thead",null,[(0,t._)("tr",null,[(0,t._)("th"),(0,t._)("th",null,"User Name"),(0,t._)("th",null,"Email"),(0,t._)("th",null,"Phone Number"),(0,t._)("th",null,"Skills"),(0,t._)("th",null,"Hobby"),(0,t._)("th",null,"File Name"),(0,t._)("th")])],-1),v=["onClick"],m=["onClick"],p={key:1},y={class:"col-md-3 col-sm-3"},U={key:0},k={key:1},w=(0,t._)("div",{class:"card"},[(0,t._)("div",{class:"card-header"},[(0,t._)("p",null,"Please click on a user...")])],-1),f=[w],L={id:"myModal",class:"modal fade",tabindex:"-1","aria-hidden":"true","data-bs-backdrop":"static"},D={class:"modal-dialog"},N={class:"modal-content"},z=(0,t._)("div",{class:"modal-header"},[(0,t._)("h4",{class:"modal-title"},"Create a New User"),(0,t._)("button",{type:"button",class:"btn-close","data-bs-dismiss":"modal"})],-1),A={class:"modal-body"};function I(e,s,l,w,I,C){const S=(0,t.up)("loading-bar"),x=(0,t.up)("user-details"),Z=(0,t.up)("add-user");return(0,t.wg)(),(0,t.iD)(t.HY,null,[(0,t.Wm)(S,{"is-loading":I.isLoading,progress:I.progress},null,8,["is-loading","progress"]),(0,t._)("div",n,[(0,t._)("div",o,[(0,t._)("div",i,[(0,t.wy)((0,t._)("input",{type:"text",class:"form-control",placeholder:"Search by key","onUpdate:modelValue":s[0]||(s[0]=e=>I.key=e)},null,512),[[r.nr,I.key]]),(0,t._)("div",d,[(0,t._)("button",{class:"btn btn-outline-secondary",type:"button",onClick:s[1]||(s[1]=(...e)=>C.searchByKey&&C.searchByKey(...e))}," Search ")])])]),(0,t._)("div",c,[(0,t._)("div",u,[h,(0,t._)("div",_,[I.users?((0,t.wg)(),(0,t.iD)("table",g,[b,(0,t._)("tbody",null,[((0,t.wg)(!0),(0,t.iD)(t.HY,null,(0,t.Ko)(I.users,((e,s)=>((0,t.wg)(),(0,t.iD)("tr",{class:(0,a.C_)({active:s==I.currentIndex}),onClick:l=>C.setActiveUser(e,s),key:s},[(0,t._)("td",null,(0,a.zw)(s+1),1),(0,t._)("td",null,(0,a.zw)(e.userName),1),(0,t._)("td",null,(0,a.zw)(e.email),1),(0,t._)("td",null,(0,a.zw)(e.phoneNumber),1),(0,t._)("td",null,(0,a.zw)(e.skillSets),1),(0,t._)("td",null,(0,a.zw)(e.hobby),1),(0,t._)("td",null,(0,a.zw)(e.fileName),1),(0,t._)("td",null,[(0,t._)("button",{onClick:(0,r.iM)((s=>C.deleteNewUser(e.id)),["stop"]),class:"btn btn-danger"}," Delete",8,m)])],10,v)))),128))])])):((0,t.wg)(),(0,t.iD)("p",p," No user available"))])])]),(0,t._)("div",y,[I.currentUser?((0,t.wg)(),(0,t.iD)("div",U,[(0,t.Wm)(x,{currentUser:I.currentUser},null,8,["currentUser"])])):((0,t.wg)(),(0,t.iD)("div",k,f))])]),(0,t._)("div",L,[(0,t._)("div",D,[(0,t._)("div",N,[z,(0,t._)("div",A,[(0,t.Wm)(Z,{onRefreshList:C.refreshList},null,8,["onRefreshList"])])])])])],64)}var C=l(9442);const S={class:"card"},x={class:"card-header"},Z={class:"card-body"},P=(0,t._)("label",null,[(0,t._)("strong",null,"Email:")],-1),B=(0,t._)("label",null,[(0,t._)("strong",null,"Phone Number:")],-1),K=(0,t._)("label",null,[(0,t._)("strong",null,"Skill Sets:")],-1),H=(0,t._)("label",null,[(0,t._)("strong",null,"Hobby:")],-1);function M(e,s,l,r,n,o){const i=(0,t.up)("router-link");return(0,t.wg)(),(0,t.iD)("div",S,[(0,t._)("div",x,[(0,t._)("h4",null,(0,a.zw)(l.currentUser.userName),1)]),(0,t._)("div",Z,[(0,t._)("div",null,[P,(0,t._)("label",null,(0,a.zw)(l.currentUser.email),1)]),(0,t._)("div",null,[B,(0,t.Uk)(),(0,t._)("label",null,(0,a.zw)(l.currentUser.phoneNumber),1)]),(0,t._)("div",null,[K,(0,t.Uk)(),(0,t._)("label",null,(0,a.zw)(l.currentUser.skillSets),1)]),(0,t._)("div",null,[H,(0,t.Uk)(),(0,t._)("label",null,(0,a.zw)(l.currentUser.hobby),1)]),(0,t.Wm)(i,{to:"/users/"+l.currentUser.id,class:"badge badge-warning"},{default:(0,t.w5)((()=>[(0,t.Uk)("Edit")])),_:1},8,["to"])])])}var W={props:["currentUser"],setup(e){console.log("Selected User: "+e.currentUser)}},q=l(89);const E=(0,q.Z)(W,[["render",M]]);var R=E,F=l(9231);const Y={key:0,class:"loading-bar"};function $(e,s,l,r,n,o){return n.isLoading?((0,t.wg)(),(0,t.iD)("div",Y,[(0,t._)("div",{class:"bar",style:(0,a.j5)({width:`${n.progress}%`})},null,4)])):(0,t.kq)("",!0)}var j={data(){return{isLoading:!1,progress:0}},methods:{startLoading(){this.isLoading=!0,this.progress=0;const e=setInterval((()=>{this.progress+=2,this.progress>=100&&(clearInterval(e),this.isLoading=!1)}),100)}}};const O=(0,q.Z)(j,[["render",$],["__scopeId","data-v-0b92470b"]]);var V=O,G={name:"Users-list",components:{UserDetails:R,AddUser:F["default"],LoadingBar:V},data(){return{users:[],currentUser:null,currentIndex:-1,key:"",selectedFile:null,url:null,status:"",isLoading:!1,progress:0}},methods:{openModal(){this.$refs.AddUser.isOpen=!0},getAllData(){return new Promise((e=>{const s=setInterval((()=>{this.progress+=2,this.progress>=100&&(clearInterval(s),e())}),100)}))},async retrieveUsers(){console.log("get method is called"),this.isLoading=!0,this.progress=0,C.Z.getAll().then((e=>{console.log(e.data),this.users=e.data.data,this.status=e.data.status,console.log(this.status),console.log("data"+this.users),console.log(e.data.data),this.isLoading=!1,alert("API request completed!")})).catch((e=>{console.log("[exception]->"+e),this.isLoading=!1,console.error("API request failed:",e.error)}))},async deleteNewUser(e){console.log("id to be deleted"+e),C.Z.delete(e).then((e=>{console.log(e.data),this.status=e.data.status,"Success"===this.status?(this.refreshList(),alert("Delete Successful")):alert("Delete failed")}))},async refreshList(){console.log("Refresh List is called!"),this.retrieveUsers(),this.currentUser=null,this.currentIndex=-1},setActiveUser(e,s){this.currentUser=e,this.currentIndex=e?s:-1},removeAllUsers(){C.Z.deleteAll().then((e=>{console.log(e.data),this.refreshList()})).catch((e=>{console.log(e)}))},searchByKey(){C.Z.findByKey(this.key).then((e=>{this.users=e.data.data,this.setActiveUser(null),console.log("folered user data : "+this.users)})).catch((e=>{console.log(e)}))}},mounted(){this.retrieveUsers()}};const J=(0,q.Z)(G,[["render",I]]);var Q=J}}]);
//# sourceMappingURL=843.fc030ec1.js.map