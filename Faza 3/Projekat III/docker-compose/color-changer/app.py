from os import environ
from flask import Flask, render_template, request, make_response, jsonify
from flask_restful import reqparse

app = Flask(__name__)
value = "green"


@app.route('/')
def index():
    content = make_response(render_template('index.html'))
    return content


@app.route('/_ajaxAutoRefresh', methods= ['GET'])
def stuff():
    return jsonify(value=value)


@app.route('/register', methods=['POST'])
def register():
    request.get_json(force=True)

    parser = reqparse.RequestParser()
    parser.add_argument('id', required=True)
    args = parser.parse_args()

    id = args['id']

    print("Registering device: ", id)

    returnData = "Device registered"

    return returnData, 201


@app.route('/changeVal', methods=['PUT'])
def change_value():
    global value
    request.get_json(force=True)

    parser = reqparse.RequestParser()
    parser.add_argument('value', required=True)
    args = parser.parse_args()

    value = (args['value'])
    
    print("Changing value to", value)
    
    returnData = "Command accepted"

    return returnData, 201
    
@app.route('/read-value', methods=['GET'])
def read_value():
    global value
    
    print("Reading value")
    
    returnData = value
    
    return returnData, 201


if __name__ == "__main__":
	app.run(debug=eval(environ.get("DEBUG", "True")), \
                host='0.0.0.0', \
                port=int(environ.get('PORT', '5000')), threaded=True)
