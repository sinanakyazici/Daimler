import Vue from 'vue';
import App from './App.vue';
import router from "@/router";
import { BootstrapVue, IconsPlugin, LayoutPlugin } from 'bootstrap-vue';
import '@/plugin/snotify';

import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';
// Install BootstrapVue
Vue.use(BootstrapVue);
// Optionally install the BootstrapVue icon components plugin
Vue.use(IconsPlugin);
Vue.use(LayoutPlugin);
Vue.config.productionTip = true;

new Vue({
    name: "Root",
    router,
    render: h => h(App)
}).$mount("#app");
