<template>
    <div class="container-fluid bg-light py-3">
        <div class="row">
            <div class="col-md-6 mx-auto">
                <div class="card card-body">
                    <h3 class="text-center mb-4">Регистрация</h3>
                    <fieldset>
                        <div class="form-group has-error">
                            <input class="form-control input-lg" placeholder="Введите E-mail" v-model="user.email" autofocus="" type="email">
                            <p v-show="!validation.email" class="text-danger">Неверно введён E-mail.</p>
                            <p v-show="!validation.emailNotEmpty" class="text-danger">emptyFieldError</p>
                        </div>
                        <div class="form-group has-success">
                            <input class="form-control input-lg" placeholder="Введите пароль" v-model="user.password" type="password">
                            <p v-show="!validation.passwordNotEmpty" class="text-danger">emptyFieldError</p>
                        </div>
                        <div class="form-group has-success">
                            <input class="form-control input-lg" placeholder="Повторите пароль" v-model="user.confirmPassword" type="password">
                            <p v-show="!validation.confirmPassword" class="text-danger">Пароли не совпадают.</p>
                            <p v-show="!validation.confirmPasswordNotEmpty" class="text-danger">emptyFieldError</p>
                        </div>
                        <div class="form-group">
                            <input class="form-control input-lg" placeholder="Введите Ваше имя" v-model="user.name" type="text">
                            <p v-show="!validation.name" class="text-danger">Имя может содержать только буквы.</p>
                            <p v-show="!validation.nameNotEmpty" class="text-danger">emptyFieldError</p>
                        </div>
                        <input v-on:click="handleSubmit" class="btn btn-lg btn-primary btn-block" value="Зарегистрироваться" type="submit">
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
                        confirmPassword: '',
                        name: ''
                    },
                    emptyFieldError: 'Поле не может быть пустым.'
                }            
        },
        // computed property for form validation state
        computed: {
            validation: function () {
                return {
                    email: emailRE.test(this.user.email),
                    confirmPassword: this.user.password == this.user.confirmPassword ? true : false,
                    nameNotEmpty: !!this.user.name.trim(),
                    emailNotEmpty: !!this.user.email.trim(),
                    passwordNotEmpty: !!this.user.password.trim(),
                    confirmPasswordNotEmpty: !!this.user.confirmPassword.trim(),
                }
            },
            isValid: function () {
                var validation = this.validation
                return Object.keys(validation).every(function (key) {
                    return validation[key]
                })
            }
        },
        // methods
        methods: {
            handleSubmit: function () {
                //if (this.isValid) {
                    axios.post('/user/register', {
                        email: this.user.email,
                        password: this.user.password,
                        name: this.user.name
                        },
                        { withCredentials: true })
                    .then(response => {
                        console.log(response)
                    })
                    .catch(error => {
                        console.log(error.response)
                    });
                //}
            }
        }
    }
</script>

<style lang="scss">
</style>