import { createWebHistory, createRouter } from "vue-router";

const routes =  [
  {
    path: "/",
    alias: "/users",
    name: "users",
    component: () => import("./components/UsersList")
  },
//   {
//     path: "/Users/:id",
//     name: "user-details",
//     component: () => import("./components/User")
//   },
//   {
//     path: "/add",
//     name: "add",
//     component: () => import("./components/AddUsers")
//   }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;