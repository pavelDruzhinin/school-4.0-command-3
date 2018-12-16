import Vue from 'vue'
import Router from 'vue-router'
import App from './App.vue'
import Lot from './pages/Lot.vue'
import AuctionList from './pages/AuctionList.vue'
import Payment from './pages/Payment.vue'
import Register from './pages/Register.vue'
import Login from './pages/Login.vue'
import axios from 'axios'
import VueSignalR from '@latelier/vue-signalr';


Vue.use(VueSignalR, 'http://localhost:5000/auctionHub');

Vue.use(Router);
axios.defaults.withCredentials = true;


const routes = [
    { name: 'Main', path: '/', component: AuctionList },
    { name: 'Lots', path: '/lot/:productId', props: true, component: Lot },
    { name: 'Payment', path: '/payment/:productId', props: true, component: Payment },
    { name: 'Register', path: '/register', component: Register },
    { name: 'Login', path: '/login', component: Login }
];

const router = new Router({
    mode: 'history',
    routes: routes
});

new Vue({
    el: '#app',
    router,
    render: h => h(App),
    created() {
        this.$socket.start({
            log: true // Active only in development for debugging.
        });
    }
});
