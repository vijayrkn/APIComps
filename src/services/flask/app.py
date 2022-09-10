from flask import Flask, request, jsonify, make_response
app = Flask(__name__)


@app.route('/compute')
def Fib():
    n = int(request.args.get('n'))
    a = 0
    b = 1
    result = 0
    if n == 0:
        result = a
    else:
        for i in range(2, n+1):
            c = a + b
            a = b
            b = c
        result = b

    response = make_response(jsonify({'Result': result}))
    response.headers["stack"] = "python"
    return response


if __name__ == '__main__':
    app.run(host="localhost", port=5003)
