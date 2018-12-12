<template>
    <div class="container-fluid bg-light py-3">
        <div class="row">
            <div class="col-md-6 mx-auto">
                <div class="card card-body">
                    <h3 class="text-center mb-4">Вход</h3>
                    <fieldset>
                        <div class="form-group has-error">
                            <input class="form-control input-lg" placeholder="Введите E-mail" v-model.trim="user.email" autofocus="" type="email">
                        </div>
                        <div class="form-group has-success">
                            <input class="form-control input-lg" placeholder="Введите пароль" v-model.trim="user.password" type="password">
                        </div>
                        <!--<div v-show="!isValid" class="alert alert-danger" role="alert"></div>-->
                        <button v-on:click="handleSubmit" class="btn btn-lg btn-primary btn-block">Войти</button>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import axios from "axios";
    var emailRE = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    //var passRE = /^[a-zA-Z\-0-9].$/;

    export default {
        // initial data
        data:
            function () {
                return {
                    user: {
                        email: '',
                        password: ''
                    },
                    passwordMsg: 'Не введён пароль.',
                    emailMsg: 'Неверно введён E-mail.',
                    errorMsg: 'Неверно введён E-mail или пароль',
                    err: false
                }
            },
        computed: {
            validation: function () {
                return {
                    email: emailRE.test(this.user.email),
                    passwordNotEmpty: this.user.password === '' ? false : true,
                }
            },
            isValid: function () {
                var validation = this.validation
                return Object.keys(validation).every(function (key) {
                    return validation[key]
                })
            },
        },
        // methods
        methods: {
            handleSubmit: function () {
                /*if (this.isValid) */{
                    axios.post('/login', this.user)
                        .then(response => {
                            console.log(response)
                            var result = response.data.result
                            if (response.data.success === true) {
                                var userInfo = { id: result.id, name: result.name, email: this.user.email }
                                localStorage.setItem('user', JSON.stringify(userInfo))
                                var savedUser = JSON.parse(localStorage.getItem('user')) //TODO: убрать эту строку
                                alert('Вы были успешно авторизованы, ' + savedUser.name) // TODO: заменить на нормальное диалоговое окно или всплывающее сообщение
                                this.$router.push('/')
                            }
                            else {
                                this.errorMsg = result
                            }
                        })
                        .catch(error => {
                            alert('Ошибка! Повторите вход позже!')
                            console.log(error.response)
                        });
                }
            }
        }
    }
</script>

<style lang="scss">
</style>