<template>
    <div>
        <div class="alert alert-danger" role="alert">
            <h4 class="alert-heading">Ошибка!</h4>
            <p>{{msg}}</p>
        </div>
        <button v-on:click="goBack" type="button" class="btn btn-primary">Вернуться назад</button>
    </div>
</template>

<script>
    export default {
        props: ['msg'],
        methods: {
            goBack() {
                this.$router.go(-1)
            }
        }
    }
</script>