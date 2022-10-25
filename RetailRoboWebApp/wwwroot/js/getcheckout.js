//script.js
import { createApp, reactive } from "https://unpkg.com/petite-vue?module";
const app = reactive({
    results: "",
    async pull() {
        const getCheckout = await fetch('http://localhost:7071/api/checkout/')
        this.results = await getCheckout.json()
        console.log(results);
    }
})
createApp({ app }).mount("#getCheckout");