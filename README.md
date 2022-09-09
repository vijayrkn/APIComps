# APIComps

## Structure of the project: 

- src/services
    - aspnetcore (running on port 5001)
    - nodejs (running on port 5002)
    - flask (running on port 5003)
- src/proxy 
    - YARP (running on port 5000 that redirects traffic to 5001, 5002 & 5003 based on round robin policy)
- src/driver
    - driver program that send http request to port 5000 to test the time taken to run the API
