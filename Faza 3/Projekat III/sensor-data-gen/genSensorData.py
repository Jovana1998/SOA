import csv
import requests
import json
import random
import time


edgexip = '127.0.0.1'

tempList = list()
humList = list()
pingList = list()
timeList = list()

with open("data.csv", "r") as file:
    csv_reader = csv.DictReader(file)
    for row in csv_reader:
        tempList.append(row['temperature_c'])
        humList.append(row['humidity_p'])
        pingList.append(row['ping_ms'])
        timeList.append(row['time_id'])

def generateSensorData():

    i = random.randint(1, 130000)
    temp = int(round(float(tempList[i])))
    humidity = int(round(float(humList[i])))
    ping = int(round(float(pingList[i])))
    timee = timeList[i]
    

    print("Sending values: Temperature %s, Humidity %s, Ping %s, Time %s" % (temp, humidity, ping, timee))

    return (temp, humidity, ping, timee)
            

if __name__ == "__main__":

    sensorTypes = ["temperature", "humidity", "ping", "time"]

    while(1):

        (temperature, humidity, ping, timee) = generateSensorData()

        url = 'http://%s:49986/api/v1/resource/Sensor_cluster_project_iii/temperature' % edgexip
        payload = temperature
        headers = {'content-type': 'application/json'}
        response = requests.post(url, data=json.dumps(payload), headers=headers, verify=False)

        url = 'http://%s:49986/api/v1/resource/Sensor_cluster_project_iii/humidity' % edgexip
        payload = humidity
        headers = {'content-type': 'application/json'}
        response = requests.post(url, data=json.dumps(payload), headers=headers, verify=False)

        url = 'http://%s:49986/api/v1/resource/Sensor_cluster_project_iii/ping' % edgexip
        payload = ping
        headers = {'content-type': 'application/json'}
        response = requests.post(url, data=json.dumps(payload), headers=headers, verify=False)

        url = 'http://%s:49986/api/v1/resource/Sensor_cluster_project_iii/time' % edgexip
        payload = timee
        headers = {'content-type': 'application/json'}
        response = requests.post(url, data=json.dumps(payload), headers=headers, verify=False)

        time.sleep(5)