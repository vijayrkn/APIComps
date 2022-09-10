# APIComps (an API comparison tool)

## Structure of the project: 

- src/services
    - aspnetcore (dotnet running on port 5001)
    - expressjs (node running on port 5002)
    - flask (python running on port 5003)
    - gin (go running on port 5004)
- src/proxy 
    - YARP (running on port 5000 that redirects traffic to 5001, 5002, 5003 & 5004 based on round robin policy)
- src/driver
    - driver program that send http request to port 5000 to test the time taken to run the API for each stack.
    
    
## How to try this project?
    - Run setup.sh
        - this will run the dotnetcore project on 5001
        - this will run the nodejs project on 5002
        - this will run the python project on 5003
        - this will run the go project on 5004
        - this will run the YARP proxy on 5000 that routes requests to the above 4 servers based on round robin policy.
    - Run test.sh
        - this will run the driver project that sends 50 requests each (computing the fib series of each number till 50) to each of the servers and pick the server that responds in the least time.
        
## Sample Result
![image](https://user-images.githubusercontent.com/3981619/189455610-4dfad6b5-1e6d-477e-a584-d1161862276f.png)

### Interpretation of the result: 
Out of the 50 requests sent to each of the 4 stacks: 
    - node processed 13 requests faster than others
    - dotnet processed 19 requests faster than others
    - go processed 17 requests faster than others & 
    - python processed 1 request faster than others.
