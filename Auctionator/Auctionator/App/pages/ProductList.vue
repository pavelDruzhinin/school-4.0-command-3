<template>
    <div class="container">
        <h2 class="text-center">{{info}}</h2>
        <div v-if="products.length == 0">
            {{msg}}
        </div>
        <ul v-else class="list-group" v-for="product in this.products">
            <router-link :to="{ name: 'Lot', params: { productId: product.id }}">
                <li class="list-group-item list-group-item-action" style="margin-top:20px">
                    <div class="row">
                        <div class="col-md-10">
                            {{product.name}}
                        </div>
                        <div class="col-md-2">
                            Статус:
                            {{getProductStatus(product.status)}}
                        </div>
                    </div>
                </li>
            </router-link>
        </ul>
    </div>
</template>

<script>
    import routes from "../index.js"
    import prodStatus from "../enums/productStatus.js"
    import axios from "axios";
    export default {
        components: {
            routes
        },
        data: function () {
            return {
                products: [],
                isDataChanged: false,
                msg: 'Нет товаров в списке',
                info: 'Мои товары'
            }
        },
        created() {

                axios.get('/product/own-products').then(response => {
                    console.log(response)
                    if (response.data.success) {
                        let resp = response.data

                        this.products = resp.result

                    }
                    this.isDataChanged = true
                    this.products.push()
                }).catch(err => {
                    console.log(err)
                })

        },
        methods: {
            getProductStatus(status) {
                if (status == prodStatus.onAuction) {
                    return 'На аукционе'
                } if (status == prodStatus.deleted) {
                    return 'Удалён'
                } if (status == prodStatus.onPayment) {
                    return 'На оплате'
                } if (status == prodStatus.paid) {
                    return 'Оплачен'
                } if (status == prodStatus.waitAuction) {
                    return 'В ожидании аукциона'
                }
            }
        }
        //watch: {
        //    isDataChanged: {
        //        handler: function (val, oldVal) {
        //            this.isDataChanged = true
        //            this.products.push()
        //        },
        //    }
        //}
    }
</script>