<template>
    <div class="row">
        <div class="col">
            <div class="card">
                <div id="lot" class="carousel slide" data-ride="carousel">
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
                                <img class="figure-img img-fluid rounded"
                                     src="http://www.vokrugsveta.ru/img/cmn/2014/03/21/039.jpg"
                                     alt="Kitty1">
                            </figure>
                        </div>
                        <div class="carousel-item item">
                            <figure>
                                <img class="figure-img img-fluid rounded"
                                     src="http://www.vokrugsveta.ru/img/cmn/2014/03/21/080.jpg"
                                     alt="Kitty2">
                            </figure>
                        </div>
                        <div class="carousel-item item">
                            <figure>
                                <img class="figure-img img-fluid rounded"
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
                <div class="card-body">
                    <h5 class="card-title">Title of the product</h5>
                    <p class="card-text">Some great description about the product.</p>
                </div>
            </div>
        </div>
        <div class="col">
            <div class="card">
                <div class="card-body">
                    <div class="input-group">
                        <input type="number"
                               class="form-control"
                               placeholder="Ваша ставка"
                               aria-label="Ваша ставка"
                               aria-describedby="button-addition">
                        <div class="input-group-append">
                            <button class="btn btn-danger"
                                    type="button"
                                    id="button-addition">
                                Сделать ставку
                            </button>
                        </div>
                    </div>
                </div>
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
                            <tr>
                                <th scope="row">1</th>
                                <td class="text-center">Craig</td>
                                <td>22.12.2018 13:15:27</td>
                                <td class="text-center">
                                    745
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
                            <tr class="table-success">
                                <th scope="row">3</th>
                                <td class="text-center">LoonelyToons</td>
                                <td>22.12.2018 13:15:27</td>
                                <td class="text-center">
                                    2345
                                    <span class="rubSmall">P</span>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import VueAgile from "vue-agile";

    export default {
        components: { VueAgile }
    };
</script>

<style lang="scss">
.rubSmall {
  line-height: 4px;
  width: 0.4em;
  border-bottom: 1px solid #000;
  display: inline-block;
}
</style>