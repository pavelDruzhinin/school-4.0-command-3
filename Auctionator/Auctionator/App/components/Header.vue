<template>
    <!--<nav class="navbar navbar-expand-lg navbar-light bg-light">
        <div>
            <router-link to="/" class="navbar-brand">

                <img src="https://crimea-news.com/img/20180718/344102b2345b120a47feea2de58fa131.jpg" width="30" height="30" class="d-inline-block align-top" alt="">
                Auctionator
            </router-link>
        </div>

        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav mr-auto">
                <li class="nav-item dropdown">
                    <router-link to="/" class="nav-link dropdown-toggle text-primary" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        Categories
                    </router-link>
                    <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                        <router-link to="/" class="dropdown-item">Category1</router-link>
                        <router-link to="/" class="dropdown-item">Category2</router-link>
                        <router-link to="/" class="dropdown-item">Category3</router-link>
                    </div>
                </li>

                <li class="nav-item active">
                    <router-link to="/" class="nav-link" href="#">Home <span class="sr-only">(current)</span></router-link>
                </li>
                <li class="nav-item active">
                    <router-link to="/register" class="nav-link" href="#">Регистрация</router-link>
                </li>
                <li class="nav-item active">
                    <router-link to="/login" class="nav-link" href="#">Вход</router-link>
                </li>

                <li class="nav-item">
                    <router-link to="/" class="nav-link disabled" href="#">Disabled</router-link>
                </li>
            </ul>

            <form class="form-inline my-2 my-lg-0">
                <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search">
                <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
            </form>
            <div v-if="isAuth" class="alert alert-danger alert-dismissible fade show" role="alert">
                ЗАРЕГАН ТЫ!!!
                <button v-on:click="logout" class="btn btn-outline-success my-2 my-sm-0" type="submit">Выйти нафиг</button>
            </div>
            <div v-else class="alert alert-danger alert-dismissible fade show" role="alert">АВТОРИЗИРУЙСЯЫ!!!</div>
        </div>
    </nav>-->





    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <router-link to="/" class="navbar-brand">Аукционатор</router-link>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNavDropdown">
            <ul class="navbar-nav">
                <li class="nav-item active">
                    <router-link to="/" class="nav-link">Аукционы <span class="sr-only">(current)</span></router-link>
                </li>
                <li class="nav-item">
                    <router-link to="/create" class="nav-link">Добавить товар</router-link>
                </li>
                <li v-if="isAuth" class="nav-item dropdown">
                    <a href="#" class="nav-link dropdown-toggle" id="navbarDropdownMenuLink" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        Личный кабинет
                    </a>
                    <div class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                        <router-link to="/cabinet/products" class="dropdown-item">Мои товары</router-link>
                        <router-link to="/cabinet/for-payment" class="dropdown-item">Товары, ожидающие оплату</router-link>
                        <router-link to="/cabinet/purchases" class="dropdown-item">Мои покупки</router-link>
                        <span v-on:click="logout" class="dropdown-item">Выйти</span>
                    </div>
                </li>

                <li v-else class="nav-item">
                    <router-link to="/login" class="nav-link">Войти</router-link>
                </li>
            </ul>
        </div>
    </nav>


</template>

<script>
    import routes from '../index.js'
    import getIsAuth from '../auth.js'
    import axios from "axios"
    export default {
        components: {
            routes
        },
        data: function () {
            return {
                isAuth: getIsAuth()
            }
        },
        created() {
            // загружаем данные, когда представление создано
            // и данные реактивно отслеживаются
            this.updateAuth()
        },
        watch: {
            // при изменениях маршрута запрашиваем данные снова
            '$route': 'updateAuth'
        },
        methods: {
            updateAuth() {
                this.isAuth = getIsAuth()
            },
            logout() {
                axios.get('/logout')
                    .then(response => {
                        console.log(response) // TODO: убрать ненужное
                        alert('Выход был выполнен') // TODO: заменить на нормальное диалоговое окно или всплывающее сообщение
                        this.$router.push('/')
                    })
                    .catch(response => {
                        console.log(response) // TODO: убрать ненужное
                    })
                localStorage.removeItem('user')
                this.updateAuth()
            }
        }
    }
</script>