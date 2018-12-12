import Vue from 'vue'
import Router from 'vue-router'
import App from './App.vue'
import Category from './components/Category.vue'
import Lot from './components/Lot.vue'
import AuctionList from './pages/AuctionList.vue'
import Register from './pages/Register.vue'
import Login from './pages/Login.vue'
import axios from 'axios'

Vue.use(Router);
axios.defaults.withCredentials = true;

const routes = [
    { name: 'Main', path: '/', component: AuctionList },
    { name: 'Categories', path: '/category/:id', component: Category },
    { name: 'Lots', path: '/lot/:id', component: Lot },
    { name: 'Register', path: '/register', component: Register },
    { name: 'Login', path: '/login', component: Login }
]

const router = new Router({
    mode: 'history',
    routes: routes
})

new Vue({
    el: '#app',
    router,
    render: h => h(App)
})