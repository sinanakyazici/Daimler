<template>
  <div style="height: 100vh !important">
    <b-row class="h-100 text-center" style="padding:0; margin:0;">
      <b-col cols="12" md="2"></b-col>
      <b-col align-self="center" cols="12" md="8">
        <b-row>
          <b-col>
            <h1>
              Daiml.er
            </h1>
          </b-col>
        </b-row>
        <b-row>
          <b-col class="py-3">
            <h3>
              URL Shortening Service
            </h3></b-col
          >
        </b-row>
        <b-row>
          <b-col cols="12" :md="CodeType == 1 ? 8 : 6"
            ><b-form-input
              v-model="Url.UrlAddress"
              placeholder="Enter Url"
            ></b-form-input
          ></b-col>
          <b-col cols="12" v-if="CodeType == 2" :md="2"
            ><b-form-input
              v-model="Url.Code"
              placeholder="Enter Code"
            ></b-form-input
          ></b-col>
          <b-col cols="12" md="2">
            <b-form-select
              v-model="CodeType"
              :options="Options"
              @change="change"
            ></b-form-select>
          </b-col>
          <b-col cols="12" md="2" class="text-left"
            ><b-button class="w-100" variant="success" @click="encodeUrl"
              >Encode</b-button
            ></b-col
          >
        </b-row>
        <b-row>
          <b-col class="py-3" style="color:green">
            <h3>
              {{ EncodedUrlAddress }}
            </h3></b-col
          >
        </b-row>
      </b-col>
      <b-col cols="12" md="2"></b-col>
    </b-row>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import BaseView from "@/view/base-view";
import UrlRepository from "@/repo/url-repo";
import UrlViewModel from "@/model/url-view-model";
@Component
export default class Home extends BaseView {
  Url: UrlViewModel = new UrlViewModel();
  EncodedUrlAddress: String = "";
  CodeType: number = 1;
  Options = [
    { value: 1, text: "Auto Code" },
    { value: 2, text: "Manuel Code" },
  ];
  async encodeUrl() {
    UrlRepository.encodeUrl(this.Url)
      .then((response) => {
        this.EncodedUrlAddress = response;
        this.successMsg("Link başarıyla oluşturuldu.", "Bilgi");
      })
      .catch((error) => {
        this.errorMsg(error, "Hata");
      })
      .finally(() => {
        this.Url = new UrlViewModel();
        this.CodeType = 1;
      });
  }

  change() {
    if (this.CodeType == 1) {
      this.Url.Code = "";
    }
  }
}
</script>
