<template>
    <div class="container-fluid py-3">
        <div class="row">
            <div class="col-md-6 mx-auto">
                <div class="card card-body">
                    <h3 class="text-center mb-4">Регистрация</h3>
                    <fieldset>
                        <div class="form-group has-error">
                            <input class="form-control input-lg" placeholder="Введите E-mail" v-model="user.email" autofocus="" type="email">
                            <p v-show="!validation.email" class="text-danger">Неверно введён E-mail.</p>
                        </div>
                        <div class="form-group has-success">
                            <input class="form-control input-lg" placeholder="Введите пароль" v-model="user.password" type="password">
                            <p v-show="!validation.passwordLength" class="text-danger">{{passLengthMsg}}</p>
                        </div>
                        <div class="form-group has-success">
                            <input class="form-control input-lg" placeholder="Повторите пароль" v-model="confirmPassword" type="password">
                            <p v-show="!validation.confirmPassword" class="text-danger">Пароли не совпадают.</p>
                        </div>
                        <div class="form-group">
                            <input class="form-control input-lg" placeholder="Введите Ваше имя" v-model="user.name" type="text">
                            <p v-show="!validation.nameNotEmpty" class="text-danger">{{emptyMsg}}</p>
                        </div>
                        <div v-if="isResponseMsg" class="alert alert-danger alert-dismissible fade show" role="alert">
                            {{responseMsg}}
                        </div>
                        <input v-if="isValid" v-on:click="handleSubmit" class="btn btn-lg btn-primary btn-block" value="Зарегистрироваться" type="submit">
                        <input v-else class="btn btn-lg btn-danger btn-block" value="Зарегистрироваться" type="submit" disabled>
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
            function() {
                return {
                    user: {
                        email: '',
                        password: '',
                        name: ''
                    },
                    confirmPassword: '',
                    emptyMsg: 'Поле не должно быть пустым.',
                    passLengthMsg: 'Пароль не должен быть короче 6 символов',
                    responseMsg: ''
                }            
        },
        // computed property for form validation state
        computed: {
            validation: function () {
                return {
                    email: emailRE.test(this.user.email),
                    nameNotEmpty: this.user.name === '' ? false : true,
                    passwordNotEmpty: this.user.password === '' ? false : true,
                    confirmPassword: this.user.password === this.confirmPassword ? true : false,
                    passwordLength: this.user.password.length > 5 ? true : false
                }
            },
            isValid: function () {
                var validation = this.validation
                return Object.keys(validation).every(function (key) {
                    return validation[key]
                })
            },
            isResponseMsg: function () {
                return this.responseMsg === '' ? false : true
            }
        },
        // methods
        methods: {
            handleSubmit: function () {
                if (this.isValid) {
                    axios.post('/register', this.user)
                    .then(response => {
                        console.log(response)
                        var result = response.data.result
                        if (response.data.success === true) {
                            var userInfo = { id: result.id, name: result.name, email: this.user.email }
                            localStorage.setItem('user', JSON.stringify(userInfo))
                            var savedUser = JSON.parse(localStorage.getItem('user')) //TODO: убрать эту строку
                            alert('Вы были успешно зарегистрированы и авторизованы, ' + savedUser.name) // TODO: заменить на нормальное диалоговое окно или всплывающее сообщение
                            this.$router.push('/')
                        }
                        else {
                            this.responseMsg = result
                        }
                    })
                    .catch(error => {
                        alert('Ошибка! Повторите регистрацию позже!')
                        console.log(error.response)
                    });
                }
            }
        },
    }
</script>

<style lang="scss">
</style>