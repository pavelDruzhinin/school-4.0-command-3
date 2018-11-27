import Vue from 'vue'
import AuctionItem from './components/AuctionItem.vue'
import App from './App.vue'

Vue.component('auctionItem', AuctionItem)

new Vue({
    el: '#app',
    template: '<div><App/></div>',
    components: {
        'App': App
    },
    data: {
        items: [
            { message: 'Foo' },
            { message: 'Bar' },
            { message: App }
        ]
    }
});