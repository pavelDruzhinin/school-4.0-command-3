﻿<template>
  <div class="col-md-4 col-sm-6">
    <!-- Restaurant Item -->
    <div class="item">
      <!-- Item's image -->
      <img class="img-responsive" :src="'https://lorempixel.com/200/200/food/' + auction.id" alt>
      <!-- Item details -->
      <div class="item-dtls">
        <!-- product title -->
        <h4>
          <router-link :to="'lot/' + auction.id">{{ auction.title }}</router-link>
        </h4>
        <!-- price -->
        <span class="price lblue">
          <p>
            <!-- <span v-if="auction.lastBet == '0'">Текущая цена: {{ auction.startPrice }}</span> -->
            <!-- <span v-else>Текущая цена: {{ auction.lastBet }}</span>             -->
            <span>{{ currentPrice }}</span>
            <span class="rub">Р</span>
          </p>
        </span>
        <div id="timer" class="timer">
          <Timer
            starttime="Sep 5, 2019 15:37:25"
            endtime="Dec 18, 2018 16:37:25"
            trans='{  
            "day":"Дни",
            "hours":"Часы",
            "minutes":"Минуты",
            "seconds":"Секунды",
            "expired":"Аукцион окончен",
            "running":"До конца аукциона",
            "upcoming":"До начала аукциона",
            "status": {
                "expired":"Expired",
                "running":"Running",
                "upcoming":"Future"
              }}'></Timer>
        </div>
      </div>
      <!-- add to cart btn -->
      <div class="ecom bg-lblue">
        <!--<a class="btn btn-orange" href="#">Сделать ставку</a>-->
        <button class="btn btn-orange" @click="getProduct">Сделать ставку</button>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import Timer from "./Timer.vue";

export default {
  data: function() {
    return {
      currentPrice:""
    }
  },
  props: ["auction"],
  methods: {
    getProduct: function() {
      
    }
  },
  components: { Timer },
  created: function() {
      // keep the link to Vue object
      let that = this;
      axios
        .get(`/Home/GetProduct?id=${that.auction.id}`)
        .then(response => {
          // handle success
          if(response.data.lastBet == 0) {
            that.currentPrice = response.data.startPrice;
          }
          else {
            that.currentPrice = response.data.lastBet;
          }
        })
        .catch(error => {
          // handle error
          console.log(error);
        })
        .then(() => {
          // always executed
        });
  }
};
</script>

<style lang="scss">
.rub {
  line-height: 8px;
  width: 0.4em;
  border-bottom: 1px solid #000;
  display: inline-block;
}
/* Background color classes */
.bg-white {
  background-color: #ffffff !important;
}
.bg-grey {
  background-color: #eeeeee !important;
}
.bg-black {
  background-color: #555555 !important;
}
.bg-red {
  background-color: #f75353 !important;
}
.bg-green {
  background-color: #51d466 !important;
}
.bg-lblue {
  background-color: #32c8de !important;
}
.bg-blue {
  background-color: #609cec !important;
}
.bg-orange {
  background-color: #f78153 !important;
}
.bg-yellow {
  background-color: #fcd419 !important;
}
.bg-purple {
  background-color: #cb79e6 !important;
}
.bg-rose {
  background-color: #ff61e7 !important;
}
.bg-brown {
  background-color: #d08166 !important;
}

/* Button classes */
.btn {
  border-radius: 2px;
  position: relative;
}
.btn.btn-no-border {
  border: 0px !important;
}
/* Button colors */
.btn.btn-white {
  background: #ffffff;
  color: #666666;
  border: 1px solid #dddddd;
}
.btn.btn-white:hover,
.btn.btn-white:focus,
.btn.btn-white.active,
.btn.btn-white:active {
  background: #f7f7f7;
  color: #666666;
}
.btn.btn-grey {
  background: #eeeeee;
  color: #666666;
  border: 1px solid #d5d5d5;
}
.btn.btn-grey:hover,
.btn.btn-grey:focus,
.btn.btn-grey.active,
.btn.btn-grey:active {
  background: #d5d5d5;
  color: #999;
}
.btn.btn-black {
  color: #ffffff;
  background: #666666;
  border: 1px solid #4d4d4d;
}
.btn.btn-black:hover,
.btn.btn-black:focus,
.btn.btn-black.active,
.btn.btn-black:active {
  background: #4d4d4d;
  color: #ffffff;
}
.btn.btn-red {
  color: #ffffff;
  background: #ed5441;
  border: 1px solid #e52d16;
}
.btn.btn-red:hover,
.btn.btn-red:focus,
.btn.btn-red.active,
.btn.btn-red:active {
  color: #ffffff;
  background: #e52d16;
}
.btn.btn-green {
  color: #ffffff;
  background: #51d466;
  border: 1px solid #30c247;
}
.btn.btn-green:hover,
.btn.btn-green:focus,
.btn.btn-green.active,
.btn.btn-green:active {
  background: #30c247;
  color: #ffffff;
}
.btn.btn-lblue {
  color: #ffffff;
  background: #32c8de;
  border: 1px solid #1faabe;
}
.btn.btn-lblue:hover,
.btn.btn-lblue:focus,
.btn.btn-lblue.active,
.btn.btn-lblue:active {
  background: #1faabe;
  color: #ffffff;
}
.btn.btn-blue {
  color: #ffffff;
  background: #609cec;
  border: 1px solid #3280e7;
}
.btn.btn-blue:hover,
.btn.btn-blue:focus,
.btn.btn-blue.active,
.btn.btn-blue:active {
  background: #3280e7;
  color: #ffffff;
}
.btn.btn-orange {
  color: #ffffff;
  background: #f8a841;
  border: 1px solid #f69110;
}
.btn.btn-orange:hover,
.btn.btn-orange:focus,
.btn.btn-orange.active,
.btn.btn-orange:active {
  background: #f69110;
  color: #ffffff;
}
.btn.btn-yellow {
  background: #fcd419;
  color: #ffffff;
  border: 1px solid #dfb803;
}
.btn.btn-yellow:hover,
.btn.btn-yellow:focus,
.btn.btn-yellow.active,
.btn.btn-yellow:active {
  background: #dfb803;
  color: #ffffff;
}
.btn.btn-purple {
  background: #cb79e6;
  color: #ffffff;
  border: 1px solid #ba4ede;
}
.btn.btn-purple:hover,
.btn.btn-purple:focus,
.btn.btn-purple.active,
.btn.btn-purple:active {
  background: #ba4ede;
  color: #ffffff;
}
.btn.btn-rose {
  background: #ff61e7;
  color: #ffffff;
  border: 1px solid #ff2edf;
}
.btn.btn-rose:hover,
.btn.btn-rose:focus,
.btn.btn-rose.active,
.btn.btn-rose:active {
  background: #ff2edf;
  color: #ffffff;
}
.btn.btn-brown {
  background: #d08166;
  color: #ffffff;
  border: 1px solid #c4613f;
}
.btn.btn-brown:hover,
.btn.btn-brown:focus,
.btn.btn-brown.active,
.btn.btn-brown:active {
  background: #c4613f;
  color: #ffffff;
}
</style>