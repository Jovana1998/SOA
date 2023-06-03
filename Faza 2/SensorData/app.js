
const express = require('express');
const amqp = require("amqplib")
const app = express();
const fs = require('fs');


 
fs.readFile('./SensorData.txt', 'utf8', (err, data) => {
  if (err) {
    console.error(err);
    return;
  }
 
const amqpUrl = 'amqp://joka:joka@host.docker.internal:5673';
(async () => {
  const connection = await amqp.connect(amqpUrl, 'heartbeat=60');
  const channel = await connection.createChannel();
 
    console.log('Publishing');
    const exchange = 'sensordata';
    const queue = 'sensordata';
    const routingKey = 'sensordata';
    
    await channel.assertExchange(exchange, 'direct', {durable: true});
    await channel.assertQueue(queue, {durable: true});
    await channel.bindQueue(queue, exchange, routingKey);
    
   channel.publish(exchange, routingKey, Buffer.from(JSON.stringify(data)));
    console.log('Message published');
})()
.
  // Prints "caught woops"
  catch(error => { console.log('caught', error.message); });;


  app.get('/', (req, res) => res.send("Sending data... /n"+data))
});


app.listen(3000,() => console.log("Server ready") )

