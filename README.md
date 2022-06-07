# Trackzam
Students tracker for teaching and exams.

## Description:
Trackzam contains 2 parts:
1. Student side software: where they can record themselves while attending a lecture or an exam and the video is sent to the server
2. Server (which is meant for teachers): receives the videos, analyzes them for example if the student is not paying attention to the lecture and reports it to the teachers.

## [Live demo](https://youtu.be/e3om6VbYJJU?t=233)
(time stamp is when showing it live, you can watch the rest of the video too)

## How to Run:

### 1. Server part

#### a. install git, docker, docker-compose

#### b. clone the repo in your machine
```
git clone https://github.com/TheSharpOwl/Trackzam.git
cd Trackzam
```

#### c. Set proper environment variables in docker-compose.yml 
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

#### d. Make sure that firewall allows TCP connection on ports 8000 and 8080 (or custom ports if you changed them in docker-compose.yml)

#### Use docker-compose to build and run the application:
  ```
  docker-compose build
  docker-compose up
  ```
  
### 2. Client Part

#### a. Download [Latest release](https://github.com/TheSharpOwl/Trackzam/releases)

#### b. How to use client application:
   1. Configuration:
         * To change temporary storage directory, you could use button "Change dir".
         * To change IP address of a server to send data, you should go application directory, open "Config.json" file and change ServerIP field to your server IP address.
         * Please make sure you follow .json files standard to correctly change server IP.

   2. Recording:
         * To start recording, press the "Start Recording Session" button. It should change it's name to "Stop Recording Session".
         * To stop recording, press the "Stop Recording Session" button. It should change it's name to "Start Recording Session".
         * After finishing recording you will see where all of your recording files where stored.
