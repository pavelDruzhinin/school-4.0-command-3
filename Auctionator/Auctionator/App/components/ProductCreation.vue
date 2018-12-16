<template>
  <form>
    <div class="form-group">
      <label for="name">Название товара</label>
      <input
        v-model="name"
        type="text"
        class="form-control"
        id="name"
        placeholder="Введите название товара"
      >
    </div>
    <div class="form-group">
      <label for="Description">Краткое описание товара</label>
      <input
        v-model="description"
        type="text"
        class="form-control"
        id="Description"
        placeholder="Введите описание товара"
      >
    </div>
    <div class="form-group">
        <label for="price">Стартовая цена</label>
        <input
          v-model="price"
          type="number"
          class="form-control"
          id="price"
          placeholder="Введите стартовую цену товара"
        >
    </div>
    <div class="form-group">
        <label for="auctionTime">Время начала аукциона</label>
        <input
          v-model="startDateTime"
          type="Date"
          class="form-control"
          id="aucttionTime"
          placeholder="Введите время начала аукциона"
        >
    </div>
    <vue-dropzone ref="dropzone" id="drop1" :options="dropOptions"></vue-dropzone>
    <br>
    <button type="button" class="btn btn-primary" @click="onSubmit">Submit</button>
  </form>
</template>


<script>
import axios from "axios";
import vueDropzone from "vue2-dropzone";

export default {
  data() {
    return {
        name: '',
        description: '',
        price: '',
        files:'',
        startDateTime: '',
        productId:'',
      dropOptions:{
          url: '/product/upload-img/',
          addRemoveLinks: true,
          uploadMultiple: true
      }
    };
  },
  components: {vueDropzone},
  methods: {
    getUploadedFileNames() {      
      var photo = {}
      return this.$refs.dropzone.getAcceptedFiles().map(f => photo = {Path: f.name})
    },
    onSubmit() {
      var that = this;
      var product = {
        Name: this.name,
        Price: this.price,
        Description: this.description,
        Photos: this.getUploadedFileNames()
      }

      axios.post('/product/create', product)
        .then((resp) => {
          console.log(that);
          var prodId = resp.data.result;
          var auction = {
            startPrice: that.price,
            productId: prodId,
            startDateTime: that.startDateTime
          }

          axios.post('/auction/create', auction)
          .then((rr) => {
            alert("Ваш аукцион был создан")
          })
        })
    }  
  }
}
</script>


<style lang="scss">
</style>
