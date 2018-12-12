import Vue from 'vue';
import Router from 'vue-router';
import App from './App.vue';
import Lot from './components/Lot.vue';
import Register from './components/Register.vue';

Vue.use(Router);


const routes = [
    { name: 'Main', path: '/', component: App },
    { name: 'Lots', path: '/lot/:id', component: Lot },
    { name: 'Register', path: '/register', component: Register }
];

const router = new Router({
    mode: 'history',
    routes
});

new Vue({
    el: '#app',
    router,
    template: 
        `<div>
            <router-view></router-view>
            <hr/>
            <footer>
                <p>Copyright 2018 - Auctionator. All rights reserved by Command - 3 team.</p>
            </footer>
        </div>`,
    components: { App },

    data: function() {
        return { isActive: true };
    },
    mount: function () {
        this.isActive = false;

        this.$nextTick(function () {
            //this.isActive = false
        });
    }

})