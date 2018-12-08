<template>
    <section class="section">
        <div class="container has-text-centered">
            <div class="column is-4 is-offset-4">
                <h3 class="title has-text-grey">Регистрация</h3>
                <p class="subtitle has-text-grey">Пожалуйста, заполните следующие поля</p>
                <div class="box">
                    <!-- @submit handles any form of submission. -->
                    <!-- .prevent keeps the event from bubbling around and doing anything else. -->
                    <form @submit.prevent="handleSubmit">
                        <div class="field">
                            <div class="control">
                                <input class="input is-large" type="email" placeholder="Введите E-mail" v-model="email" autofocus="">
                            </div>
                        </div>
                        <div class="field">
                            <div class="control">
                                <input class="input is-large" type="password" placeholder="Введите пароль" v-model="password">
                            </div>
                        </div>
                        <div class="field">
                            <div class="control">
                                <input class="input is-large" type="text" placeholder="Введите имя" v-model="name">
                            </div>
                        </div>
                        <button v-on:click="handleSubmit" class="button is-block is-info is-large is-fullwidth" type="submit">Зарегистрироваться</button>
                        <div class="errors-container" v-if="errors">
                            {{errors}}
                        </div>
                    </form>
                </div>
                <p class="has-text-grey">
                    <router-link to="/login">Войти</router-link>
                </p>
            </div>
        </div>
    </section>
</template>

<script>
    import axios from "axios";
    export default {
        methods: {
            handleSubmit: function () {
                axios.post('http://localhost:50321/register', {
                    email: 'a@na.ru',
                    password: '123321',
                    name: 'MyName'
                    },
                    { withCredentials: true })
            }
        }
    }

</script>

<style lang="scss">
</style>