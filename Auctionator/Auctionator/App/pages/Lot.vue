<template>
    <div>
        <div v-if="product.status === productStatus.waitAuction ||
         product.status === productStatus.onAuction" id="auctionList">
            <div class="row" style="margin-top: 30px;">
                <div class="col-md-6">
                    <div id="lot" class="carousel slide owl-carousel card-body" data-ride="carousel">
                        <!-- Indicators -->
                        <ul class="carousel-indicators">
                            <li data-target="#lot" data-slide-to="0" class="active"></li>
                            <li data-target="#lot" data-slide-to="1"></li>
                            <li data-target="#lot" data-slide-to="2"></li>
                        </ul>
                        <!-- The slideshow -->
                        <div class="carousel-inner">
                            <div class="carousel-item item active">
                                <figure>
                                    <img class="figure-img img-fluid rounded imgAutoSize"
                                         src="http://www.vokrugsveta.ru/img/cmn/2014/03/21/039.jpg"
                                         alt="Kitty1">
                                </figure>
                            </div>
                            <div class="carousel-item item">
                                <figure>
                                    <img class="figure-img img-fluid rounded imgAutoSize"
                                         src="http://www.vokrugsveta.ru/img/cmn/2014/03/21/080.jpg"
                                         alt="Kitty2">
                                </figure>
                            </div>
                            <div class="carousel-item item">
                                <figure>
                                    <img class="figure-img img-fluid rounded imgAutoSize"
                                         src="http://www.vokrugsveta.ru/img/cmn/2014/03/21/083.jpg"
                                         alt="Kitty3">
                                </figure>
                            </div>
                        </div>
                        <!-- Left and right controls -->
                        <a class="carousel-control-prev" href="#lot" data-slide="prev">
                            <span class="carousel-control-prev-icon"></span>
                        </a>
                        <a class="carousel-control-next" href="#lot" data-slide="next">
                            <span class="carousel-control-next-icon"></span>
                        </a>
                    </div>
                </div>
                <div class="col-md-6">
                    <div id="timer" class="timer">
                        <Timer v-bind:starttime="auction.startDateTime"
                               v-bind:endtime="auction.endDateTime"
                               trans='{
            "day":"Дни",
            "hours":"Часы",
            "minutes":"Минуты",
            "seconds":"Секунды",
            "expired":"Аукцион окончен",
            "running":"До конца аукциона",
            "upcoming":"До начала аукциона",
            "status": {
                "expired":"Аукцион завершён",
                "running":"Аукцион идёт",
                "upcoming":"В ожидании аукциона"
              }}'></Timer>
                    </div>
                    <div v-if="product.status == productStatus.onAuction">
                        <div class="input-group col-md-9" style="margin-top: 30px;">
                            <input v-model="auction.bet"
                                   type="number"
                                   class="form-control"
                                   placeholder="Ваша ставка"
                                   aria-label="Ваша ставка"
                                   aria-describedby="button-addition"
                                   id="bet">
                            <div class="input-group-append">
                                <button v-if="auction.bet <= currentBet"
                                        class="btn btn-danger"
                                        type="button"
                                        id="button-addition"
                                        style="width: 200px;"
                                        disabled>
                                    Сделать ставку
                                </button>
                                <button v-else
                                        v-on:click="addBet"
                                        class="btn btn-success"
                                        type="button"
                                        id="button-addition"
                                        style="width: 200px;">
                                    Сделать ставку
                                </button>
                            </div>
                        </div>
                        <button class="btn btn-primary btn-block" type="button" style="margin-top: 20px;" disabled>Минимальная ставка: {{currentBet + 1}}</button>
                        <div class="card" style="margin-top:20px;">
                            <div class="card-body">
                                <table class="table table-borderless">
                                    <thead>
                                        <tr>
                                            <th scope="col">№</th>
                                            <th scope="col" class="text-center">Пользователь</th>
                                            <th style="width:33%" scope="col" class="text-center">Дата</th>
                                            <th scope="col" class="text-center">Ставка</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(betItem, index) in auction.betList">
                                            <!--<div v-show="index == 0" class="table-success">-->
                                            <th index scope="row">{{index + 1}}</th>
                                            <td class="text-center">{{betItem.userName}}</td>
                                            <td>{{new Date(betItem.betDateTime).toLocaleDateString()}} {{new Date(betItem.betDateTime).toLocaleTimeString()}}</td>
                                            <td class="text-center">
                                                {{betItem.currentBet}}
                                                <span class="rubSmall">P</span>
                                            </td>
                                            <!--</div>-->
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                <div v-else></div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="card card-body">
                    <h5 class="card-title">The standard Lorem Ipsum passage, used since the 1500s</h5>
                    <p class="card-text">"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."</p>
                </div>
            </div>
        </div>
        <div v-else><error v-bind:msg="errorMsg"></error></div>
    </div>
</template>

<script>
    import getIsAuth from '../auth.js'
    import Timer from "../components/Timer.vue";
    import axios from "axios";
    //import { HubConnectionBuilder } from '@aspnet/signalr'

    //var betList = [] // свойства: userName, currentBet, betDatetime
    //var isBetListChanged = false

    //const hubConnection = new HubConnectionBuilder()
    //    .withUrl('http://localhost:5000/auctionHub')
    //    .configureLogging(signalR.LogLevel.Information)
    //    .build();

    //hubConnection.on("GetBet", function (newBet) {
    //    console.log(newBet)
    //    let bets = betList
    //    betList = [newBet]
    //    betList = betList.concat(bets)
    //    if (betList.length >= 10) {
    //        betList.pop()
    //    }
    //});


    //hubConnection.onclose(() => {
    //    hubConnection.start().then(() => {
    //        console.log('Join to Group: ')
    //        hubConnection.invoke("JoinGroup", this.productId.toString())
    //    })
    //})
    import Error from "../components/Error.vue";
    import prodStatus from "../enums/productStatus.js"
    export default {

        sockets: {
            // Equivalent of
            // signalrHubConnection.on('someEvent', (data) => this.someActionWithData(data))
            GetBet(newBet) {
                //console.log(newBet)
                this.auction.newBet = { currentBet: newBet.currentBet, betInfo: newBet }
                //this.betList.push(newBet)
                //if (this.betList.length() >= 10) {
                //    this.betList.shift()
                //}
            }
        },

        components: {
            Timer,
            'error': Error,
        },

        props: ['productId'],

        data() {
            return {
                time: '',
                isAuth: getIsAuth(),
                currentBet: 0,
                auction: { // Вынести в отдельный компонент
                    id: '',
                    bet: 0,
                    newBet: 0, // чтоб можно было легко наблюдать за изменением массива betList
                    betList: [],
                    startPrice: 0,
                    startDateTime: '',
                    endDateTime: '',
                },
                product: { // Вынести в отдельный компонент
                    name: '',
                    status: 0,
                    description: '',
                    photoPaths: '',
                    ownerName: '',
                    price: 0,
                },
                productStatus: prodStatus,
                errorMsg: "Товар недоступен для покупки"
            }
        },

        created() {
            // Проверить, что productId приходит в типе string!!!
            // В данном случае productId - идентификатор группы
            //this.$socket.invoke('JoinGroup', this.productId.toString())// подключение к группе при создании компонента

            //hubConnection.start().then(() => {
            //    console.log('Join to Group: ')
            //    hubConnection.invoke("JoinGroup", this.productId.toString())
            //})
            axios.get('/product/get/' + this.productId).then(response => {
                if (response.data.success) {
                    let resp = response.data
                    this.product.name = resp.result.name
                    this.product.status = resp.result.status
                    this.product.description = resp.result.description
                    this.auction.id = resp.result.auctionId
                }
                console.log(response)


                axios.get('/auction/get/' + this.auction.id).then(response => {
                    if (response.data.success) {
                        let resp = response.data
                        this.auction.startDateTime = resp.result.startDateTime
                        this.auction.endDateTime = resp.result.endDateTime
                        this.auction.startPrice = resp.result.startPrice
                    }
                    console.log(response)


                    axios.get('/auction/getbets/' + this.auction.id).then(response => {
                        if (response.data.success) {
                            this.auction.betList = response.data.result
                            if (this.auction.betList.length == 0)
                                this.currentBet = this.auction.startPrice
                            else
                                this.currentBet = this.auction.betList[0].currentBet
                        }
                        console.log(response)
                    }).catch(err => {
                        console.log(err)
                    })
                }).catch(err => {
                    console.log(err)
                })
            }).catch(err => {
                console.log(err)
            })


        },

        watch: {
            'auction.newBet': {
                handler: function (val, oldVal) {
                    console.log('Старое значение: ' + oldVal.currentBet + ' Новое значение: ' + val.currentBet)
                    let bets = this.auction.betList
                    this.auction.betList = [val.betInfo]
                    this.auction.betList = this.auction.betList.concat(bets)
                    if (this.auction.betList.length >= 10) {
                        this.auction.betList.pop()
                    }
                    this.currentBet = this.auction.betList[0].currentBet

                },
                deep: true
            }
        },
        //updated() {
        //    hubConnection.invoke("JoinGroup", this.productId.toString());
        //},

        //destroyed() {
        //    //this.$socket.invoke('LeaveGroup', this.productId.toString()) // отключение от групп при удалении компонента
        //    hubConnection.invoke("LeaveGroup", this.productId.toString());
        //},

        methods:
        {
            //getBetList() {
            //    return auction.betList
            //},

            addBet() {
                if (!getIsAuth) {
                    // если push перезагружает страницу, то попробовать выводить диалоговое окно
                    // или объявить <router-link :to="" v-if=""></router-link :to="">
                    this.$router.push('/login')
                }
                let bets = this.auction.betList.length > 0 ? this.auction.betList : [{ currentBet: 0 }]
                if (this.auction.bet > bets[0].currentBet) {
                    let userName = JSON.parse(localStorage.getItem('user')).name
                    let betDto = { productId: this.productId, auctionId: this.auction.id, currentBet: this.auction.bet, userName: userName }
                    this.$socket.invoke('Send', betDto, userName)
                    //hubConnection.invoke("Send", this.productId.toString(), this.auction.bet.toString(), userName).catch;
                }
            },
        },

    };

    $(function () {
        var carousel = $(".owl-carousel");

        carousel.owlCarousel({
            autoplay: true,
            autoplayHoverPause: true,
            autoplayTimeout: 500,
            items: 1,
            loop: true
        });

        $('.carousel_selector a').on('mouseover', function (e) {
            carousel.trigger('stop.owl.autoplay');
        });

        $('.carousel_selector a').on('mouseleave', function (e) {
            carousel.trigger('play.owl.autoplay');
        });

        $('.carousel_selector a').on('click', function (e) {
            e.preventDefault();
            var slideTo = $(this).data('to');
            carousel.trigger('to.owl.carousel', [slideTo]);
            carousel.trigger('stop.owl.autoplay');
        });
    });
</script>

<style lang="scss">
    .rubSmall {
        line-height: 4px;
        width: 0.4em;
        border-bottom: 1px solid #000;
        display: inline-block;
    }

    .imgAutoSize {
        width: 500px;
        height: 350px;
        object-fit: cover;
    }
</style>