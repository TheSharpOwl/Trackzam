# Trackzam
Students tracker for lecturer's purposes.

### List of detected states
#### These states are used in the document to describe the state of the user at specific timestamps in the output document without dependance on source.

* User is actively present
* User is looking away
* User is not present
* Voice detected
* Window focus changed
* One of sources is unavailable (Complete silence, Nothing on user video, etc)

### List of processed states for each source

#### Sound
* Usual background silence
* Compelte silence
* User is speaking
* Background speaking

#### Keyboard
* Typing
* System keys
* Idle

#### User Video
* Looking at screen
* Looking away
* Not present

#### Opened windows
* %name of the active window%



## Instructions

### install pip3, git, docker, docker-compose on your machine

### clone the repo in your machine
```
git clone https://github.com/TheSharpOwl/Trackzam.git
cd Trackzam
```

### Install all the requirements
```
pip3 install -r Server/req.py
pip3 install -r VideoServer/req.py
```

### apply all migrations
```
python3 Server/manage.py makemigrations
python3 Server/manage.py migrate

python3 VideoServer/manage.py makemigrations
python3 VideoServer/manage.py migrate
```

### create superusers in both of the servers
```
python3 Server/manage.py createsuperuser and follow the process
python3 VideoServer/manage.py createsuperuser and follow the process
```

### Specify config files Server/config.json Server/config.json


### create regular users (the same users should be on both servers)
  * you can use django admin panel
  * you can use django shell
```
python3 Server/manage.py shell
```
and inside shell type
```
from django.contrib.auth.models import User
user = User.objects.create_user('John', 'lennon@thebeatles.com', 'johnpassword')
user.save()
```

Repeat for VideoServer
```
python3 VideoServer/manage.py shell
```
and inside shell type
```
from django.contrib.auth.models import User
user = User.objects.create_user('John', 'lennon@thebeatles.com', 'johnpassword')
user.save()
```

### Run the servers on different consoles
```
python3 Server/manage.py runserver
python3 VideoServer/manage.py runserver
```

### If everything works fine, run the same thing using docker-compose

  * If you changed ports in config files, do the same changes in **Server/Dockerfile VideoServer/Dockerfile docker-compose.yaml** files
  
  * Then from Trackzam directory run
  ```
  docker-compose build
  docker-compose up
  ```
