
const express = require('express');
const mqtt = require('mqtt');
const app = express();

var path = require('path');

const fs = require('fs');



const client = mqtt.connect('mqtt://172.28.80.1:1883', {
  clientId: "131df074d5c8",
  clean: true
});
client.on('connect', () =>{
  console.log("Connection established successfully!");
});

const topic = 'JokaVida';
const message = 'test message';



fs.readFile('./aa.txt', 'utf8', (err, data) => {
  if (err) {
    console.error(err);
    return;
  }
  console.log(data);
  client.on('connect', () => {
    console.log(`Is client connected: ${client.connected}`);    
    if (client.connected === true) {
        console.log(`message: ${message}, topic: ${topic}`); 
        client.publish(topic, data);
    }
});
  app.get('/', (req, res) => res.send(data))
});

// error handling
client.on('error',(error) => {
  console.error(error);
  process.exit(1);
});
app.listen(3000,() => console.log("Server ready") )

