<template>
    <div id="auctionList">
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
                <div class="input-group col-md-8" style="margin-top: 30px;">
                    <input type="number"
                           class="form-control"
                           placeholder="Стоимость"
                           aria-label="Стоимость"
                           aria-describedby="button-addition"
                           id="cost">
                    <div class="input-group-append">
                        <button class="btn btn-success"
                                type="button"
                                id="button-buy-now"
                                style="width: 150px;">
                            Выкупить лот
                        </button>
                    </div>
                </div>
                <div class="input-group col-md-8" style="margin-top: 30px;">
                    <input type="number"
                           class="form-control"
                           placeholder="Ваша ставка"
                           aria-label="Ваша ставка"
                           aria-describedby="button-addition"
                           id="bet">
                    <div class="input-group-append">
                        <button class="btn btn-danger"
                                type="button"
                                id="button-addition"
                                style="width: 150px;">
                            Сделать ставку
                        </button>
                    </div>
                </div>
                <div id="betList"></div>
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
                                <tr class="table-success">
                                    <th scope="row">4</th>
                                    <td class="text-center">Freddy</td>
                                    <td>22.12.2018 13:15:27</td>
                                    <td class="text-center">
                                        12345
                                        <span class="rubSmall">P</span>
                                    </td>
                                </tr>
                                <tr>
                                    <th scope="row">3</th>
                                    <td class="text-center">Craig</td>
                                    <td>22.12.2018 13:15:27</td>
                                    <td class="text-center">
                                        2345
                                        <span class="rubSmall">P</span>
                                    </td>
                                </tr>
                                <tr>
                                    <th scope="row">2</th>
                                    <td class="text-center">MotherKisser</td>
                                    <td>22.12.2018 13:15:27</td>
                                    <td class="text-center">
                                        2145
                                        <span class="rubSmall">P</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
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
</template>

<script>
    import Vue from 'vue';
    import VueAgile from "vue-agile";
    import VueSignalR from '@latelier/vue-signalr';

    Vue.use(VueSignalR, 'http://localhost:5000/auctionHub');

    new Vue({
        el: '#betList',
        render: h => h(App),
        created() {
            this.$socket.start({
                log: false
            });
        },
    });

    export default {
        components: { VueAgile }
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