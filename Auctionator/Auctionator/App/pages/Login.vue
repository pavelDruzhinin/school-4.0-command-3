<template>
    <div class="container-fluid py-3">
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
                        <div v-if="errorMsg != ''" class="alert alert-danger alert-dismissible fade show" role="alert">
                            {{errorMsg}}
                        </div>
                        <button v-on:click="handleAuth" class="btn btn-lg btn-primary btn-block">Войти</button>
                        <button v-on:click="handleRegister" class="btn btn-lg btn-success btn-block">Зарегистрироваться</button>
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
                    //passwordMsg: 'Не введён пароль.',
                    //emailMsg: 'Неверно введён E-mail.',
                    errorMsg: '',
                }
            },
        //computed: {
        //    validation: function () {
        //        return {
        //            email: emailRE.test(this.user.email),
        //            passwordNotEmpty: this.user.password === '' ? false : true,
        //        }
        //    },
        //    isValid: function () {
        //        var validation = this.validation
        //        return Object.keys(validation).every(function (key) {
        //            return validation[key]
        //        })
        //    },
        //},
        // methods
        methods: {
            handleAuth: function () {
                /*if (this.isValid) */
                /*{*/ //TODO: Постараться сделать эту грёбанную проверку на клиенте
                axios.post('/login', this.user)
                    .then(response => {
                        console.log(response) // TODO: убрать ненужное
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
            },
            handleRegister() {
                this.$router.push('/register')
            }
        }
    }

</script>

<style lang="scss">
</style>