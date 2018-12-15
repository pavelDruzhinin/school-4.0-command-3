<template>
  <div class="shop-items">
			<div class="container-fluid">
        <div class="row" v-for="i in Math.ceil(auctions.length / 3)">
          <auction v-for="auction in auctions.slice((i-1) * 3, i * 3)" :key="auction.id" :auction="auction"></auction>
        </div>
      </div>    
  </div>
</template>

<script>
  import Auction from './components/Auction'
	import axios from 'axios'
	
  export default { 
    
    data() {
      return {
        auctions: []
      }   
    },
		components: {Auction},
		beforeCreate() {
			var that = this;
			axios.get('auction/getformain/10')
			.then(response => {
				that.auctions = response.data.result;
			})
		}
}
</script>

<style lang="scss">
  
.shop-items{
	max-width:1150px;
	margin:10px auto;
	padding:0px 20px;
}
.shop-items .item {
	position: relative;
	max-width: 360px;
	margin: 15px auto;
	padding: 5px;
	text-align: center;
	border-radius: 4px;
	overflow: hidden;
	border:2px solid #eee;
}
.shop-items .item:hover{	
	border:2px solid #32c8de;
}
.shop-items .item img {
	width: 100%;
	max-width: 360px;
	margin: 0 auto;
	border: 1px solid #eee;
	border-radius: 3px;
}
.shop-items .item  .item-dtls h4 {
	margin-top: 13px;
	margin-bottom: 10px;
	text-transform: uppercase;
}
.shop-items .item  .item-dtls .price {
	display: block;
	margin-bottom: 13px;
	font-size: 20px;
}
.shop-items .item  .ecom {
	position: absolute;
	top: 100%;
	left: 0;
	width: 100%;
	padding-bottom:10px;
	padding-top: 10px;
	-webkit-transition: all 0.35s ease-in;
	-moz-transition: all 0.35s ease-in;
	-ms-transition: all 0.35s ease-in;
	-o-transition: all 0.35s ease-in;
	transition: all 0.35s ease-in;
}
.shop-items .item:hover  .ecom { 
	margin-top: -50px; 
}
.shop-items .item  .ecom  a.btn{
	border:1px solid #fff;
	color:#fff;
	background:transparent;
	-webkit-transition: all 0.35s ease-in;
	-moz-transition: all 0.35s ease-in;
	-ms-transition: all 0.35s ease-in;
	-o-transition: all 0.35s ease-in;
	transition: all 0.35s ease-in;
}
.shop-items .item  .ecom  a.btn:hover{
	background:#fff;
	color:#777;
}   
</style>