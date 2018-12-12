<template>
    <div>
        <header-vue></header-vue>
        <router-link to="/category/1">category1</router-link>
        <router-link to="/category/2">category2</router-link>
        <router-view></router-view>
        <hr /><footer>
            <p>Copyright 2018 - Auctionator. All rights reserved by Command - 3 team.</p>
        </footer>
    </div>
</template>

<script>
    import Header from './components/Header.vue'
    import routes from './index.js'
    export default {
        components: {
            'header-vue': Header,
            routes
        },
    }
</script>
