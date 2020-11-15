import Axios, { AxiosRequestConfig, AxiosInstance } from 'axios'

export default class AxiosClient {
    private apiGatewayUrl: String = `https://localhost:61092/api`;

    private config!: AxiosRequestConfig | {};
    axiosClient!: AxiosInstance;
    constructor() {
        this.setConfiguration();
        this.create();
    }

    getConfig(): AxiosRequestConfig {
        return this.config;
    }

    setConfiguration() {
        this.config = <AxiosRequestConfig>{
            baseURL: this.apiGatewayUrl,
            headers: {
                'Access-Control-Allow-Origin': '*'
            }
        }
    }

    create() {
        this.axiosClient = Axios.create(this.config);
    }
}
