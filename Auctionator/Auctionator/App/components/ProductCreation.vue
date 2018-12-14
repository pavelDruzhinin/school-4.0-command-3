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
        name:this.name,
        description: this.description,
        price: this.price,
        files:'',
      dropOptions:{
          url: '/product/upload-img/',
          addRemoveLinks: true,
          uploadMultiple: true
      }
    };
  },
  components: {vueDropzone},
  methods: {
    onSubmit() {
      var that = this;
      axios({
        method: "post",
        url: "/product/create",
        data: {
          "Name": this.name,
          "Description": this.description,
          "Price": this.price
        }
      })
        .then(function(response) {          
          var productId = response.data.result;
          
          // upload images          
        })
        .catch(function(error) {

        });
    }
  }
};
</script>


<style lang="scss">
</style>
