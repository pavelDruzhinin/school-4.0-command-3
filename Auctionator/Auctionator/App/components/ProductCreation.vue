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
      <button
        type="file"
        class="btn btn-outline-secondary"
        id="download-images"
      >Загрузить изображения</button>
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
        description: this.description
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
          "Price": "556",
          "OwnerId":"2",
          "AuctionId":"22"
        }
      })
        .then(function(response) {
          console.log(response.success);
        })
        .catch(function(error) {
          console.log(this);
        });
    }
  }
};
</script>


<style lang="scss">
</style>
