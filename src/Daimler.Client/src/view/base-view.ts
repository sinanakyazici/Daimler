import { Component, Vue } from "vue-property-decorator";


@Component
export default class BaseView extends Vue {

    constructor() {
        super();
    }

    successMsg(message: string, title: string) {
        this.$snotify.success(message, title);
    }

    errorMsg(message: any, title: string) {
        if (message && message.response && message.response.data && message.response.data.ErrorMessage) {
            this.$snotify.error(message.response.data.ErrorMessage, title);
        } else {
            this.$snotify.error(message.message, title);
        }
    }
}
