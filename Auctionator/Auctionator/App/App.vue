<template>
    <div>
        <header-vue></header-vue>
        <transition name="fade">
            <router-view></router-view>
        </transition>
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
            routes : routes
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
