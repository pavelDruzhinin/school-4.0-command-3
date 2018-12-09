import Vue from 'vue'
import App from './App.vue'
import Category from './components/Category.vue'
import Lot from './components/Lot.vue'
import Router from 'vue-router'
import Navbar from   './components/Navbar.vue'

Vue.use(Router)


const routes = [
    { name: 'Main', path: '/', component: App },
    { name: 'Categories', path: '/category/:id', component: Category },
    { name: 'Lots', path: '/lot/:id', component: Lot }
]

const router = new Router({
    mode: 'history',
    routes
})

new Vue({
    el: '#app',
    router,
    template: 
        `<div>
            <Navbar/>
            <div class="container body-content">
                <router-view></router-view>
                <hr/>
                <footer>
                    <p>Copyright 2018 - Auctionator. All rights reserved by Command - 3 team.</p>
                </footer>
            </div>
        </div>`,
    components: { App, Navbar },

    data: function() {
        return { isActive: true }
        console.log(this);
    },
    mount: function () {
        console.log(this);
        this.isActive = false

        this.$nextTick(function () {
            //this.isActive = false
        })
    }

})