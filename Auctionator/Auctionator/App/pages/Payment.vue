<template>
    <div>
        <div v-if="errorStatus == 1"></div> <!--Пошли костыли... data формируется даже быстрее отображения, чего нельзя сказать о created()...-->
        <div v-else-if="errorStatus == 2">
            <error v-bind:msg="errorMsg"></error>
        </div>
        <div v-else>
            <div class="container py-3">
                <div class="row">
                    <div class="col-12 col-sm-8 col-md-6 mx-auto">
                        <div id="pay-invoice" class="card">
                            <div class="card-body">
                                <div class="card-title">
                                    <h2 class="text-center">Оплата товара</h2>
                                </div>
                                <hr>
                                    <div class="form-group">
                                        <label>Цена</label>
                                        <h2>{{price}} руб.</h2>
                                    </div>
                                    <div class="form-group">
                                        <label>Название товара</label>
                                        <h3>{{productName}}</h3>
                                    </div>
                                    <div class="form-group">
                                        <label>Продавец</label>
                                        <h3>{{ownerName}}</h3>
                                    </div>

                                    <div class="form-group">
                                        <label for="cc-number" class="control-label">Card number</label>
                                        <input id="cc-number" name="cc-number" type="tel" class="form-control cc-number identified visa" value="" data-val="true" data-val-required="Please enter the card number" data-val-cc-number="Please enter a valid card number" autocomplete="cc-number">
                                        <span class="help-block" data-valmsg-for="cc-number" data-valmsg-replace="true"></span>
                                    </div>
                                    <div class="row">
                                        <div class="col-6">
                                            <div class="form-group">
                                                <label for="cc-exp" class="control-label">Expiration</label>
                                                <input id="cc-exp" name="cc-exp" type="tel" class="form-control cc-exp" value="" data-val="true" data-val-required="Please enter the card expiration" data-val-cc-exp="Please enter a valid month and year" placeholder="MM / YY" autocomplete="cc-exp">
                                                <span class="help-block" data-valmsg-for="cc-exp" data-valmsg-replace="true"></span>
                                            </div>
                                        </div>
                                        <div class="col-6">
                                            <label for="x_card_code" class="control-label">Security code</label>
                                            <div class="input-group">
                                                <input id="x_card_code" name="x_card_code" type="tel" class="form-control cc-cvc" value="" data-val="true" data-val-required="Please enter the security code" data-val-cc-cvc="Please enter a valid security code" autocomplete="off">
                                                <div class="input-group-addon">
                                                    <span class="fa fa-question-circle fa-lg" data-toggle="popover" data-container="body" data-html="true" data-title="Секретный код"></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <button v-on:click="pay" class="btn btn-lg btn-success btn-block">Оплатить</button>
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</template>

<script>
    import axios from "axios";
    import Error from "../components/Error.vue";
    import status from "../enums/productStatus.js"
    export default {
        name: "Payment",
        props: ['productId'],
        data: function () {
            return {
                price: "",
                productName: "",
                ownerName: "",
                errorStatus: 1, // 1 - до полной загрузки, 2 - если пользователю недоступны данные, 0 - если всё ОК
                errorMsg: "",
            }
        },
        created() {
            //console.log('Введённый адрес: /product/' + this.productId + '/get-payment-info')
            axios.get('/product/payment-info/' + this.productId)
                .then(response => {
                    console.log(response) // TODO: убрать ненужное
                    var result = response.data.result
                    var success = response.data.success
                    if (success) {
                        this.price = result.price
                        this.productName = result.name
                        this.productStatus = result.status
                        this.auctionId = result.auctionId
                        this.ownerName = result.ownerName
                        this.errorStatus = 0
                    }
                    else {
                        this.errorStatus = 2
                        this.errorMsg = result
                    }
                })
                .catch(error => {
                    console.log(error)
                    this.errorStatus = 2
                    this.errorMsg = "Ошибка сервера."
                })
        },
        methods: {
            pay() {
                if (this.productStatus == status.onPayment) {
                    axios.get('/auction/pay/' + this.auctionId)
                        .then(response => {
                            console.log(response) // TODO: убрать ненужное
                            var result = response.data.result
                            var success = response.data.success
                            if (success) {
                                alert('Поздравляем! Вы приобрели ' + this.productName + '.') // TODO: заменить на нормальное диалоговое окно или всплывающее сообщение
                                this.$router.push('/') // TODO: заменить на ссылку покупок
                            }
                            else {
                                this.errorStatus = 2
                                this.errorMsg = result
                            }
                        })
                        .catch(error => {
                            console.log(error)
                            this.errorStatus = 2
                            this.errorMsg = "Ошибка сервера."
                        })
                }
                else if (this.productStatus == status.waitAuction) {
                    /* Обработка покупки через цену выкупа,, обращение к контроллеру ProductController*/
                }
            }
        },
        components: {
            'error': Error,
        }
    }

</script>

<style scoped>
    .input-group-addon {
        background-color: transparent;
        border-left: 0;
    }

    .cc-number.identified {
        background-repeat: no-repeat;
        background-position-y: 3px;
        background-position-x: 99%;
    }

    .cc-number.visa {
        background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADAAAAAeCAYAAABqpJ3BAAAABHNCSVQICAgIfAhkiAAABLhJREFUWIXtmGtMFGcUhp9vdhaWBceyuCjFCwouIBhQ0agYMYKphraoMbZVY9T+0NQ/pbZpE2uTpknTJk1JWnszsdVqrVbTRpNeTDCtqRULoiAW2S1euClQdhfXZYHdYac/djtgtMVWE6Dx/TVzvnO+ed9z+WYyAsDhcCQCJZqm5WuaZmEYQwjhEkKcAIptNluLqKqqSjSbzTVxcXExiqIYZVkeao7/CFVV8Xg8AafT6fX5fNNls9lcEhcXN8pisQxv5mHIsozFYjECMUCJFAwGCxRFGRHkB0JRFGMwGCyQgNjh3jZ3Q5hzrDTURO4XDwUMNR4KGGrc9fhR/6hFddUj0EIGYcAwJh3Zkqz7+Lr9HC+9CEBUVARLCzL59vgF/H6VmOhIlizOACAY1Cgrv0xVTRNt7TcxGCTiraNY/8x8os2R+n7XGju48FszfWoQRYkiPy/9vwuQrdMwRMfT6zhG17ldEPAhRcQQu+EUiFDRDn1dwVsl3wGweeMi5s1J5oXthwBYON/GksUZ3PL2sH7Lbmrrrt/xjFVFOboAd6ePFWs/wNftByDCKFN5cgcREYMf73/rIcxjMGVv4tV9PWzOKsPSXU2f+woGS0pYQDkARtnAuqfm4qhv02OTJ1sBeOf94zr5x/IzKVg0Db9f5Vx1A6OVKN1/38EynTyAP6BSU9vCrOxJgwoYdAamZmZQtHMCbcaZqB2XADhz9gpXGzoAKFyaRfyYUTjqW/tjkscCUHWhUbctL5zBk8uyWFU0izdfW6lnt6cnwIEjZwCYN7u/Rc8PiL0vAXm5NvqCgqd3JdJwtRmAg0fK9fWNa+YDYB9QgalT4gGYmdWfwee27eelHYdpvu6+bf8jRytxd/oAeKV4mV6Zc9UND0ZAmi2BsVaFXhVKjgbocHop/akWCGUszZYAoFdACEFyWMDLzy9j9YrZCCHQNI1j31fx+Or3OP3rZSA04HsO/AJAVuYE0mwJZE+fCMD56gdUAYC83FQAfqxo59P9pwiofQBsWrdA9/n9cqgCiQmP6MNpMhl5Y/tyDu/ZQk52EgDdPX5ef/sYAD+cuEhTiwuAuTlTqHPcYGy8AoCrs4trjc4HJGCBDQBN09j75WkAkpOsLMwN2d2dXXi7egGwpYwLEw3o8dMzxvPhu+sQQgDg84UGdvfnP+s+n+w5SdGanXz1TYVuu5c2uqfP0HlzUjDKBgJqH2o4+xvW5urrN1pv6te2lNAAb9r6GQAZ6Y9ilA2UV15F00LvlcV5aZw5e4WLl1oAiImOJDLSqO/hcnehaRrnqxtZ+cTM+xcQbY4gZ0YSZRWh3rXERrO8cIa+3treLyA1XAFHfSvert47sliwaBrbtxWy9cUvdNvej58lMz1Rv1+1/iNqapuprG7A6fJiMhlve+kNhKirq9NSU1MHFdHh9OJyewFQFDPjwr0K4LnVQ2tbJwATx8dhMhlpanHT0NSB19uLpmmMHh3FlKR4Pe6voTfIBpKTrLc9q7Xdg8fjQwjB5ElWJEkgSeIOTna7/d4FDEfY7faR/zH3UMBQ438hwK2q6lDz+NcIc3ZLkiSVejyeEafA4/EEJEkqlX0+XzFQAIy4X4tAsYCR/XP3T6N8y7XxxywcAAAAAElFTkSuQmCC);
    }

    .one-card > div {
        height: 150px;
        background-position: center center;
        background-repeat: no-repeat;
    }   
</style>