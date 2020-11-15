import AxiosClient from '@/service/axios-client';
import UrlViewModel from '@/model/url-view-model.ts';

class UrlRepository extends AxiosClient {
    async encodeUrl(url: UrlViewModel): Promise<String> {
        return await this.axiosClient.post(`/encode-url`, url).then((response) => {
            return response.data;
        });
    }

    async navigate(code: String): Promise<String> {
        return await this.axiosClient.post(`/navigate`, { code: code }).then((response) => {
            return response.data;
        });
    }
}

export default new UrlRepository();
