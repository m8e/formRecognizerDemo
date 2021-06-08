import http from 'k6/http';
import { sleep } from 'k6';
import { Counter } from 'k6/metrics';

export let options = {
    vus: 100,
    duration: '180s'
};

let myErrorCounter = new Counter("my_error_counter");

export default function() {
    var url = ''
    var payload = JSON.stringify({
        "fileUri": "",
        "webhookUrl": "dede"        
    });

    var params = {
        headers: {
            'Content-Type': 'application/json'
        }
    };

    let res = http.post(url,payload,params);
    if (res.status != 200){
        myErrorCounter.add(1);
    }
}

