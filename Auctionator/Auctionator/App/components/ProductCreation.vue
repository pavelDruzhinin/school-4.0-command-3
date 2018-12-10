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
      <label for="price">Цена</label>
      <input
        v-model="price"
        type="number"
        class="form-control"
        id="price"
        placeholder="Введите цену товара"
      >
    </div>
    <div class="form-group">
        
<div class="form-group">
      <label>Загрузить файлы
        <input multiple accept="image/*" type="file" id="file" ref="files" @change="handleFileUploads"/>
      </label>       
    </div>

    </div>
    <div class="form-group">
      <input type="checkbox" id="create-auction">
      <label class="form-check-label" for="create-auction">Создать аукцион</label>
    </div>
    <br>
    <button type="button" class="btn btn-primary" @click="onSubmit">Submit</button>
  </form>
</template>


<script>
import axios from "axios";

export default {
  data() {
    return {
        name:this.name,
        description: this.description,
        price: this.price,
        files:''
    };
  },
  methods: {
    onSubmit() {
      axios({
        method: "post",
        url: "/products/create",
        data: {
          "Name": this.name,
          "Description": this.description,
          "Price": this.price
        }
      })
        .then(function(response) {
          console.log(response);
          debugger;
          var productId = response.data.productId;
          // upload images
          this.submitFiles(productId);
        })
        .catch(function(error) {

        });
    },
    submitFiles(productId) {
      let formData = new FormData();
      for (let index = 0; index < this.files.length; index++) {
        const file = this.files[index];
        formData.append('files['+i+']', file);
      }

      axios.post(`/product/upload-img/${productId}`)
    },
    handleFileUploads() {
        this.files =this.$refs.files.files;
    }
  }
};
</script>


<style lang="scss">
</style>
