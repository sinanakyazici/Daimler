import Vue from 'vue';
import Snotify, { SnotifyPosition } from 'vue-snotify';
import { SnotifyService } from 'vue-snotify/SnotifyService'
import 'vue-snotify/styles/material.css';

const options = {
    global: {
        showProgressBar: true,
        closeOnClick: true,
        pauseOnHover: true,
        newOnTop: true,
        oneAtTime: false,
        preventDuplicates: false,
        titleMaxLength: 110,
        bodyMaxLength: 1110,
        timeout: 5000
    },
    toast: {
        position: SnotifyPosition.rightTop
    }
};

Vue.use(Snotify, options);

declare module "vue/types/vue" {
    interface Vue {
        readonly $snotify: SnotifyService;
    }
}
