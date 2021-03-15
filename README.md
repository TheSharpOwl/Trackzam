# Trackzam
Students tracker for lecturer's purposes.

## How to Run servers:

### install git, docker, docker-compose on your machine

### clone the repo in your machine
```
git clone https://github.com/TheSharpOwl/Trackzam.git
cd Trackzam
```

### Set proper environment variables in docker-compose.yml 
By default:
```
   - host_ip=34.71.243.7                    # should be public IP of the host
   - self_port=8000                         
   - DJANGO_SU_NAME=adminadmin              # administrator account with these credentials will be created 
   - DJANGO_SU_EMAIL=admin@admin.ru         
   - DJANGO_SU_PASSWORD=adminpassword       
   - DJANGO_U_NAME=username                 # user account with these credentials will be created
   - DJANGO_U_EMAIL=username@username.ru    
   - DJANGO_U_PASSWORD=usernamepassword     
```   

### Make sure that firewall allows TCP connection on ports 8000 and 8080 (or custom ports if you changed them in docker-compose.yml)

### Use docker-compose to build and run the application 
  ```
  docker-compose build
  docker-compose up
  ```
  
## Client
How to use client application

### Configuration

 * To change temporary storage directory, you could use button "Change dir".
 * To change IP address of a server to send data, you should go application directory, open "Config.json" file and change ServerIP field to your server IP address.
 * Please make sure you follow .json files standard to correctly change server IP.

### Recording
 * To start recording, press the "Start Recording Session" button. It should change it's name to "Stop Recording Session".
 * To stop recording, press the "Stop Recording Session" button. It should change it's name to "Start Recording Session".
 * After finishing recording you will see where all of your recording files where stored.
