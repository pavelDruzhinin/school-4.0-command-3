<template>
    <div>
        <header-vue></header-vue> <!--Соединить Header и Navbar!!!--> 
        <!--<Navbar />-->
        <div class="container body-content">
            <transition name="fade">
                <router-view></router-view>
            </transition>
            <hr /><footer>
                <p>Copyright 2018 - Auctionator. All rights reserved by Command - 3 team.</p>
            </footer>
        </div>
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

<style>
       .fade-enter-active, .fade-leave-active {
  transition-property: opacity;
  transition-duration: .25s;
}

.fade-enter-active {
  transition-delay: .25s;
}

.fade-enter, .fade-leave-active {
  opacity: 0
}
</style>
