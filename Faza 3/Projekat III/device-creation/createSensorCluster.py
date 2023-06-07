import requests, json, sys, re, time, os, warnings, argparse
from requests_toolbelt.multipart.encoder import MultipartEncoder
from datetime import datetime

warnings.filterwarnings("ignore")

parser=argparse.ArgumentParser(description="Python script for creating a new device from scratch in EdgeX Foundry")
parser.add_argument('-ip',help='EdgeX Foundry IP address', required=True)

args=vars(parser.parse_args())

edgex_ip=args["ip"]


def createValueDescriptors():
    url = 'http://%s:48080/api/v1/valuedescriptor' % edgex_ip


    payload =   {
                    "name":"temperature",
                    "description":"Ambient temperature in Celcius", 
                    "min":"-50",
                    "max":"100",
                    "type":"Int64",
                    "uomLabel":"count",
                    "defaultValue":"0",
                    "formatting":"%s",
                    "labels":["environment", "temperature"]
                }
    headers = {'content-type': 'application/json'}
    response = requests.post(url, data=json.dumps(payload), headers=headers, verify=False)
    print("Result for createValueDescriptors #2: %s - Message: %s" % (response, response.text))

    payload =   {
                    "name":"humidity",
                    "description":"Ambient humidity in percent", 
                    "min":"0",
                    "max":"100",
                    "type":"Int64",
                    "uomLabel":"count",
                    "defaultValue":"0",
                    "formatting":"%s",
                    "labels":["environment","humidity"]
                }
    headers = {'content-type': 'application/json'}
    response = requests.post(url, data=json.dumps(payload), headers=headers, verify=False)
    print("Result for createValueDescriptors #1: %s - Message: %s" % (response, response.text))


    payload =   {
                    "name":"ping",
                    "description":"Ping description", 
                    "type":"Int64",
                    "uomLabel":"count",
                    "defaultValue":"0",
                    "formatting":"%s",
                    "labels":["environment","ping"]
                }
    headers = {'content-type': 'application/json'}
    response = requests.post(url, data=json.dumps(payload), headers=headers, verify=False)
    print("Result for createValueDescriptors #1: %s - Message: %s" % (response, response.text))


    payload =   {
                    "name":"time",
                    "description":"Time detection", 
                    "type":"Object",
                    "uomLabel":"count",
                    "defaultValue":"1/01/2000 00:00:00 AM",
                    "formatting":"%s",
                    "labels":["environment","time"]
                }
    headers = {'content-type': 'application/json'}
    response = requests.post(url, data=json.dumps(payload), headers=headers, verify=False)
    print("Result for createValueDescriptors #1: %s - Message: %s" % (response, response.text))



def uploadDeviceProfile():
    multipart_data = MultipartEncoder(
        fields={
                'file': ('sensorClusterDeviceProfile.yaml', open('sensorClusterDeviceProfile.yaml', 'rb'), 'text/plain')
               }
        )

    url = 'http://%s:48081/api/v1/deviceprofile/uploadfile' % edgex_ip
    response = requests.post(url, data=multipart_data,
                      headers={'Content-Type': multipart_data.content_type})

    print("Result of uploading device profile: %s with message %s" % (response, response.text))



def addNewDevice():
    url = 'http://%s:48081/api/v1/device' % edgex_ip

    payload =   {
                    "name":"Sensor_cluster_project_iii",
                    "description":"Raspberry Pi sensor cluster",
                    "adminState":"unlocked",
                    "operatingState":"enabled",
                    "protocols": {
                        "example": {
                        "host": "dummy",
                        "port": "1234",
                        "unitID": "1"
                        }
                    },
                    "labels": ["Humidity sensor","Temperature sensor","DHT11", "Ping sensor", "Time sensor"],
                    "location":"Nis",
                    "service": {
                        "name":"edgex-device-rest"
                    },
                    "profile": {
                        "name":"SensorClusterProjectIII"
                    }
                }
    headers = {'content-type': 'application/json'}
    response = requests.post(url, data=json.dumps(payload), headers=headers, verify=False)
    print("Result for addNewDevice: %s - Message: %s" % (response, response.text))



if __name__ == "__main__":
    createValueDescriptors()
    uploadDeviceProfile()
    addNewDevice()
