
const express = require('express');
var mqtt = require('mqtt');
const app = express();
const fs = require('fs');

const readline = require("readline");
const stream = fs.createReadStream("./data.csv");
const rl = readline.createInterface({ input: stream });
let temperatureData = [];
let humidityData = [];
let pingData = [];
let timeData = [];


var client;
const exchange = 'sensordata';
const routingKey = 'sensordata';

(async () => {
  client = mqtt.connect("mqtt://test.mosquitto.org:1883");
  
})()
.
  // Prints "caught woops"
  catch(error => { console.log('caught', error.message); });

async function sendData (args) {
 try{      
   var data= "{\"temperature\":"+parseFloat(temperatureData[args])+", \"humidity\":"+parseFloat(humidityData[args])+", \"ping\":"+parseFloat(pingData[args])+", \"time\":"+timeData[args]+"\"}";
  client.publish("sensordata", data);
  console.log(data);
 

    console.log('Message published');
    app.get('/', (req, res) => res.send("Sending data... /n"+data))
 }
 catch(ex){
  console.log(ex);
 }
}


rl.on("line", async (row) => {

  var list = row.split("\",\"");
  timeData.push(list[0]);
  pingData.push(list[1]);
  temperatureData.push(list[2]);
  humidityData.push(list[3]);
});
var i =1;
setInterval(() => {
  console.log(i);
  if(i<1000)
  sendData(i++);
 }, 5000);

app.listen(3000,() => console.log("Server ready") )

